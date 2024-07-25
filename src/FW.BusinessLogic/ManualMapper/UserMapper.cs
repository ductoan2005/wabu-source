using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BusinessLogic.ManualMapper
{
    public static class UserMapper
    {
        public static UserProfile ConvertUserToUserProfile(Users user)
        {
            if (user == null)
            {
                return null;
            }

            var userProfileVM = new UserProfile()
            {
                UserID = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Authority = user.Authority,
                IsActive = user.IsActive,
                AvatarPath = user.AvatarPath,
                AvatarName = user.AvatarName
            };

            return userProfileVM;
        }

        public static UserProfile ConvertUserToUserProfile(UserProfile user)
        {
            if (user == null)
            {
                return null;
            }

            var userProfileVM = new UserProfile()
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FullName = user.FullName,
                Authority = user.Authority,
                IsActive = user.IsActive,
                AvatarPath = user.AvatarPath,
                AvatarName = user.AvatarName
            };

            return userProfileVM;
        }
    }
}
