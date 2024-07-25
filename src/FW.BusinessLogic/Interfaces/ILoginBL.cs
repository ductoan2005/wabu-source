using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.BusinessLogic.Interfaces
{
    public interface ILoginBL
    {
        string CheckAccount(LoginVM currentAccountLogin, Users accountDB);
        string CheckPassword(LoginVM currentAccountLogin, ref UserProfile account);
        UserProfile GetUserById(long? id);

        Task<bool> ForgotPassWord(string email);

        Task<bool> ChangePassword(UserMasterVM userVM);
    }
}
