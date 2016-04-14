using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Utility.扩展
{

    /// <summary>
    /// The <c>FtpConnection</c> class provides the ability to connect to FTP servers.
    ///     using (FtpConnection ftp = new FtpConnection("ftpserver", "username", "password"))
            //{
                
            //    ftp.Open(); /* Open the FTP connection */
            //    ftp.Login(); /* Login using previously provided credentials */

            //    if (ftp.DirectoryExists("/incoming")) /* check that a directory exists */
            //        ftp.SetCurrentDirectory("/incoming"); /* change current directory */

            //    if (ftp.FileExists("/incoming/file.txt"))  /* check that a file exists */
            //        ftp.GetFile("/incoming/file.txt", false); /* download /incoming/file.txt as file.txt to current executing directory, overwrite if it exists */

            //    //do some processing

            //    try
            //    {
            //        ftp.SetCurrentDirectory("/outgoing");
            //        ftp.PutFile(@"c:\localfile.txt", "file.txt"); /* upload c:\localfile.txt to the current ftp directory as file.txt */
            //    }
            //    catch (FtpException e)
            //    {
            //        Console.WriteLine(String.Format("FTP Error: {0} {1}", e.ErrorCode, e.Message));
            //    }

            //    foreach(var dir in ftp.GetDirectories("/incoming/processed"))
            //    {
            //        Console.WriteLine(dir.Name);
            //        Console.WriteLine(dir.CreationTime);
            //        foreach (var file in dir.GetFiles())
            //        {
            //            Console.WriteLine(file.Name);
            //            Console.WriteLine(file.LastAccessTime);
            //        }
            //    }
            //}
    /// </summary>
    public class FtpClient : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect to.</param>
        public FtpClient(string host)
        {
            _host = host;
        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="port">An <see cref="Int32"/> type representing the port on which to connect.</param>
        public FtpClient(string host, int port)
        {
            _host = host;
            _port = port;
        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="username">A <see cref="String"/> type representing the username with which to authenticate.</param>
        /// <param name="password">A <see cref="String"/> type representing the password with which to authenticate.</param>
        public FtpClient(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="port">An <see cref="Int32"/> type representing the port on which to connect.</param>
        /// <param name="username">A <see cref="String"/> type representing the username with which to authenticate.</param>
        /// <param name="password">A <see cref="String"/> type representing the password with which to authenticate.</param>
        public FtpClient(string host, int port, string username, string password)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Establishes a connection to the host.
        /// </summary>
        /// <exception cref="ArgumentNullException">If Host is null or empty.</exception>
        public void Open()
        {
            if(String.IsNullOrEmpty(_host)) throw new ArgumentNullException("Host");

            _hInternet = WININET.InternetOpen(
                System.Environment.UserName,
                WININET.INTERNET_OPEN_TYPE_PRECONFIG,
                null,
                null,
                WININET.INTERNET_FLAG_SYNC);

            if (_hInternet == IntPtr.Zero)
            {
                Error();
            }
        }

        /// <summary>
        /// Logs into the host server using the credentials provided when the class was instantiated.
        /// </summary>
        public void Login()
        {
            Login(_username, _password);
        }

        /// <summary>
        /// Logs into the host server using the provided credentials.
        /// </summary>
        /// <exception cref="ArgumentNullException">If <paramref name="username"/> or <paramref name="password"/> are null.</exception>
        /// <param name="username">A <see cref="String" /> type representing the user name with which to authenticate.</param>
        /// <param name="password">A <see cref="String" /> type representing the password with which to authenticate.</param>
        public void Login(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");
            {
                
            }
            _hConnect = WININET.InternetConnect(_hInternet,
                _host,
                _port,
                username,
                password,
                WININET.INTERNET_SERVICE_FTP,
                WININET.INTERNET_FLAG_PASSIVE,
                IntPtr.Zero);

            if (_hConnect == IntPtr.Zero)
            {
                Error();
            }
        }

        /// <summary>
        /// Changes the current FTP working directory to the specified path.
        /// </summary>
        /// <exception cref="FtpException">If the directory does not exist on the FTP server.</exception>
        /// <param name="directory">A <see cref="String"/> representing the file path of the directory.</param>
        public void SetCurrentDirectory(string directory)
        {
            int ret = WININET.FtpSetCurrentDirectory(
                _hConnect,
                directory);

            if (ret == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Changes the local working directory to the specified path.
        /// </summary>
        /// <exception cref="InvalidDataException">If the directory does not exist on the local system.</exception>
        /// <param name="directory"></param>
        public void SetLocalDirectory(string directory)
        {
            if(Directory.Exists(directory))
                System.Environment.CurrentDirectory = directory;
            else
                throw new InvalidDataException(String.Format("{0} is not a directory!", directory));
        }

        /// <summary>
        /// Gets the current working FTP directory
        /// </summary>
        /// <returns>A <see cref="String"> representing the current working directory.</see></returns>
        public string GetCurrentDirectory()
        {
            int buffLength = WINAPI.MAX_PATH + 1;
            StringBuilder str = new StringBuilder(buffLength);
            int ret = WININET.FtpGetCurrentDirectory(_hConnect, str, ref buffLength);

            if (ret == 0)
            {
                Error();
                return null;
            }

            return str.ToString();
        }

        /// <summary>
        /// Get the current FtpDirectory information for the current working directory
        /// </summary>
        /// <returns>A <see cref="FtpDirectoryInfo"/> with available details about the current working directory.</returns>
        public FtpDirectoryInfo GetCurrentDirectoryInfo()
        {
            string dir = GetCurrentDirectory();
            return new FtpDirectoryInfo(this, dir);
        }

        /// <summary>
        /// Gets the specified file's size
        /// </summary>
        /// <param name="file">The file to get the size for</param>
        /// <returns>The file size in bytes</returns>
        public long GetFileSize(string file)
        {
            IntPtr hFile = new IntPtr(
                WININET.FtpOpenFile(_hConnect, file, WINAPI.GENERIC_READ, WININET.FTP_TRANSFER_TYPE_BINARY, IntPtr.Zero)
            );

            if(hFile == IntPtr.Zero)
            {
                Error();
            }
            else
            {
                try
                {
                    int sizeHigh = 0;
                    int sizeLo = WININET.FtpGetFileSize(hFile, ref sizeHigh);

                    ulong fileSize = (ulong)(sizeHigh << 32) | (ulong)sizeLo;

                    return (long)fileSize;
                }
                catch (Exception)
                {
                    Error();
                    
                }
                finally
                {
                    WININET.InternetCloseHandle(hFile);
                }
            }

            return 0;
        }

        /// <summary>
        /// Downloads a file from the FTP server to the local system
        /// </summary>
        /// <remarks>The file will be downloaded to the local working directory with the same name it has on the FTP server.</remarks>
        /// <exception cref="FtpException">If the file does not exist.</exception>
        /// <param name="remoteFile">A <see cref="String"/> representing the full or relative path to the file to download.</param>
        /// <param name="failIfExists">A <see cref="Boolean"/> that determines whether an existing local file should be overwritten.</param>
        public void GetFile(string remoteFile, bool failIfExists)
        {
            GetFile(remoteFile, remoteFile, failIfExists);
        }

        /// <summary>
        /// Downloads a file from the FTP server to the local system
        /// </summary>
        /// <exception cref="FtpException">If the file does not exist.</exception>
        /// <param name="remoteFile">A <see cref="String"/> representing the full or relative path to the file to download.</param>
        /// <param name="localFile">A <see cref="String"/> representing the local file path to save the file.</param>
        /// <param name="failIfExists">A <see cref="Boolean"/> that determines whether an existing local file should be overwritten.</param>
        public void GetFile(string remoteFile, string localFile, bool failIfExists)
        {
            int ret = WININET.FtpGetFile(_hConnect,
                 remoteFile,
                 localFile,
                 failIfExists,
                 WINAPI.FILE_ATTRIBUTE_NORMAL,
                 WININET.FTP_TRANSFER_TYPE_BINARY,
                 IntPtr.Zero);

            if (ret == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"> representing the local file path to upload.</see></param>
        public void PutFile(string fileName)
        {
            PutFile(fileName, Path.GetFileName(fileName));
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"/> representing the local file path to upload.</param>
        /// <param name="localFile">A <see cref="String"/> representing the file path to save the file.</param>
        public void PutFile(string localFile, string remoteFile)
        {
            int ret = WININET.FtpPutFile(_hConnect,
                localFile,
                remoteFile,
                WININET.FTP_TRANSFER_TYPE_BINARY,
                IntPtr.Zero);

            if (ret == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Renames a file on the FTP server
        /// </summary>
        /// <param name="existingFile">A <see cref="String"/> representing the current file name</param>
        /// <param name="newFile">A <see cref="String"/> representing the new file name</param>
        public void RenameFile(string existingFile, string newFile)
        {
            int ret = WININET.FtpRenameFile(_hConnect, existingFile, newFile);

            if (ret == 0)
                Error();
        }

        /// <summary>
        /// Deletes a file from the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"/> representing the path of the file to delete.</param>
        public void RemoveFile(string fileName)
        {
            int ret = WININET.FtpDeleteFile(_hConnect, fileName);

            if (ret == 0)
            {
                Error();
            }
        }
        
        /// <summary>
        /// Deletes a directory from the FTP server
        /// </summary>
        /// <param name="directory">A <see cref="String"/> representing the path of the directory to delete.</param>
        public void RemoveDirectory(string directory)
        {
            int ret = WININET.FtpRemoveDirectory(_hConnect, directory);
            if (ret == 0)
                Error();
        }
       
        /// <summary>
        /// Gets details of all files and their available FTP file information from the current working FTP directory.
        /// </summary>
        /// <returns>A <see cref="FtpFileInfo[]"/> representing the files in the current working directory.</returns>
        public FtpFileInfo[] GetFiles()
        {
            return GetFiles(GetCurrentDirectory()); 
        }

        /// <summary>
        /// Gets details of all files and their available FTP file information from the current working FTP directory that match the file mask.
        /// </summary>
        /// <param name="mask">A <see cref="String"/> representing the file mask to match files.</param>
        /// <returns>A <see cref="FtpFileInfo[]"/> representing the files in the current working directory.</returns>
        public FtpFileInfo[] GetFiles(string mask) 
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                mask,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                List<FtpFileInfo> files = new List<FtpFileInfo>();
                if (hFindFile == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == WINAPI.ERROR_NO_MORE_FILES)
                    {
                        return files.ToArray();
                    }
                    else
                    {
                        Error();
                        return files.ToArray();
                    }
                }

                if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) != WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                {
                    var __temp = new string(findData.fileName).TrimEnd('\0');
                    __temp = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(__temp));

                    FtpFileInfo file = new FtpFileInfo(this, __temp.TrimEnd('\0'));
                    file.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                    file.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                    file.CreationTime = findData.ftCreationTime.ToDateTime();
                    file.Attributes = (FileAttributes)findData.dfFileAttributes;
                    files.Add(file);
                }

                findData = new WINAPI.WIN32_FIND_DATA();
                while (WININET.InternetFindNextFile(hFindFile, ref findData) != 0)
                {
                    if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) != WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                    {
                        var __temp = new string(findData.fileName).TrimEnd('\0');
                        __temp = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(__temp));

                        FtpFileInfo file = new FtpFileInfo(this, __temp.TrimEnd('\0'));
                        file.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                        file.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                        file.CreationTime = findData.ftCreationTime.ToDateTime();
                        file.Attributes = (FileAttributes)findData.dfFileAttributes;
                        files.Add(file);
                    }

                    findData = new WINAPI.WIN32_FIND_DATA();
                }

                if (Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                    Error();

                return files.ToArray();
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Gets details of all directories and their available FTP directory information from the current working FTP directory.
        /// </summary>
        /// <returns>A <see cref="FtpDirectoryInfo[]"/> representing the directories in the current working directory.</returns>
        public FtpDirectoryInfo[] GetDirectories()
        {
            return GetDirectories(this.GetCurrentDirectory());
        }

        /// <summary>
        /// Gets details of all directories and their available FTP directory information from the current working FTP directory that match the directory mask.
        /// </summary>
        /// <returns>A <see cref="FtpDirectoryInfo[]"/> representing the directories in the current working directory that match the mask.</returns>
        public FtpDirectoryInfo[] GetDirectories(string path) 
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();
            
            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                List<FtpDirectoryInfo> directories = new List<FtpDirectoryInfo>();

                if (hFindFile == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == WINAPI.ERROR_NO_MORE_FILES)
                    {
                        return directories.ToArray();
                    }
                    else
                    {
                        Error();
                        return directories.ToArray();
                    }
                }

                if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                {
                    var __temp = new string(findData.fileName).TrimEnd('\0');
                    __temp = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(__temp));
                    FtpDirectoryInfo dir = new FtpDirectoryInfo(this, __temp.TrimEnd('\0'));
                    dir.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                    dir.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                    dir.CreationTime = findData.ftCreationTime.ToDateTime();
                    dir.Attributes = (FileAttributes)findData.dfFileAttributes;
                    directories.Add(dir);
                }

                findData = new WINAPI.WIN32_FIND_DATA();

                while (WININET.InternetFindNextFile(hFindFile, ref findData) != 0)
                {
                    if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                    {
                        var __temp = new string(findData.fileName).TrimEnd('\0');
                        __temp = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(__temp));

                        FtpDirectoryInfo dir = new FtpDirectoryInfo(this, __temp.TrimEnd('\0'));
                        dir.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                        dir.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                        dir.CreationTime = findData.ftCreationTime.ToDateTime();
                        dir.Attributes = (FileAttributes)findData.dfFileAttributes;
                        directories.Add(dir);
                    }

                    findData = new WINAPI.WIN32_FIND_DATA();
                }

                if (Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                    Error();

                return directories.ToArray();
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Creates a directory on the FTP server.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the full or relative path of the directory to create.</param>
        public void CreateDirectory(string path)
        {
            if (WININET.FtpCreateDirectory(_hConnect, path) == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Checks if a directory exists.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the path to check.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether the directory exists.</returns>
        public bool DirectoryExists(string path)
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                if (hFindFile == IntPtr.Zero)
                {
                    return false;
                }

                return true;
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }

        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the path to check.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether the file exists.</returns>
        public bool FileExists(string path)
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                if (hFindFile == IntPtr.Zero)
                {
                    return false;
                }

                return true;
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Sends a command to the FTP server
        /// </summary>
        /// <param name="cmd">A <see cref="String"/> representing the command to send.</param>
        /// <returns>A <see cref="String"/> containing the server response.</returns>
        public string SendCommand(string cmd)
        {
            int result;
            IntPtr dataSocket = new IntPtr();
            switch(cmd)
            {
                case "PASV":
                    result = WININET.FtpCommand(_hConnect, false, WININET.FTP_TRANSFER_TYPE_ASCII, cmd, IntPtr.Zero, ref dataSocket);
                    break;
                default:
                    result = WININET.FtpCommand(_hConnect, false, WININET.FTP_TRANSFER_TYPE_ASCII, cmd, IntPtr.Zero, ref dataSocket);
                    break;
            }

            int BUFFER_SIZE = 8192;

            if(result == 0){
                Error();
            }
            else if(dataSocket != IntPtr.Zero)
            {
                StringBuilder buffer = new StringBuilder(BUFFER_SIZE);
                int bytesRead = 0;

                do
                {
                    result = WININET.InternetReadFile(dataSocket, buffer, BUFFER_SIZE, ref bytesRead);
                } while (result == 1 && bytesRead > 1);

                return buffer.ToString();
                
            }

            return "";
        }

        /// <summary>
        /// Closes the current FTP connection
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Retrieves error message text
        /// </summary>
        /// <param name="code">A <see cref="Int32"/> representing the system error code.</param>
        /// <returns>A <see cref="String"/> containing the error text.</returns>
        private string InternetLastResponseInfo(ref int code)
        {
            int BUFFER_SIZE = 8192;

            StringBuilder buff = new StringBuilder(BUFFER_SIZE);
            WININET.InternetGetLastResponseInfo(ref code, buff, ref BUFFER_SIZE);
            return buff.ToString();
        }

        /// <summary>
        /// Retrieves error text and throws an Exception.
        /// </summary>
        private void Error()
        {
            int code = Marshal.GetLastWin32Error();

            if (code == WININET.ERROR_INTERNET_EXTENDED_ERROR)
            {
                string errorText = InternetLastResponseInfo(ref code);
                throw new FtpException(code, errorText);
            }
            else
            {
                throw new Win32Exception(code);
            }
        }

        /// <summary>
        /// The Port used to connect
        /// </summary>
        public int Port
        {
            get { return _port; }
        }

        /// <summary>
        /// The host used to connect.
        /// </summary>
        public string Host
        {
            get { return _host; }
        }

        private IntPtr _hInternet;
        private IntPtr _hConnect;

        private string _host;
        private string _username = "";
        private string _password = "";
        private int _port = WININET.INTERNET_DEFAULT_FTP_PORT;

        private bool _disposed = false;

        #region IDisposable Members

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_hConnect != IntPtr.Zero)
                    WININET.InternetCloseHandle(_hConnect);

                if (_hInternet != IntPtr.Zero)
                    WININET.InternetCloseHandle(_hInternet);

                _hInternet = IntPtr.Zero;
                _hConnect = IntPtr.Zero;

                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        ~FtpClient()
        {
            Dispose();
        }
    }

    public class FtpDirectoryInfo : FileSystemInfo
    {
        public FtpDirectoryInfo(FtpClient ftp, string path)
        {
            _ftp = ftp;
            base.FullPath = path;
        }

        private FtpClient _ftp;
        private DateTime? _lastAccessTime;
        private DateTime? _creationTime;
        private DateTime? _lastWriteTime;
        private FileAttributes _attribues;

        public FtpClient FtpConnection { get { return _ftp; } }

        public new DateTime? LastAccessTime
        {
            get { return _lastAccessTime.HasValue ? (DateTime?)_lastAccessTime.Value : null; }
            internal set { _lastAccessTime = value; }
        }
        public new DateTime? CreationTime
        {
            get { return _creationTime.HasValue ? (DateTime?)_creationTime.Value : null; }
            internal set { _creationTime = value; }
        }
        public new DateTime? LastWriteTime
        {
            get { return _lastWriteTime.HasValue ? (DateTime?)_lastWriteTime.Value : null; }
            internal set { _lastWriteTime = value; }
        }

        public new DateTime? LastAccessTimeUtc
        {
            get { return _lastAccessTime.HasValue ? (DateTime?)_lastAccessTime.Value.ToUniversalTime() : null; }
        }
        public new DateTime? CreationTimeUtc
        {
            get { return _creationTime.HasValue ? (DateTime?)_creationTime.Value.ToUniversalTime() : null; }
        }
        public new DateTime? LastWriteTimeUtc
        {
            get { return _lastWriteTime.HasValue ? (DateTime?)_lastWriteTime.Value.ToUniversalTime() : null; }
        }

        public new FileAttributes Attributes
        {
            get { return _attribues; }
            internal set { _attribues = value; }
        }

        public override void Delete()
        {
            try
            {
                this._ftp.RemoveDirectory(Name);
            }
            catch (FtpException ex)
            {
                throw new Exception("Unable to delete directory.", ex);
            }
        }

        public override bool Exists
        {
            get { return this.FtpConnection.DirectoryExists(this.FullName); }
        }

        public override string Name
        {
            get { return Path.GetFileName(this.FullPath); }
        }

        public FtpDirectoryInfo[] GetDirectories()
        {
            return this.FtpConnection.GetDirectories(this.FullPath);
        }

        public FtpDirectoryInfo[] GetDirectories(string path)
        {
            path = Path.Combine(this.FullPath, path);
            return this.FtpConnection.GetDirectories(path);
        }

        public FtpFileInfo[] GetFiles()
        {
            return this.GetFiles(this.FtpConnection.GetCurrentDirectory());
        }

        public FtpFileInfo[] GetFiles(string mask)
        {
            return this.FtpConnection.GetFiles(mask);
        }
    }

    public class FtpFileInfo : FileSystemInfo
    {
        public FtpFileInfo(FtpClient ftp, string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException("fileName");

            base.OriginalPath = filePath;
            base.FullPath = filePath;

            _filePath = filePath;
            _ftp = ftp;

            this._name = Path.GetFileName(filePath);
        }

        private FtpClient _ftp;
        private string _filePath;
        private string _name;

        private DateTime? _lastAccessTime;
        private DateTime? _creationTime;
        private DateTime? _lastWriteTime;
        private FileAttributes _attribues;

        public FtpClient FtpConnection
        {
            get { return _ftp; }
        }

        public new DateTime? LastAccessTime
        {
            get { return _lastAccessTime.HasValue ? (DateTime?)_lastAccessTime.Value : null; }
            internal set { _lastAccessTime = value; }
        }
        public new DateTime? CreationTime
        {
            get { return _creationTime.HasValue ? (DateTime?)_creationTime.Value : null; }
            internal set { _creationTime = value; }
        }
        public new DateTime? LastWriteTime
        {
            get { return _lastWriteTime.HasValue ? (DateTime?)_lastWriteTime.Value : null; }
            internal set { _lastWriteTime = value; }
        }

        public new DateTime? LastAccessTimeUtc
        {
            get { return _lastAccessTime.HasValue ? (DateTime?)_lastAccessTime.Value.ToUniversalTime() : null; }
        }
        public new DateTime? CreationTimeUtc
        {
            get { return _creationTime.HasValue ? (DateTime?)_creationTime.Value.ToUniversalTime() : null; }
        }
        public new DateTime? LastWriteTimeUtc
        {
            get { return _lastWriteTime.HasValue ? (DateTime?)_lastWriteTime.Value.ToUniversalTime() : null; }
        }

        public new FileAttributes Attributes
        {
            get { return _attribues; }
            internal set { _attribues = value; }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override void Delete()
        {
            this.FtpConnection.RemoveFile(this.FullName);
        }

        public override bool Exists
        {
            get { return this.FtpConnection.FileExists(this.FullName); }
        }
    }

    static class Extensions
    {
        public static DateTime? ToDateTime(this WINAPI.FILETIME time)
        {
            if (time.dwHighDateTime == 0 && time.dwLowDateTime == 0)
                return null;

            unchecked
            {
                uint low = (uint)time.dwLowDateTime;
                long ft = (((long)time.dwHighDateTime) << 32 | low);
                return DateTime.FromFileTimeUtc(ft);
            }
        }
    }

    class FtpException : Exception
    {
        public FtpException(int error, string message)
            : base(message)
        {
            _error = error;
        }

        private int _error;

        public int ErrorCode
        {
            get { return _error; }
        }
    }

    static class WINAPI
    {
        public const int MAX_PATH = 260;
        public const int NO_ERROR = 0;
        public const int FILE_ATTRIBUTE_NORMAL = 128;
        public const int FILE_ATTRIBUTE_DIRECTORY = 16;
        public const int ERROR_NO_MORE_FILES = 18;
        public const uint GENERIC_READ = 0x80000000;

        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            public int dwLowDateTime;
            public int dwHighDateTime;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA
        {
            public int dfFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PATH)]
            public char[] fileName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] alternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA1
        {
            public int dfFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PATH * 2)]
            public byte[] fileName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14 * 2)]
            public byte[] alternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct INTERNET_BUFFERS
        {
            public int dwStructSize;
            public IntPtr Next;
            public string Header;
            public int dwHeadersLength;
            public int dwHeadersTotal;
            public IntPtr lpvBuffer;
            public int dwBufferLength;
            public int dwBufferTotal;
            public int dwOffsetLow;
            public int dwOffsetHigh;
        }



        public static string TranslateInternetError(uint errorCode)
        {
            IntPtr hModule = IntPtr.Zero;
            try
            {

                StringBuilder buf = new StringBuilder(255);
                hModule = LoadLibrary("wininet.dll");
                if (FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, hModule, errorCode, 0U, buf, (uint)buf.Capacity + 1, IntPtr.Zero) != 0)
                {
                    return buf.ToString();
                }
                else
                {
                    System.Diagnostics.Debug.Write("Error:: " + Marshal.GetLastWin32Error());
                    return string.Empty;
                }
            }
            catch
            {
                return "Unknown Error";
            }
            finally
            {
                FreeLibrary(hModule);
            }
        }

        private const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512;
        private const uint FORMAT_MESSAGE_FROM_HMODULE = 2048;
        private const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096;

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        private static extern int FreeLibrary(IntPtr hModule);

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        private static extern uint FormatMessage(uint dwFlags, System.IntPtr lpSource, uint dwMessageId, uint dwLanguageId, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPTStr)] System.Text.StringBuilder lpBuffer, uint nSize, System.IntPtr Arguments);

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        private static extern System.IntPtr LoadLibrary([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPTStr)] string lpLibFileName);


    }

    static class WININET
    {
        public const int INTERNET_SERVICE_FTP = 1;

        public const int INTERNET_OPEN_TYPE_PRECONFIG = 0;
        public const int INTERNET_OPEN_TYPE_DIRECT = 1;

        public const int INTERNET_DEFAULT_FTP_PORT = 21;

        public const int INTERNET_NO_CALLBACK = 0;

        public const int FTP_TRANSFER_TYPE_UNKNOWN = 0x00000000;
        public const int FTP_TRANSFER_TYPE_ASCII = 0x00000001;
        public const int FTP_TRANSFER_TYPE_BINARY = 0x00000002;

        public const int INTERNET_FLAG_HYPERLINK = 0x00000400;
        public const int INTERNET_FLAG_NEED_FILE = 0x00000010;
        public const int INTERNET_FLAG_NO_CACHE_WRITE = 0x04000000;
        public const int INTERNET_FLAG_RELOAD = 8;
        public const int INTERNET_FLAG_RESYNCHRONIZE = 0x00000800;

        public const int INTERNET_FLAG_ASYNC = 0x10000000;
        public const int INTERNET_FLAG_SYNC = 0x00000004;
        public const int INTERNET_FLAG_FROM_CACHE = 0x01000000;
        public const int INTERNET_FLAG_OFFLINE = 0x01000000;

        public const int INTERNET_FLAG_PASSIVE = 0x08000000;

        public const int INTERNET_ERROR_BASE = 12000;
        public const int ERROR_INTERNET_EXTENDED_ERROR = (INTERNET_ERROR_BASE + 3);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static IntPtr InternetOpen(
            [In] string agent,
            [In] int dwAccessType,
            [In] string proxyName,
            [In] string proxyBypass,
            [In] int dwFlags);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static IntPtr InternetConnect(
            [In] IntPtr hInternet,
            [In] string serverName,
            [In] int serverPort,
            [In] string userName,
            [In] string password,
            [In] int dwService,
            [In] int dwFlags,
            [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int InternetCloseHandle(
            [In] IntPtr hInternet);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpCommand(
            [In]  IntPtr hConnect,
            [In]  bool fExpectResponse,
            [In]  int dwFlags,
            [In]  string command,
            [In]  IntPtr dwContext,
            [In][Out]  ref IntPtr ftpCommand);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpCreateDirectory(
            [In] IntPtr hConnect,
            [In] string directory);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpDeleteFile(
            [In] IntPtr hConnect,
            [In] string fileName);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static IntPtr FtpFindFirstFile(
            [In] IntPtr hConnect,
            [In] string searchFile,
            [In][Out] ref WINAPI.WIN32_FIND_DATA findFileData,
            [In] int dwFlags,
            [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpGetCurrentDirectory(
            [In] IntPtr hConnect,
            [In][Out] StringBuilder currentDirectory,
            [In][Out] ref int dwCurrentDirectory); //specifies buffer length

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpGetFile(
            [In] IntPtr hConnect,
            [In] string remoteFile,
            [In] string newFile,
            [In] bool failIfExists,
            [In] int dwFlagsAndAttributes,
            [In] int dwFlags,
            [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpGetFileSize(
            [In] IntPtr hConnect,
            [In][Out] ref int dwFileSizeHigh);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpOpenFile(
            [In] IntPtr hConnect,
            [In] string fileName,
            [In] uint dwAccess,
            [In] int dwFlags,
            [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpPutFile(
            [In] IntPtr hConnect,
            [In] string localFile,
            [In] string newRemoteFile,
            [In] int dwFlags,
            [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpRemoveDirectory(
            [In] IntPtr hConnect,
            [In] string directory);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpRenameFile(
            [In] IntPtr hConnect,
            [In] string existingName,
            [In] string newName);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int FtpSetCurrentDirectory(
            [In] IntPtr hConnect,
            [In] string directory);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int InternetFindNextFile(
            [In] IntPtr hInternet,
            [In][Out] ref WINAPI.WIN32_FIND_DATA findData);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern public static int InternetGetLastResponseInfo(
            [In][Out] ref int dwError,
            [MarshalAs(UnmanagedType.LPTStr)]
            [Out] StringBuilder buffer,
            [In][Out] ref int bufferLength);

        [DllImport("wininet.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        extern public static int InternetReadFile(
            [In] IntPtr hConnect,
            [MarshalAs(UnmanagedType.LPTStr)]
            [In][Out] StringBuilder buffer,
            [In] int buffCount,
            [In][Out] ref int bytesRead);

        [DllImport("wininet.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        extern public static int InternetReadFileEx(
            [In] IntPtr hFile,
            [In][Out] ref WINAPI.INTERNET_BUFFERS lpBuffersOut,
            [In] int dwFlags,
            [In][Out] int dwContext);

    }

}
