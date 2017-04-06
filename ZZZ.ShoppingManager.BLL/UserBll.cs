using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZZZ.ShoppingManager.DAL;
using ZZZ.ShoppingManager.Model;

namespace ZZZ.ShoppingManager.BLL
{
    public class UserBll
    {
        public int Login(UserInfo info)
        {
            return new UserDal().Login(info);
        }
        public string RolesByNameAndPwd(UserInfo info)
        {
            return new UserDal().RolesByNameAndPwd(info);
        }
        public int Regin(UserInfo info)
        {
            return new UserDal().Regin(info);
        }
        public List<Roles> GetRoles()
        {
            return new UserDal().GetRoles();
        }
        public int SearchByUName(UserInfo info)
        {
            return new UserDal().SearchByUName(info);
        }
        public int SelectUserId(UserInfo info)
        {
            return new UserDal().SelectUserId(info);
        }
        public string SelectUserName(string UserName)
        {
            return new UserDal().SelectUserName(UserName);
        }
    }
}
