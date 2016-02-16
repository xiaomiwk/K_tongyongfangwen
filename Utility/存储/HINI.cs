
using System.Runtime.InteropServices;
using System.Text;

namespace Utility.存储
{
	/// <summary>
    ///  读写INI文件
	/// </summary>
    public class HINI
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //[DllImport("kernel32.dll")]
        //public static extern int Beep(int dwFreq, int dwDuration);
 
        public static void Write(string __文件名, string __组, string __键, string __值)
        {
            var __绝对路径 = H路径.获取绝对路径(__文件名);

            WritePrivateProfileString(__组, __键, __值, __绝对路径);
        }
 
        public static string Read(string __文件名, string __组, string __键)
        {
            var __绝对路径 = H路径.获取绝对路径(__文件名);
            var temp = new StringBuilder(255);
            int i = GetPrivateProfileString(__组, __键, "", temp, 255, __绝对路径);
            if (i == 0)
            {
                return "";
            }
            return temp.ToString();
        }
    }
}