using FW.BusinessLogic.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.ViewModels;
using FW.Data.Infrastructure;
using FW.Models;
using AutoMapper;
using FW.Common.Helpers;
using FW.BusinessLogic.ManualMapper;
using FW.Common.TemplateEmail;
using System.Configuration;
using FW.Common.Utilities;

namespace FW.BusinessLogic.Implementations
{
    public class RegisterBL : BaseBL, IRegisterBL
    {
        private readonly IUserMasterRepository _iUserRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterBL(IUserMasterRepository iUserRepository, ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _iUserRepository = iUserRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        #region Create
        public async Task<LoginVM> CreateUser(LoginVM userMasterVM)
        {
            // map userMasterVM -> Users (VM -> model)
            var users = Mapper.Map<Users>(userMasterVM);

            var existUser = await _iUserRepository.GetAsync(x => (x.UserName == users.UserName || x.Email == users.Email) && x.IsDeleted != true).ConfigureAwait(false); //check user exist
            if (existUser == null)
            {
                users.IsDeleted = false;
                users.IsActive = true;
                string emailConfirmToken = TokenUtils.GenerateEmailConfirmationToken();
                users.EmailConfirmToken = emailConfirmToken;

                _iUserRepository.Add(users);
                // tra ve ket qua sau khi thao tac DB
                DbExecutionResult result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                CheckDbExecutionResultAndThrowIfAny(result);

                bool isSendEmailSuccess = await SendEmailCreateUser(users);
                if (!isSendEmailSuccess)
                {
                    userMasterVM.AddResultStatus = CommonConstants.STR_THREE;
                    return userMasterVM;
                }
            }
            else
            {
                if (existUser.IsDeleted == true)
                {
                    existUser.IsDeleted = false;
                    existUser.IsActive = true;
                    _iUserRepository.Update(existUser); // ton tai user name nhung bi xoa, update flag delete = false

                    // tra ve ket qua sau khi thao tac DB
                    DbExecutionResult result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                    CheckDbExecutionResultAndThrowIfAny(result);
                }
                else if (userMasterVM.Email.Equals(existUser.Email))
                {
                    userMasterVM.AddResultStatus = CommonConstants.STR_TWO;
                    return userMasterVM; // Email da dc dung
                }
                else if (existUser.IsDeleted == false)
                {
                    userMasterVM.AddResultStatus = CommonConstants.STR_ONE;
                    return userMasterVM; // ton tai username
                }
            }

            userMasterVM.Id = users.Id;
            userMasterVM.AddResultStatus = CommonConstants.STR_ZERO;
            return userMasterVM;
        }

        /// <summary>
        /// GetUserProfileByUserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns
        public async Task<UserProfile> GetUserProfileByUserName(string userName)
        {
            var accountDB = await _iUserRepository.GetAsync(x => x.UserName == userName && x.IsDeleted != true).ConfigureAwait(false);
            // mapper accountDB to userProfile
            return UserMapper.ConvertUserToUserProfile(accountDB);
        }

        public UserProfile LoginAfterRegister(LoginVM userMasterVM)
        {
            UserProfile account = new UserProfile();

            return account;
        }

        /// <summary>
        /// ConfirmEmailAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<UserProfile> ConfirmEmailAsync(string userId, string code)
        {
            var id = Convert.ToDouble(userId);
            var user = await _iUserRepository.GetAsync(x => x.Id == id && x.IsDeleted != true).ConfigureAwait(false);
            if (user.EmailConfirmToken == code)
            {
                user.EmailConfirmed = true;
                _iUserRepository.Update(user);
                // tra ve ket qua sau khi thao tac DB
                DbExecutionResult result = await _unitOfWork.CommitAsync().ConfigureAwait(false); ;
                CheckDbExecutionResultAndThrowIfAny(result);

                return UserMapper.ConvertUserToUserProfile(user);
            }

            return null;
        }

        public async Task<RegisterUserInformationVM> UpdateUserInformation(RegisterUserInformationVM registerUserInformationVM)
        {
            var user = await _iUserRepository.GetByIdAsync(registerUserInformationVM.Id);
            if (user != null)
            {
                ConvertRegisterUserInformationVMToUser(ref user, registerUserInformationVM);
                string emailConfirmToken = TokenUtils.GenerateEmailConfirmationToken();
                user.EmailConfirmToken = emailConfirmToken;
                bool isSendEmailSuccess = await SendEmailCreateUser(user);
                if (!isSendEmailSuccess)
                {
                    registerUserInformationVM.AddResultStatus = CommonConstants.STR_THREE;
                    return registerUserInformationVM;
                }

                _iUserRepository.Update(user);

                var company = new Company
                {
                    UserId = user.Id,
                    CompanyAddress = registerUserInformationVM.CompanyAddress,
                    CompanyName = registerUserInformationVM.CompanyName,
                    CompanyPhoneNumber = registerUserInformationVM.CompanyPhoneNumber
                };

                _companyRepository.Add(company);
                // tra ve ket qua sau khi thao tac DB
                DbExecutionResult result = await _unitOfWork.CommitAsync().ConfigureAwait(false); ;
                CheckDbExecutionResultAndThrowIfAny(result);

                registerUserInformationVM.AddResultStatus = CommonConstants.STR_ZERO;
                return registerUserInformationVM;
            }

            registerUserInformationVM.AddResultStatus = CommonConstants.STR_TWO;
            return registerUserInformationVM;
        }

        #endregion

        private void ConvertRegisterUserInformationVMToUser(ref Users users, RegisterUserInformationVM registerUserInformationVM)
        {
            users.FullName = registerUserInformationVM.FullName;
            users.Address = registerUserInformationVM.Address;
            users.PhoneNumber = registerUserInformationVM.PhoneNumber;
        }

        private async Task<bool> SendEmailCreateUser(Users users)
        {
            // send email
            string subject = ConfigurationManager.AppSettings["SubjectEmailRegisterAccount"];
            string htmlEmail = TemplateEmail.EmailConfirmRegister;
            htmlEmail = htmlEmail.Replace("{{username}}", users.UserName);
            htmlEmail = htmlEmail.Replace("{{email}}", users.Email);
            htmlEmail = htmlEmail.Replace("{{userId}}", users.Id.ToString());
            htmlEmail = htmlEmail.Replace("{{token}}", users.EmailConfirmToken);
            return await CommonEmails.SendEmailCompany(users.Email, htmlEmail, subject);
        }
    }
}
