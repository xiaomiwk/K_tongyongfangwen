using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Utility.存储
{
    public static class H程序配置
    {
        public static string 获取字符串(string __键)
        {
            if (HttpContext.Current == null)
            {
                return ConfigurationManager.AppSettings[__键] ?? string.Empty;
            }
            return WebConfigurationManager.AppSettings[__键] ?? string.Empty;
        }

        public static bool 获取Bool值(string __键)
        {
            bool __默认值;
            var __值 = HttpContext.Current == null ? ConfigurationManager.AppSettings[__键] : WebConfigurationManager.AppSettings[__键];
            bool.TryParse(__值, out __默认值);
            return __默认值;
        }

        public static int 获取Int32值(string __键)
        {
            int __默认值;
            var __值 = HttpContext.Current == null ? ConfigurationManager.AppSettings[__键] : WebConfigurationManager.AppSettings[__键];
            int.TryParse(__值, out __默认值);
            return __默认值;
        }

        public static void 设置(string __键, string __值)
        {
            Configuration __配置 = HttpContext.Current == null ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) : WebConfigurationManager.OpenWebConfiguration(null);
            __配置.AppSettings.Settings[__键].Value = __值;
            __配置.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void 设置(Dictionary<string, string> __键值对)
        {
            if (__键值对 == null || __键值对.Count == 0)
            {
                return;
            }
            Configuration __配置 = HttpContext.Current == null ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) : WebConfigurationManager.OpenWebConfiguration(null);
            foreach (var item in __键值对)
            {
                __配置.AppSettings.Settings[item.Key].Value = item.Value;
            }
            __配置.Save(ConfigurationSaveMode.Minimal);
            if (HttpContext.Current == null)
            {
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static bool 判断功能启用(string __功能)
        {
            return 获取字符串("功能配置").Contains(string.Format("|{0}:开启|", __功能));
        }

        public static bool 判断参数启用(string __参数)
        {
            return 获取字符串("参数配置").Contains(string.Format("|{0}:开启|", __参数));
        }

    }
}
