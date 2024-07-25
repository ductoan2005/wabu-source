using FW.Models;
using FW.ViewModels;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Interfaces
{
    public interface IRegisterBL
    {
        #region Create

        Task<LoginVM> CreateUser(LoginVM userMasterVM);

        #endregion

        #region Read

        UserProfile LoginAfterRegister(LoginVM userMasterVM);

        Task<UserProfile> GetUserProfileByUserName(string userName);

        #endregion

        #region Update

        Task<UserProfile> ConfirmEmailAsync(string userId, string code);

        Task<RegisterUserInformationVM> UpdateUserInformation(RegisterUserInformationVM registerUserInformationVM);

        #endregion

    }
}
