using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Utility.Windows
{
    public static class H注册表
    {
        /// <summary>
        /// 给所有类型的文件添加自定义的右键菜单
        /// HKEY_CLASSES_ROOT\*\shell  
        /// HKEY_CLASSES_ROOT\*\shell\自定义的菜单名
        /// HKEY_CLASSES_ROOT\*\shell\自定义的菜单名\command
        /// 值名称：(默认)     类型：REG_SZ      数据：关联程序的完全限定名称
        /// </summary>
        /// <param name="菜单名称"></param>
        /// <param name="执行文件路径">含目录与文件名</param>
        public static void 为所有文件添加操作系统右键菜单(string 菜单名称, string 执行文件路径)
        {
            //创建项：shell 
            RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true) ??
                                   Registry.ClassesRoot.CreateSubKey(@"*\shell");

            //创建项：右键显示的菜单名称
            RegistryKey rightCommondKey = shellKey.CreateSubKey(菜单名称);
            RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

            //创建默认值：关联的程序
            associatedProgramKey.SetValue(string.Empty, 执行文件路径 + " \"%1\"");

            //刷新到磁盘并释放资源
            associatedProgramKey.Close();
            rightCommondKey.Close();
            shellKey.Close();
        }

        public static bool 验证是否为所有文件添加操作系统右键菜单(string 菜单名称, string 执行文件路径)
        {
            try
            {
                return
                    Registry.ClassesRoot.OpenSubKey(string.Format(@"*\shell\{0}\command", 菜单名称))
                        .GetValue(string.Empty)
                        .ToString().Contains(执行文件路径);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 给所有文件夹添加自定义的右键菜单
        /// HKEY_CLASSES_ROOT\Directory\shell
        /// HKEY_CLASSES_ROOT\Directory\shell\自定义的菜单名  
        /// HKEY_CLASSES_ROOT\Directory\shell\自定义的菜单名\command
        /// 值名称：(默认)     类型：REG_SZ      数据：关联程序的完全限定名称
        /// </summary>
        /// <param name="菜单名称"></param>
        /// <param name="执行文件路径">含目录与文件名</param>
        public static void 为所有文件夹添加操作系统右键菜单(string 菜单名称, string 执行文件路径)
        {
            //创建项：shell 
            RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"directory\shell", true) ??
                                   Registry.ClassesRoot.CreateSubKey(@"directory\shell");

            //创建项：右键显示的菜单名称
            RegistryKey rightCommondKey = shellKey.CreateSubKey(菜单名称);
            RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

            //创建默认值：关联的程序
            associatedProgramKey.SetValue("", 执行文件路径 + " \"%1\"");

            //刷新到磁盘并释放资源
            associatedProgramKey.Close();
            rightCommondKey.Close();
            shellKey.Close();
        }

        public static bool 验证是否为所有文件夹添加操作系统右键菜单(string 菜单名称, string 执行文件路径)
        {
            try
            {
                return
                    Registry.ClassesRoot.OpenSubKey(string.Format(@"directory\shell\{0}\command", 菜单名称))
                        .GetValue(string.Empty)
                        .ToString().Contains(执行文件路径);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
