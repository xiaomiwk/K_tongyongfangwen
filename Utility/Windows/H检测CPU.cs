using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Utility.Windows
{
    public static class H检测CPU
    {
        private static List<PerformanceCounter> _计数器 = new List<PerformanceCounter>();

        static H检测CPU()
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                _计数器.Add(new PerformanceCounter("Processor", "% Processor Time", i.ToString()));
            }
        }

        public static List<float> 获取CPU使用率()
        {
            return _计数器.Select(q => q.NextValue()).ToList();
        }
    }
}
