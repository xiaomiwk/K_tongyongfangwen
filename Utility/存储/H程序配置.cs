using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Utility.存储
{
    public static class H程序配置
    {
        public static string 获取字符串(string key, string configFile = null)
        {
            Configuration config;
            if (configFile == null)
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            else
            {
                config = ConfigurationManager.OpenExeConfiguration(configFile);
            }
            return config.AppSettings.Settings.AllKeys.Contains(key) ? config.AppSettings.Settings[key].Value : string.Empty;
        }

        public static bool 获取Bool值(string key, string configFile = null)
        {
            bool temp;
            bool.TryParse(获取字符串(key, configFile), out temp);
            return temp;
        }

        public static int 获取Int32值(string key, string configFile = null)
        {
            int temp;
            int.TryParse(获取字符串(key, configFile), out temp);
            return temp;
        }

        public static void 设置(string key, string value, string configFile = null)
        {
            bool __外部 = configFile == null;
            Configuration config;
            if (configFile == null)
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            else
            {
                config = ConfigurationManager.OpenExeConfiguration(configFile);
            }
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Minimal);
            if (!__外部)
            {
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static void 设置(Dictionary<string, string> dic, string configFile = null)
        {
            bool __外部 = configFile == null;
            if (dic == null || dic.Count == 0)
            {
                return;
            }
            Configuration config;
            if (configFile == null)
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            else
            {
                config = ConfigurationManager.OpenExeConfiguration(configFile);
            }
            foreach (var item in dic)
            {
                config.AppSettings.Settings[item.Key].Value = item.Value;
            }
            config.Save(ConfigurationSaveMode.Minimal);
            if (!__外部)
            {
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static Dictionary<string, string> 查询键值对(string key, string configFile = null)
        {
            var __结果 = new Dictionary<string, string>();
            var temp = 获取字符串(key, configFile);
            if (string.IsNullOrEmpty(temp))
            {
                return __结果;
            }
            temp.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(q =>
            {
                var kv = q.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (kv.Length == 2)
                {
                    __结果[kv[0]] = kv[1];
                }
                if (kv.Length == 1)
                {
                    __结果[kv[0]] = "";
                }
            });
            return __结果;
        }

        public static void 设置键值对(string key, Dictionary<string, string> value, string configFile = null)
        {
            var temp = new StringBuilder();
            foreach (var item in value)
            {
                temp.AppendFormat("{0}:{1};", item.Key, item.Value);
            }
            if (temp.Length > 0)
            {
                temp = temp.Remove(temp.Length - 1, 1);
            }
            设置(key, temp.ToString(), configFile);
        }

    }
}
