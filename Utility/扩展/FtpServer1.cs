using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Utility.扩展
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (FtpServer server = new FtpServer())
            {
                server.Start();

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);
            }
        }
    }

    public class FtpServer : IDisposable
    {
        //ILog _log = LogManager.GetLogger(typeof(FtpServer));

        private bool _disposed = false;
        private bool _listening = false;

        private TcpListener _listener;
        private List<ClientConnection> _activeConnections;

        private IPEndPoint _localEndPoint;

        /// <summary>
        /// string: 账号; string: 密码; bool: 验证结果, true:表示成功, false:表示失败
        /// </summary>
        public Func<string, string, bool> _登录验证 { get;set;}


        /// <summary>
        /// string: 账号; string: 目录
        /// </summary>
        public Func<string, string> _查询目录 { get; set; }

        /// <summary>
        /// string: 账号; string: 操作命令; string: 命令参数; bool: 验证结果, true:表示成功, false:表示失败
        /// </summary>
        public Func<string, string, string, bool> _操作验证 { get; set; }

        public FtpServer()
            : this(IPAddress.Any, 21)
        {
        }


        public FtpServer(IPAddress ipAddress, int port)
        {
            _localEndPoint = new IPEndPoint(ipAddress, port);
        }

        public void Start()
        {
            _listener = new TcpListener(_localEndPoint);

            //_log.Info("#Version: 1.0");
            //_log.Info("#Fields: date time c-ip c-port cs-username cs-method cs-uri-stem sc-status sc-bytes cs-bytes s-name s-port");

            _listening = true;
            _listener.Start();

            _activeConnections = new List<ClientConnection>();

            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
        }

        public void Stop()
        {
            //_log.Info("Stopping FtpServer");

            _listening = false;
            if (_listener != null)
            {
                _listener.Stop();
                _listener = null;
            }
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            if (_listening)
            {
                _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);

                TcpClient client = _listener.EndAcceptTcpClient(result);

                ClientConnection connection = new ClientConnection(client);
                connection._操作验证 = _操作验证;
                connection._查询目录 = _查询目录;
                connection._登录验证 = _登录验证;

                _activeConnections.Add(connection);

                ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();

                    foreach (ClientConnection conn in _activeConnections)
                    {
                        conn.Dispose();
                    }
                }
            }

            _disposed = true;
        }


        class ClientConnection : IDisposable
        {
            private class DataConnectionOperation
            {
                public Func<NetworkStream, string, string> Operation { get; set; }
                public string Arguments { get; set; }
            }

            #region Copy Stream Implementations

            private static long CopyStream(Stream input, Stream output, int bufferSize)
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                long total = 0;

                while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, count);
                    total += count;
                }

                return total;
            }

            private static long CopyStreamAscii(Stream input, Stream output, int bufferSize)
            {
                char[] buffer = new char[bufferSize];
                int count = 0;
                long total = 0;

                using (StreamReader rdr = new StreamReader(input, Encoding.ASCII))
                {
                    using (StreamWriter wtr = new StreamWriter(output, Encoding.ASCII))
                    {
                        while ((count = rdr.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            wtr.Write(buffer, 0, count);
                            total += count;
                        }
                    }
                }

                return total;
            }

            private long CopyStream(Stream input, Stream output)
            {
                Stream limitedStream = output; // new RateLimitingStream(output, 131072, 0.5);

                //if (_connectionType == TransferType.Image)
                //{
                return CopyStream(input, limitedStream, 4096);
                //}
                //else
                //{
                //    return CopyStreamAscii(input, limitedStream, 4096);
                //}
            }

            #endregion

            #region Enums

            private enum TransferType
            {
                Ascii,
                Ebcdic,
                Image,
                Local,
            }

            private enum FormatControlType
            {
                NonPrint,
                Telnet,
                CarriageControl,
            }

            private enum DataConnectionType
            {
                Passive,
                Active,
            }

            private enum FileStructureType
            {
                File,
                Record,
                Page,
            }

            #endregion

            private bool _disposed = false;

            private TcpListener _passiveListener;

            private TcpClient _controlClient;
            private TcpClient _dataClient;

            private NetworkStream _controlStream;
            private StreamReader _controlReader;
            private StreamWriter _controlWriter;

            private TransferType _connectionType = TransferType.Image;
#pragma warning disable 414
            private FormatControlType _formatControlType = FormatControlType.NonPrint;
#pragma warning restore 414
            private DataConnectionType _dataConnectionType = DataConnectionType.Active;
#pragma warning disable 414
            private FileStructureType _fileStructureType = FileStructureType.File;
#pragma warning restore 414

            private string _username;
            private string _root;
            private string _currentDirectory;
            private IPEndPoint _dataEndpoint;
            private IPEndPoint _remoteEndPoint;

            private X509Certificate _cert = null;
            private SslStream _sslStream;

            private string _clientIP;

            private bool _logon;

            private List<string> _validCommands;

            /// <summary>
            /// string: 账号; string: 密码; bool: 验证结果, true:表示成功, false:表示失败
            /// </summary>
            public Func<string, string, bool> _登录验证;


            /// <summary>
            /// string: 账号; string: 目录
            /// </summary>
            public Func<string, string> _查询目录;

            /// <summary>
            /// string: 账号; string: 操作命令; string: 命令参数; bool: 验证结果, true:表示成功, false:表示失败
            /// </summary>
            public Func<string, string, string, bool> _操作验证;

            public ClientConnection(TcpClient client)
            {
                _controlClient = client;

                _validCommands = new List<string>();
            }

            private string CheckUser()
            {
                if (!_logon)
                {
                    return "530 Not logged in";
                }

                return null;
            }

            private Encoding _编码 = Encoding.Default;

            public void HandleClient(object obj)
            {
                _remoteEndPoint = (IPEndPoint)_controlClient.Client.RemoteEndPoint;

                _clientIP = _remoteEndPoint.Address.ToString();

                _controlStream = _controlClient.GetStream();

                _controlReader = new StreamReader(_controlStream, _编码);
                _controlWriter = new StreamWriter(_controlStream, _编码);

                _controlWriter.WriteLine("220 Service Ready.");
                _controlWriter.Flush();

                _validCommands.AddRange(new string[] { "AUTH", "USER", "PASS", "QUIT", "HELP", "NOOP" });

                string line;

                _dataClient = new TcpClient();

                string renameFrom = null;

                try
                {
                    while ((line = _controlReader.ReadLine()) != null)
                    {
                        Debug.WriteLine(string.Format("FTP: {0}", line));

                        string response = null;

                        string[] command = line.Split(' ');

                        string cmd = command[0].ToUpperInvariant();
                        string arguments = command.Length > 1 ? line.Substring(command[0].Length + 1) : null;

                        if (arguments != null && arguments.Trim().Length == 0)
                        {
                            arguments = null;
                        }

                        if (!_validCommands.Contains(cmd))
                        {
                            response = CheckUser();
                        }

                        if (cmd != "RNTO")
                        {
                            renameFrom = null;
                        }

                        if (response == null)
                        {
                            switch (cmd)
                            {
                                case "USER":
                                    response = User(arguments);
                                    break;
                                case "PASS":
                                    response = Password(arguments);
                                    break;
                                case "CWD":
                                    response = ChangeWorkingDirectory(arguments);
                                    break;
                                case "CDUP":
                                    response = ChangeWorkingDirectory("..");
                                    break;
                                case "QUIT":
                                    response = "221 Service closing control connection";
                                    break;
                                case "REIN":
                                    _logon = false;
                                    _username = null;
                                    _passiveListener = null;
                                    _dataClient = null;

                                    response = "220 Service ready for new user";
                                    break;
                                case "PORT":
                                    response = Port(arguments);
                                    break;
                                case "PASV":
                                    response = Passive();
                                    break;
                                case "TYPE":
                                    response = Type(command[1], command.Length == 3 ? command[2] : null);
                                    break;
                                case "STRU":
                                    response = Structure(arguments);
                                    break;
                                case "MODE":
                                    response = Mode(arguments);
                                    break;
                                case "RNFR":
                                    renameFrom = arguments;
                                    response = "350 Requested file action pending further information";
                                    break;
                                case "RNTO":
                                    response = Rename(renameFrom, arguments);
                                    break;
                                case "DELE":
                                    response = Delete(arguments);
                                    break;
                                case "RMD":
                                    response = RemoveDir(arguments);
                                    break;
                                case "MKD":
                                    response = CreateDir(arguments);
                                    break;
                                case "PWD":
                                    response = PrintWorkingDirectory();
                                    break;
                                case "RETR":
                                    response = Retrieve(arguments);
                                    break;
                                case "STOR":
                                    response = Store(arguments);
                                    break;
                                case "STOU":
                                    response = StoreUnique();
                                    break;
                                case "APPE":
                                    response = Append(arguments);
                                    break;
                                case "LIST":
                                    response = List(_currentDirectory); //List(arguments ?? _currentDirectory);
                                    break;
                                case "SYST":
                                    response = "215 UNIX Type: L8";
                                    break;
                                case "NOOP":
                                    response = "200 OK";
                                    break;
                                case "ACCT":
                                    response = "200 OK";
                                    break;
                                case "ALLO":
                                    response = "200 OK";
                                    break;
                                case "NLST":
                                    response = "502 Command not implemented";
                                    break;
                                case "SITE":
                                    response = "502 Command not implemented";
                                    break;
                                case "STAT":
                                    response = "502 Command not implemented";
                                    break;
                                case "HELP":
                                    response = "502 Command not implemented";
                                    break;
                                case "SMNT":
                                    response = "502 Command not implemented";
                                    break;
                                case "REST":
                                    response = "502 Command not implemented";
                                    break;
                                case "ABOR":
                                    response = "502 Command not implemented";
                                    break;

                                // Extensions defined by rfc 2228
                                case "AUTH":
                                    response = Auth(arguments);
                                    break;

                                // Extensions defined by rfc 2389
                                case "FEAT":
                                    response = FeatureList();
                                    break;
                                case "OPTS":
                                    //if (arguments == "utf8 on")
                                    //{
                                    //    _controlReader.Dispose();
                                    //    _controlWriter.Dispose();
                                    //    _controlStream = _controlClient.GetStream();
                                    //    _controlReader = new StreamReader(_controlStream, Encoding.UTF8);
                                    //    _controlWriter = new StreamWriter(_controlStream, Encoding.UTF8);
                                    //}
                                    response = Options(arguments);
                                    break;

                                // Extensions defined by rfc 3659
                                case "MDTM":
                                    response = FileModificationTime(arguments);
                                    break;
                                case "SIZE":
                                    response = FileSize(arguments);
                                    break;

                                // Extensions defined by rfc 2428
                                case "EPRT":
                                    response = EPort(arguments);
                                    break;
                                case "EPSV":
                                    response = EPassive();
                                    break;

                                default:
                                    response = "502 Command not implemented";
                                    break;
                            }
                        }

                        if (_controlClient == null || !_controlClient.Connected)
                        {
                            break;
                        }
                        else
                        {
                            _controlWriter.WriteLine(response);
                            _controlWriter.Flush();

                            if (response.StartsWith("221"))
                            {
                                break;
                            }

                            if (cmd == "AUTH")
                            {
                                _cert = new X509Certificate("server.cer");

                                _sslStream = new SslStream(_controlStream);

                                _sslStream.AuthenticateAsServer(_cert);

                                _controlReader = new StreamReader(_sslStream);
                                _controlWriter = new StreamWriter(_sslStream);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                Dispose();
            }

            private bool IsPathValid(string path)
            {
                return path.StartsWith(_root);
            }

            private string NormalizeFilename(string path)
            {
                if (path == null)
                {
                    path = string.Empty;
                }

                if (path == "/")
                {
                    return _root;
                }
                else if (path.StartsWith("/"))
                {
                    path = new FileInfo(Path.Combine(_root, path.Substring(1))).FullName;
                }
                else
                {
                    path = new FileInfo(Path.Combine(_currentDirectory, path)).FullName;
                }

                return IsPathValid(path) ? path : null;
            }

            #region FTP Commands

            private string FeatureList()
            {
                _controlWriter.WriteLine("211- Extensions supported:");
                _controlWriter.WriteLine(" MDTM");
                _controlWriter.WriteLine(" SIZE");
                return "211 End";
            }

            private string Options(string arguments)
            {
                return "200 Looks good to me...";
            }

            private string Auth(string authMode)
            {
                if (authMode == "TLS")
                {
                    return "234 Enabling TLS Connection";
                }
                else
                {
                    return "504 Unrecognized AUTH mode";
                }
            }

            private string User(string username)
            {
                _username = username;

                return "331 Username ok, need password";
            }

            private string Password(string password)
            {
                if (_登录验证 == null)
                {
                    _logon = true;
                }
                else
                {
                    _logon = _登录验证(_username, password);
                }
                if (_logon)
                {
                    if (_查询目录 == null)
                    {
                        _root = Environment.CurrentDirectory;
                    }
                    else
                    {
                        _root = _查询目录(_username);
                    }
                    _currentDirectory = _root;

                    return "230 User logged in";
                }
                else
                {
                    return "530 Not logged in";
                }
            }

            private string ChangeWorkingDirectory(string pathname)
            {
                if (pathname == "/")
                {
                    _currentDirectory = _root;
                }
                else
                {
                    string newDir;

                    if (pathname.StartsWith("/"))
                    {
                        pathname = pathname.Substring(1).Replace('/', '\\');
                        newDir = Path.Combine(_root, pathname);
                    }
                    else
                    {
                        pathname = pathname.Replace('/', '\\');
                        newDir = Path.Combine(_currentDirectory, pathname);
                    }

                    if (Directory.Exists(newDir))
                    {
                        _currentDirectory = new DirectoryInfo(newDir).FullName;

                        if (!IsPathValid(_currentDirectory))
                        {
                            _currentDirectory = _root;
                        }
                    }
                    else
                    {
                        _currentDirectory = _root;
                    }
                }

                return "250 Changed to new directory";
            }

            private string Port(string hostPort)
            {
                _dataConnectionType = DataConnectionType.Active;

                string[] ipAndPort = hostPort.Split(',');

                byte[] ipAddress = new byte[4];
                byte[] port = new byte[2];

                for (int i = 0; i < 4; i++)
                {
                    ipAddress[i] = Convert.ToByte(ipAndPort[i]);
                }

                for (int i = 4; i < 6; i++)
                {
                    port[i - 4] = Convert.ToByte(ipAndPort[i]);
                }

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(port);

                _dataEndpoint = new IPEndPoint(new IPAddress(ipAddress), BitConverter.ToInt16(port, 0));

                return "200 Data Connection Established";
            }

            private string EPort(string hostPort)
            {
                _dataConnectionType = DataConnectionType.Active;

                char delimiter = hostPort[0];

                string[] rawSplit = hostPort.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

                char ipType = rawSplit[0][0];

                string ipAddress = rawSplit[1];
                string port = rawSplit[2];

                _dataEndpoint = new IPEndPoint(IPAddress.Parse(ipAddress), int.Parse(port));

                return "200 Data Connection Established";
            }

            private string Passive()
            {
                _dataConnectionType = DataConnectionType.Passive;

                IPAddress localIp = ((IPEndPoint)_controlClient.Client.LocalEndPoint).Address;

                _passiveListener = new TcpListener(localIp, 0);
                _passiveListener.Start();

                IPEndPoint passiveListenerEndpoint = (IPEndPoint)_passiveListener.LocalEndpoint;

                byte[] address = passiveListenerEndpoint.Address.GetAddressBytes();
                short port = (short)passiveListenerEndpoint.Port;

                byte[] portArray = BitConverter.GetBytes(port);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(portArray);

                return string.Format("227 Entering Passive Mode ({0},{1},{2},{3},{4},{5})", address[0], address[1],
                    address[2], address[3], portArray[0], portArray[1]);
            }

            private string EPassive()
            {
                _dataConnectionType = DataConnectionType.Passive;

                IPAddress localIp = ((IPEndPoint)_controlClient.Client.LocalEndPoint).Address;

                _passiveListener = new TcpListener(localIp, 0);
                _passiveListener.Start();

                IPEndPoint passiveListenerEndpoint = (IPEndPoint)_passiveListener.LocalEndpoint;

                return string.Format("229 Entering Extended Passive Mode (|||{0}|)", passiveListenerEndpoint.Port);
            }

            private string Type(string typeCode, string formatControl)
            {
                switch (typeCode.ToUpperInvariant())
                {
                    case "A":
                        _connectionType = TransferType.Ascii;
                        break;
                    case "I":
                        _connectionType = TransferType.Image;
                        break;
                    default:
                        return "504 Command not implemented for that parameter";
                }

                if (!string.IsNullOrWhiteSpace(formatControl))
                {
                    switch (formatControl.ToUpperInvariant())
                    {
                        case "N":
                            _formatControlType = FormatControlType.NonPrint;
                            break;
                        default:
                            return "504 Command not implemented for that parameter";
                    }
                }

                return string.Format("200 Type set to {0}", _connectionType);
            }

            private string Delete(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    if (File.Exists(pathname))
                    {
                        File.Delete(pathname);
                    }
                    else
                    {
                        return "550 File Not Found";
                    }

                    return "250 Requested file action okay, completed";
                }

                return "550 File Not Found";
            }

            private string RemoveDir(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    if (Directory.Exists(pathname))
                    {
                        Directory.Delete(pathname);
                    }
                    else
                    {
                        return "550 Directory Not Found";
                    }

                    return "250 Requested file action okay, completed";
                }

                return "550 Directory Not Found";
            }

            private string CreateDir(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    if (!Directory.Exists(pathname))
                    {
                        Directory.CreateDirectory(pathname);
                    }
                    else
                    {
                        return "550 Directory already exists";
                    }

                    return "250 Requested file action okay, completed";
                }

                return "550 Directory Not Found";
            }

            private string FileModificationTime(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    if (File.Exists(pathname))
                    {
                        return string.Format("213 {0}", File.GetLastWriteTime(pathname).ToString("yyyyMMddHHmmss.fff"));
                    }
                }

                return "550 File Not Found";
            }

            private string FileSize(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    if (File.Exists(pathname))
                    {
                        long length = 0;

                        length = new FileInfo(pathname).Length;
                        //using (FileStream fs = File.Open(pathname, FileMode.Open, FileAccess.Read, FileShare.Read))
                        //{
                        //    length = fs.Length;
                        //}

                        return string.Format("213 {0}", length);
                    }
                }

                return "550 File Not Found";
            }

            private string Retrieve(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    if (File.Exists(pathname))
                    {
                        var state = new DataConnectionOperation { Arguments = pathname, Operation = RetrieveOperation };

                        SetupDataConnectionOperation(state);

                        return string.Format("150 Opening {0} mode data transfer for RETR", _dataConnectionType);
                    }
                }

                return "550 File Not Found";
            }

            private string Store(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    var state = new DataConnectionOperation { Arguments = pathname, Operation = StoreOperation };

                    SetupDataConnectionOperation(state);

                    return string.Format("150 Opening {0} mode data transfer for STOR", _dataConnectionType);
                }

                return "450 Requested file action not taken";
            }

            private string Append(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    var state = new DataConnectionOperation { Arguments = pathname, Operation = AppendOperation };

                    SetupDataConnectionOperation(state);

                    return string.Format("150 Opening {0} mode data transfer for APPE", _dataConnectionType);
                }

                return "450 Requested file action not taken";
            }

            private string StoreUnique()
            {
                string pathname = NormalizeFilename(new Guid().ToString());

                var state = new DataConnectionOperation { Arguments = pathname, Operation = StoreOperation };

                SetupDataConnectionOperation(state);

                return string.Format("150 Opening {0} mode data transfer for STOU", _dataConnectionType);
            }

            private string PrintWorkingDirectory()
            {
                string current = _currentDirectory.Replace(_root, string.Empty).Replace('\\', '/');

                if (current.Length == 0)
                {
                    current = "/";
                }

                return string.Format("257 \"{0}\" is current directory.", current);
                ;
            }

            private string List(string pathname)
            {
                pathname = NormalizeFilename(pathname);

                if (pathname != null)
                {
                    var state = new DataConnectionOperation { Arguments = pathname, Operation = ListOperation };

                    SetupDataConnectionOperation(state);

                    return string.Format("150 Opening {0} mode data transfer for LIST", _dataConnectionType);
                }

                return "450 Requested file action not taken";
            }

            private string Structure(string structure)
            {
                switch (structure)
                {
                    case "F":
                        _fileStructureType = FileStructureType.File;
                        break;
                    case "R":
                    case "P":
                        return string.Format("504 STRU not implemented for \"{0}\"", structure);
                    default:
                        return string.Format("501 Parameter {0} not recognized", structure);
                }

                return "200 Command OK";
            }

            private string Mode(string mode)
            {
                if (mode.ToUpperInvariant() == "S")
                {
                    return "200 OK";
                }
                else
                {
                    return "504 Command not implemented for that parameter";
                }
            }

            private string Rename(string renameFrom, string renameTo)
            {
                if (string.IsNullOrWhiteSpace(renameFrom) || string.IsNullOrWhiteSpace(renameTo))
                {
                    return "450 Requested file action not taken";
                }

                renameFrom = NormalizeFilename(renameFrom);
                renameTo = NormalizeFilename(renameTo);

                if (renameFrom != null && renameTo != null)
                {
                    if (File.Exists(renameFrom))
                    {
                        File.Move(renameFrom, renameTo);
                    }
                    else if (Directory.Exists(renameFrom))
                    {
                        Directory.Move(renameFrom, renameTo);
                    }
                    else
                    {
                        return "450 Requested file action not taken";
                    }

                    return "250 Requested file action okay, completed";
                }

                return "450 Requested file action not taken";
            }

            #endregion

            #region DataConnection Operations

            private void HandleAsyncResult(IAsyncResult result)
            {
                if (_dataConnectionType == DataConnectionType.Active)
                {
                    _dataClient.EndConnect(result);
                }
                else
                {
                    _dataClient = _passiveListener.EndAcceptTcpClient(result);
                }
            }

            private void SetupDataConnectionOperation(DataConnectionOperation state)
            {
                if (_dataConnectionType == DataConnectionType.Active)
                {
                    _dataClient = new TcpClient(_dataEndpoint.AddressFamily);
                    _dataClient.BeginConnect(_dataEndpoint.Address, _dataEndpoint.Port, DoDataConnectionOperation, state);
                }
                else
                {
                    _passiveListener.BeginAcceptTcpClient(DoDataConnectionOperation, state);
                }
            }

            private void DoDataConnectionOperation(IAsyncResult result)
            {
                HandleAsyncResult(result);

                DataConnectionOperation op = result.AsyncState as DataConnectionOperation;

                string response;

                using (NetworkStream dataStream = _dataClient.GetStream())
                {
                    response = op.Operation(dataStream, op.Arguments);
                }

                _dataClient.Close();
                _dataClient = null;

                _controlWriter.WriteLine(response);
                _controlWriter.Flush();
            }

            private string RetrieveOperation(NetworkStream dataStream, string pathname)
            {
                try
                {
                    using (FileStream fs = new FileStream(pathname, FileMode.Open, FileAccess.Read))
                    {
                        CopyStream(fs, dataStream);
                    }
                }
                catch (Exception)
                {
                    var __源文件 = pathname;
                    var __备份路径 = pathname + Guid.NewGuid();
                    File.Copy(__源文件, __备份路径, true);
                    using (FileStream fs = new FileStream(__备份路径, FileMode.Open, FileAccess.Read))
                    {
                        CopyStream(fs, dataStream);
                    }
                    File.Delete(__备份路径);
                }

                return "226 Closing data connection, file transfer successful";
            }

            private string StoreOperation(NetworkStream dataStream, string pathname)
            {
                long bytes = 0;

                using (
                    FileStream fs = new FileStream(pathname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None,
                        4096, FileOptions.SequentialScan))
                {
                    bytes = CopyStream(dataStream, fs);
                }

                return "226 Closing data connection, file transfer successful";
            }

            private string AppendOperation(NetworkStream dataStream, string pathname)
            {
                long bytes = 0;

                using (
                    FileStream fs = new FileStream(pathname, FileMode.Append, FileAccess.Write, FileShare.None, 4096,
                        FileOptions.SequentialScan))
                {
                    bytes = CopyStream(dataStream, fs);
                }

                //LogEntry logEntry = new LogEntry
                //{
                //    Date = DateTime.Now,
                //    CIP = _clientIP,
                //    CSMethod = "APPE",
                //    CSUsername = _username,
                //    SCStatus = "226",
                //    CSBytes = bytes.ToString()
                //};

                //_log.Info(logEntry);

                return "226 Closing data connection, file transfer successful";
            }

            private string ListOperation(NetworkStream dataStream, string pathname)
            {
                StreamWriter dataWriter = new StreamWriter(dataStream, Encoding.Default);

                IEnumerable<string> directories = Directory.EnumerateDirectories(pathname);

                foreach (string dir in directories)
                {
                    DirectoryInfo d = new DirectoryInfo(dir);

                    string date = d.LastWriteTime < DateTime.Now - TimeSpan.FromDays(180)
                        ? d.LastWriteTime.ToString("MM dd yyyy")
                        : d.LastWriteTime.ToString("MM dd HH:mm");

                    string line = string.Format("drwxr-xr-x    2 2003     2003     {0,8} {1} {2}", "4096", date, d.Name);

                    dataWriter.WriteLine(line);
                    dataWriter.Flush();
                }

                IEnumerable<string> files = Directory.EnumerateFiles(pathname);

                foreach (string file in files)
                {
                    FileInfo f = new FileInfo(file);

                    string date = f.LastWriteTime < DateTime.Now - TimeSpan.FromDays(180)
                        ? f.LastWriteTime.ToString("MM dd yyyy")
                        : f.LastWriteTime.ToString("MM dd HH:mm");

                    string line = string.Format("-rw-r--r--    2 2003     2003     {0,8} {1} {2}", f.Length, date,
                        f.Name);

                    dataWriter.WriteLine(line);
                    dataWriter.Flush();
                }

                //LogEntry logEntry = new LogEntry
                //{
                //    Date = DateTime.Now,
                //    CIP = _clientIP,
                //    CSMethod = "LIST",
                //    CSUsername = _username,
                //    SCStatus = "226"
                //};

                //_log.Info(logEntry);

                return "226 Transfer complete";
            }

            #endregion

            #region IDisposable

            public void Dispose()
            {
                Dispose(true);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        if (_controlClient != null)
                        {
                            _controlClient.Close();
                        }

                        if (_dataClient != null)
                        {
                            _dataClient.Close();
                        }

                        if (_controlStream != null)
                        {
                            _controlStream.Close();
                        }

                        if (_controlReader != null)
                        {
                            _controlReader.Close();
                        }

                        if (_controlWriter != null)
                        {
                            _controlWriter.Close();
                        }
                    }
                }

                _disposed = true;
            }

            #endregion
        }

    }


}
