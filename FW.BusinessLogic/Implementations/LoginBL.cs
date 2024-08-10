using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.ManualMapper;
using FW.Common.Helpers;
using FW.Common.TemplateEmail;
using FW.Common.Utilities;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class LoginBL : BaseBL, ILoginBL
    {
        private readonly ILoginRepository loginRepository;

        private readonly ILoginHistoryRepository loginHisRepository;

        private readonly IUnitOfWork unitOfWork;

        private readonly ICompanyRepository companyRepository;

        public LoginBL(ILoginRepository loginRepository, ILoginHistoryRepository loginHistoryRepository, IUnitOfWork unitOfWork, ICompanyRepository companyRepository)
        {
            this.loginRepository = loginRepository;
            this.loginHisRepository = loginHistoryRepository;
            this.unitOfWork = unitOfWork;
            this.companyRepository = companyRepository;
        }

        public string CheckPassword(LoginVM currentAccountLogin, ref UserProfile userProfile)
        {
            try
            {
                // Get account info from database.
                Users accountDB = loginRepository.GetAccount(currentAccountLogin.UserName);

                // Check account info exist in database.
                string existAccount = this.CheckAccount(currentAccountLogin, accountDB);
                if (!CommonConstants.STR_ZERO.Equals(existAccount))
                {
                    return existAccount;
                }

                // mapper accountDB to userProfile
                userProfile = UserMapper.ConvertUserToUserProfile(accountDB);
                var companyProfile = companyRepository.ReadCompanyFromUserId(accountDB.Id).Result;
                if (companyProfile != null)
                {
                    userProfile.AvatarPath = companyProfile.Logo;
                }
                LoginHistory loginHistory = loginHisRepository.GetLoginHistory(accountDB.Id);
                var passwordDB = accountDB.Password;

                // Check Password input match with password in database.
                if (CommonConstants.STR_ZERO.Equals(existAccount) &&
                    passwordDB != currentAccountLogin.Password)
                {
                    if (loginHistory == null)
                    {
                        LoginHistory loginHistoryNew = new LoginHistory();
                        loginHistoryNew.Id = accountDB.Id;
                        loginHistoryNew.LoginFailedTimes = 1;
                        loginHistoryNew.LastLoginTime = DateTime.Now;
                        loginHistoryNew.FirstLoginFailedTime = DateTime.Now;
                        loginHisRepository.Add(loginHistoryNew);
                        unitOfWork.Commit();
                        return LoginMessageResource.ErrorValid;
                    }

                    if (loginHistory != null && loginHistory.LoginFailedTimes >= 0)
                    {
                        //TimeSpan span = DateTime.Now.Subtract(loginHistory.FirstLoginFailedTime.Value); //first login time
                        TimeSpan span2 = DateTime.Now.Subtract(loginHistory.LastLoginTime.Value); //last login time
                        // login fail 5 times -> lock account
                        if (loginHistory.LoginFailedTimes >= 5)
                        {
                            accountDB.IsActive = false;
                            loginRepository.Update(accountDB);
                            unitOfWork.Commit();

                            return CommonConstants.SCR_LOGIN_WARN_ID_5; //Username đăng nhập này đã bị khóa vì bạn đã đăng nhập sai 5 lần
                        }

                        // fisrt login time > 8h
                        //if (span.TotalMinutes > CommonSettings.PeriodAttemptLoginByMinutes * 96)
                        //{
                        //    loginHistory.LoginFailedTimes = 1;
                        //    loginHistory.FirstLoginFailedTime = DateTime.Now;
                        //    loginHisRepository.Update(loginHistory);
                        //    unitOfWork.Commit();
                        //}

                        //last login time > 5min
                        if (span2.TotalMinutes > CommonSettings.PeriodAttemptLoginByMinutes)
                        {
                            loginHistory.LoginFailedTimes = 1;
                            loginHistory.LastLoginTime = DateTime.Now;
                            loginHistory.FirstLoginFailedTime = DateTime.Now;
                            loginHisRepository.Update(loginHistory);
                            unitOfWork.Commit();
                            return CommonConstants.SCR_LOGIN_WARN_ID_1; //Username hoặc mật khẩu không đúng. Vui lòng thử lại
                        }
                        else
                        {
                            loginHistory.LoginFailedTimes = loginHistory.LoginFailedTimes + 1;
                            loginHistory.LastLoginTime = DateTime.Now;
                            loginHisRepository.Update(loginHistory);
                            unitOfWork.Commit();
                            return CommonConstants.SCR_LOGIN_WARN_ID_1; //Username hoặc mật khẩu không đúng. Vui lòng thử lại
                        }
                    }
                }
                // Password match return flag success.
                else if (CommonConstants.STR_ZERO.Equals(existAccount) &&
                    passwordDB == currentAccountLogin.Password)
                {
                    if (loginHistory != null)
                    {
                        loginHistory.LoginFailedTimes = 0;
                        loginHistory.FirstLoginFailedTime = DateTime.Now;
                        loginHisRepository.Update(loginHistory);
                        unitOfWork.Commit();
                    }
                    else
                    {
                        LoginHistory loginHistoryNew = new LoginHistory();
                        loginHistoryNew.Id = accountDB.Id;
                        loginHistoryNew.LoginFailedTimes = 0;
                        loginHistoryNew.LastLoginTime = DateTime.Now;
                        loginHistoryNew.FirstLoginFailedTime = DateTime.Now;
                        loginHisRepository.Add(loginHistoryNew);
                        unitOfWork.Commit();
                    }

                    return CommonConstants.STR_ZERO;
                }

                return CommonConstants.SCR_LOGIN_WARN_ID_1; //Username hoặc mật khẩu không đúng. Vui lòng thử lại
            }
            finally
            {
                //// Write log in end method.
            }

        }

        public string CheckAccount(LoginVM currentAccountLogin, Users accountDB)
        {
            try
            {
                // Check user valid
                if (currentAccountLogin.UserName.Equals(string.Empty))
                {
                    return CommonConstants.SCR_LOGIN_WARN_ID_1; //Username hoặc mật khẩu không đúng. Vui lòng thử lại
                }

                // Check account exist
                if (accountDB == null)
                {
                    return CommonConstants.SCR_LOGIN_WARN_ID_1_MINUS; //Account không tồn tại trong hệ thống
                }

                // Check account is lock
                if (accountDB.IsActive == false)
                {
                    return CommonConstants.SCR_LOGIN_WARN_ID_2; //Account đang không hoạt động
                }

                return CommonConstants.STR_ZERO;
            }
            catch (Exception)
            {
                //// Write log when it makes error.
                SysLogger.addTbActionLog(currentAccountLogin.UserName, "CheckAccount", "tbl_users");
                throw;
            }
            finally
            {
                //// Write log in end method.
            }
        }

        public UserProfile GetUserById(long? id)
        {
            //var user = loginRepository.GetById(id);
            var user = loginRepository.GetInfoAccount(id.Value);
            var userProfileVm = UserMapper.ConvertUserToUserProfile(user);

            return userProfileVm;
        }

        public async Task<bool> ForgotPassWord(string email)
        {
            var user = await loginRepository.GetAsync(x => x.Email == email && x.IsDeleted != true);
            if (user == null)
                return true;

            var newPassword = StringUtils.GenerateRandomPassWord();
            string emailConfirmToken = TokenUtils.GenerateEmailConfirmationToken();

            // send email
            string subject = ConfigurationManager.AppSettings["SubjectEmailForgotPassword"];
            string htmlEmail = TemplateEmail.EmailForgetPassword;
            htmlEmail = htmlEmail.Replace("{{fullname}}", user.FullName);
            htmlEmail = htmlEmail.Replace("{{password}}", newPassword);
            htmlEmail = htmlEmail.Replace("{{email}}", user.Email);
            htmlEmail = htmlEmail.Replace("{{token}}", emailConfirmToken);
            bool checksendemail = await CommonEmails.SendEmailCompany(user.Email, htmlEmail, subject).ConfigureAwait(false);
            if (checksendemail)
            {
                user.EmailConfirmToken = emailConfirmToken;
                user.Password = newPassword;
                user.PasswordChangedDate = DateTime.Now;
                loginRepository.Update(user);
                // tra ve ket qua sau khi thao tac DB
                DbExecutionResult result = await unitOfWork.CommitAsync().ConfigureAwait(false); ;
                CheckDbExecutionResultAndThrowIfAny(result);

                return true;
            }

            return false;
        }

        public async Task<bool> ChangePassword(UserMasterVM userVM)
        {
            var user = await loginRepository.GetAsync(x => x.Email == userVM.Email && x.IsDeleted != true);
            if (user == null || user.EmailConfirmToken != userVM.EmailConfirmToken)
                return false;

            user.Password = userVM.NewPassword;
            user.PasswordChangedDate = DateTime.Now;
            loginRepository.Update(user);

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await unitOfWork.CommitAsync().ConfigureAwait(false); ;
            CheckDbExecutionResultAndThrowIfAny(result);

            // send email
            string subject = ConfigurationManager.AppSettings["SubjectEmailChangePassword"];
            string htmlEmail = TemplateEmail.EmailChangePassword;
            htmlEmail = htmlEmail.Replace("{{fullname}}", user.FullName);
            await CommonEmails.SendEmailCompany(user.Email, htmlEmail, subject).ConfigureAwait(false);

            return true;
        }
    }
}
