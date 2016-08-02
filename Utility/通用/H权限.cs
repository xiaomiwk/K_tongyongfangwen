using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Security;
using Utility.存储;

namespace Utility.通用
{
    [Serializable]
    public class M用户
    {
        public Guid 存储标识 { get; private set; }

        public string 帐号 { get; set; }

        public string 密码 { get; set; }

        public string 单位 { get; set; }

        public string 描述 { get; set; }

        public List<M角色> 角色列表 { get; set; }

        public M用户()
        {
            this.角色列表 = new List<M角色>(); 
            this.存储标识 = Guid.NewGuid();
        }
    }

    [Serializable]
    public class M角色
    {
        public Guid 存储标识 { get; private set; }

        public string 名称 { get; set; }

        public string 类别 { get; set; }

        public string 描述 { get; set; }

        public List<M权限> 权限列表 { get; set; }

        public M角色()
        {
            this.权限列表 = new List<M权限>();
            this.存储标识 = Guid.NewGuid();
        }
    }

    [Serializable]
    public class M权限
    {
        public Guid 存储标识 { get; private set; }

        public string 名称 { get; set; }

        public string 类别 { get; set; }

        public string 描述 { get; set; }

        public M权限()
        {
            this.存储标识 = Guid.NewGuid();
        }
    }

    public static class H权限
    {
        private static readonly string _文件路径;

        private static readonly Tuple<List<M角色>, List<M用户>> _所有数据;

        private static readonly object _同步对象 = new object();

        private static readonly List<M角色> __所有角色;

        private static readonly List<M用户> __所有用户;

        static H权限()
        {
            H调试.记录明细("构造函数");
            _文件路径 = "存储\\权限";
            _所有数据 = (Tuple<List<M角色>, List<M用户>>)H序列化.二进制读取(_文件路径) ??
                    new Tuple<List<M角色>, List<M用户>>(new List<M角色>(), new List<M用户>());
            __所有角色 = _所有数据.Item1;
            __所有用户 = _所有数据.Item2;
            if (__所有角色.Count == 0 || __所有用户.Count == 0)
            {
                H调试.记录明细("初始化");
                __所有角色.Clear();
                __所有用户.Clear();
                var __管理员角色 = new M角色 { 名称 = "管理员" };
                var __操作员角色 = new M角色 { 名称 = "操作员" };
                __所有角色.Add(__管理员角色);
                __所有角色.Add(__操作员角色);
                __所有用户.Add(new M用户 { 帐号 = "admin", 密码 = "admin", 角色列表 = new List<M角色> { __管理员角色 } });
                存储();
            }
        }

        static void 存储()
        {
            H序列化.二进制存储(_所有数据, _文件路径);
        }

        #region 管理
        public static List<M角色> 查询所有角色()
        {
            H调试.记录明细("查询所有角色");
            lock (_同步对象)
            {
                return __所有角色;
            }
        }

        public static M角色 查询角色(string 角色名)
        {
            H调试.记录明细("查询角色");
            lock (_同步对象)
            {
                return __所有角色.Find(q => q.名称 == 角色名);
            }
        }

        public static void 添加角色(M角色 角色)
        {
            H调试.记录明细("添加角色");
            lock (_同步对象)
            {
                __所有角色.Add(角色);
                存储();
            }
        }

        public static void 删除角色(M角色 角色)
        {
            H调试.记录明细("删除角色");
            lock (_同步对象)
            {
                __所有用户.ForEach(q => q.角色列表.RemoveAll(v => v.名称 == 角色.名称));
                __所有角色.Remove(角色);
                存储();
            }
        }

        public static void 修改角色(M角色 角色)
        {
            H调试.记录明细("角色");
            lock (_同步对象)
            {
                存储();
            }
        }

        public static List<M用户> 查询所有用户()
        {
            H调试.记录明细("查询所有用户");
            return __所有用户;
        }

        public static M用户 查询用户(string 用户名)
        {
            H调试.记录明细("查询用户");
            lock (_同步对象)
            {
                return __所有用户.Find(q => q.帐号 == 用户名);
            }
        }

        public static void 添加用户(M用户 用户)
        {
            H调试.记录明细("添加用户");
            lock (_同步对象)
            {
                用户.帐号 = 用户.帐号.Trim();
                用户.密码 = 用户.密码.Trim();
                if (用户.帐号.Length == 0)
                {
                    throw new SecurityException("请输入账号");
                }
                if (用户.密码.Length == 0)
                {
                    throw new SecurityException("请输入密码");
                }
                if (__所有用户.Exists(q => q.帐号 == 用户.帐号))
                {
                    throw new SecurityException("该账号已存在");
                }
                __所有用户.Add(用户);
                存储();
            }
        }

        public static void 删除用户(M用户 用户)
        {
            H调试.记录明细("删除用户");
            lock (_同步对象)
            {
                __所有用户.Remove(用户);
                存储();
            }
        }

        public static void 修改用户(M用户 用户)
        {
            H调试.记录明细("修改用户");
            lock (_同步对象)
            {
                存储();
            }
        }
        #endregion

        #region 应用
        public static void 登录(string 帐号, string 密码)
        {
            H调试.记录明细("登录");
            var 用户 = __所有用户.Find(q => q.帐号 == 帐号);
            if (用户 == null)
            {
                throw new SecurityException("帐号不存在");
            }
            if (用户.密码 != 密码)
            {
                throw new SecurityException("密码错误");
            }
            IIdentity identity = new GenericIdentity(用户.帐号);
            IPrincipal principal = new GenericPrincipal(identity, (from q in 用户.角色列表
                                                                   select q.名称).ToArray());
            Thread.CurrentPrincipal = principal;
        }

        public static void 注销()
        {
            H调试.记录明细("注销");
            Thread.CurrentPrincipal = null;
        }

        public static void 验证(string 权限)
        {
            H调试.记录明细("验证");
            var __用户 = 查询当前用户();
            var 包含权限 = __用户.角色列表.Exists(q => q.权限列表.Exists(v => v.名称 == 权限));
            if (!包含权限)
            {
                throw new SecurityException(string.Format("无{0}的权限", 权限));
            }
        }

        public static void 修改密码(string 原密码, string 新密码)
        {
            H调试.记录明细("修改密码");
            var __用户 = 查询当前用户();
            if (__用户.密码 == 原密码)
            {
                新密码 = 新密码.Trim();
                if(新密码.Length == 0)
                {
                    throw new SecurityException("请输入新密码");
                }
                __用户.密码 = 新密码;
                修改用户(__用户);
            }
            else
            {
                throw new SecurityException("原密码不正确");
            }
        }

        public static M用户 查询当前用户()
        {
            H调试.记录明细("查询当前用户");
            var principal = Thread.CurrentPrincipal as GenericPrincipal;
            if (principal == null)
            {
                throw new SecurityException("请先登录");
            }
            var __用户名 = principal.Identity.Name;
            return 查询用户(__用户名);
        }

        #endregion

    }
}
