using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZZZ.ShoppingManager.Model
{
    public class UserInfo
    {
        public int UserID { get; set; }//用户ID
        public string UName { get; set; }//用户账号
        public string UPwd { get; set; }//用户密码
        public int RId { get; set; }//角色名
    }
}
