using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Utility.通用;

namespace Utility.Windows
{
    public static class HMiniDump
    {
        // Taken almost verbatim from http://blog.kalmbach-software.de/2008/12/13/writing-minidumps-in-c/
        [Flags]
        public enum Option : uint
        {
            // From dbghelp.h:

            Normal = 0x00000000,

            WithDataSegs = 0x00000001,

            WithFullMemory = 0x00000002,

            WithHandleData = 0x00000004,

            FilterMemory = 0x00000008,

            ScanMemory = 0x00000010,

            WithUnloadedModules = 0x00000020,

            WithIndirectlyReferencedMemory = 0x00000040,

            FilterModulePaths = 0x00000080,

            WithProcessThreadData = 0x00000100,

            WithPrivateReadWriteMemory = 0x00000200,

            WithoutOptionalData = 0x00000400,

            WithFullMemoryInfo = 0x00000800,

            WithThreadInfo = 0x00001000,

            WithCodeSegs = 0x00002000,

            WithoutAuxiliaryState = 0x00004000,

            WithFullAuxiliaryState = 0x00008000,

            WithPrivateWriteCopyMemory = 0x00010000,

            IgnoreInaccessibleMemory = 0x00020000,

            ValidTypeFlags = 0x0003ffff,

        };

        enum ExceptionInfo
        {
            None,
            Present
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]  // Pack=4 is important! So it works also for x64!
        struct MiniDumpExceptionInformation
        {

            public uint ThreadId;

            public IntPtr ExceptionPointers;

            [MarshalAs(UnmanagedType.Bool)]

            public bool ClientPointers;

        }

        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        static extern uint GetCurrentThreadId();

        [DllImport("kernel32")]
        private static extern Int32 SetUnhandledExceptionFilter(处理异常 cb);

        /// <summary>
        /// EXCEPTION_EXECUTE_HANDLER == 1 表示我已经处理了异常,可以优雅地结束了 
        /// EXCEPTION_CONTINUE_SEARCH == 0 表示我不处理,其他人来吧,于是windows调用默认的处理程序显示一个错误框,并结束 
        /// EXCEPTION_CONTINUE_EXECUTION e== -1 表示错误已经被修复,请从异常发生处继续执行。
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static Int32 最后处理异常(ref long a)
        {
            var __文件名 = "未处理异常";
            if (!_获取是否DotNet异常())
            {
                H调试.记录致命("！！！跨平台(P/INVOKE)调用异常 ！！！");
                __文件名 = "跨平台调用异常";
            }
            __文件名 += DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");
            记录(string.Format("{0}\\{1}.dmp", H调试.日志目录, __文件名));
            H调试.截屏(__文件名);
            return 1;
        }

        delegate Int32 处理异常(ref long a);

        static bool Write(SafeHandle fileHandle, Option options, ExceptionInfo exceptionInfo)
        {
            Process currentProcess = Process.GetCurrentProcess();
            IntPtr currentProcessHandle = currentProcess.Handle;
            uint currentProcessId = (uint)currentProcess.Id;
            MiniDumpExceptionInformation exp;
            exp.ThreadId = GetCurrentThreadId();
            exp.ClientPointers = false;
            exp.ExceptionPointers = IntPtr.Zero;
            if (exceptionInfo == ExceptionInfo.Present)
            {
                exp.ExceptionPointers = Marshal.GetExceptionPointers();
            }
            bool bRet;
            if (exp.ExceptionPointers == IntPtr.Zero)
            {
                bRet = MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint)options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                bRet = MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint)options, ref exp, IntPtr.Zero, IntPtr.Zero);
            }
            return bRet;

        }

        public static bool 记录(string dmpPath, Option dumpType = Option.ScanMemory)
        {
            using (FileStream stream = new FileStream(dmpPath, FileMode.Create))
            {
                return Write(stream.SafeFileHandle, dumpType, ExceptionInfo.Present);
            }
        }

        private static Func<bool> _获取是否DotNet异常;

        public static void 自动记录(Func<bool> 获取是否DotNet异常)
        {
            _获取是否DotNet异常 = 获取是否DotNet异常;
            SetUnhandledExceptionFilter(最后处理异常);
        }
    }

}
