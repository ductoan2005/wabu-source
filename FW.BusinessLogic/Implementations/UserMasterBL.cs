using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.Services;
using FW.Common.Helpers;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Common.TemplateEmail;
using FW.Common.Utilities;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class UserMasterBL : BaseBL, IUserMasterBL
    {
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly IUserMasterRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        internal const string ORDER_BY_DEFAULT = "Id";

        private static string GetStoragePath(string id) => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetUserAvatarFolderName, id);


        public UserMasterBL(IUserMasterRepository userRepository,
                            IUnitOfWork unitOfWork,
                            ICompanyRepository iCompanyRepository,
                            IAttachmentsToDOServices attachmentsToDOServices)
        {
            _iCompanyRepository = iCompanyRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            _attachmentsToDOServices = attachmentsToDOServices;
        }

        #region Create
        public void CreateUser(UserMasterVM userMasterVM)
        {
            // map userMasterVM -> Users (VM -> model)
            var users = Mapper.Map<Users>(userMasterVM);

            var oldUser = userRepository.ReadUserByUserName(users.UserName, users.Email); //check user exist
            if (oldUser == null)
            {
                users.IsDeleted = false;
                userRepository.Add(users);
            }
            else
            {
                oldUser.IsDeleted = false;
                oldUser.IsActive = true;
                userRepository.Update(oldUser);
            }

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = unitOfWork.Commit();
            CheckDbExecutionResultAndThrowIfAny(result);
        }

        #endregion

        #region Update

        //If update in admin page is not send email to user
        //If update in admin page can update username and password
        public async Task<UserMasterVM> UpdateUser(UserMasterVM userMasterVM, bool isAdminUpdate = false)
        {
            //get exist user
            var user = userRepository.Get(x => x.Id == userMasterVM.Id && x.IsDeleted != true);

            if (user != null)
            {
                if (!string.IsNullOrEmpty(userMasterVM.Password) && !user.Password.Equals(userMasterVM.Password) && !isAdminUpdate)
                {
                    user.Password = userMasterVM.Password;
                    // send email
                    string subject = ConfigurationManager.AppSettings["SubjectEmailChangePassword"];
                    string htmlEmail = TemplateEmail.EmailChangePassword;
                    htmlEmail = htmlEmail.Replace("{{fullname}}", user.FullName);
                    await CommonEmails.SendEmailCompany(user.Email, htmlEmail, subject).ConfigureAwait(false);
                }

                if (isAdminUpdate)
                {
                    user.UserName = userMasterVM.UserName;
                    user.Password = userMasterVM.Password;
                    userMasterVM.EmailConfirmed = true;
                }

                //Check avatar is update
                if (userMasterVM.AvatarFile?.ContentLength > 0)
                {

                    var listIFormFile = new List<IFormFile>();

                    listIFormFile.Add(FileUtils.ConvertToIFormFile(userMasterVM.AvatarFile));
                    await _attachmentsToDOServices.DeleteAttachmentsToDO(listIFormFile.Select(x => x.FileName));
                    await _attachmentsToDOServices.UploadAttachmentsToDO(listIFormFile);

                    user.AvatarName = userMasterVM.AvatarFile.FileName;
                    user.AvatarPath = ConfigurationManager.AppSettings["AttachmentUrl"] + user.AvatarName;
                }

                user.FullName = userMasterVM.FullName;
                user.Email = userMasterVM.Email;
                user.Authority = userMasterVM.Authority;
                user.DateOfBirth = userMasterVM.DateOfBirth;
                user.CMND = userMasterVM.CMND;
                user.Gender = userMasterVM.Gender;
                user.PhoneNumber = userMasterVM.PhoneNumber;
                user.Address = userMasterVM.Address;
                user.IsActive = userMasterVM.IsActive;

                userRepository.Update(user);

                //If user is contractor
                if (userMasterVM.Authority == 3)
                {
                    var company = await _iCompanyRepository.GetByIdAsync(userMasterVM.CompanyId);
                    if (company != null)
                    {
                        UsersMasterVMToCompanyModel(ref company, userMasterVM);

                        //Check avatar is update
                        if (userMasterVM.AdvertisingBackgroundImageFile?.ContentLength > 0)
                        {
                            var listIFormFile = new List<IFormFile>();
                            listIFormFile.Add(FileUtils.ConvertToIFormFile(userMasterVM.AdvertisingBackgroundImageFile));
                            await _attachmentsToDOServices.DeleteAttachmentsToDO(listIFormFile.Select(x => x.FileName));
                            await _attachmentsToDOServices.UploadAttachmentsToDO(listIFormFile);

                            company.AdvertisingBackgroundImage = ConfigurationManager.AppSettings["AttachmentUrl"] + userMasterVM.AdvertisingBackgroundImageFile.FileName;
                        }

                        _iCompanyRepository.Update(company);
                    }
                }

                // tra ve ket qua sau khi thao tac DB
                DbExecutionResult result = await unitOfWork.CommitAsync().ConfigureAwait(false);
                CheckDbExecutionResultAndThrowIfAny(result);

                return new UserMasterVM
                {
                    AvatarPath = user.AvatarPath,
                    Authority = user.Authority
                };
            }

            return new UserMasterVM();
        }

        #endregion

        #region Read
        public IEnumerable<UserMasterVM> ReadAllUsers()
        {
            var companies = userRepository.GetAll();
            var companyMasterVMs = Mapper.Map<IEnumerable<Users>, IEnumerable<UserMasterVM>>(companies);
            return companyMasterVMs.ToList();
        }

        public List<UserMasterVM> ReadUsersToPagingByCondition(IPaginationInfo iPaginationInfo, int selectedTab, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            IEnumerable<Users> users = null;

            users = userRepository.ReadUsersToPagingByCondition(paginationInfo, selectedTab, condition, orderByStr);

            var userVMs = Mapper.Map<IEnumerable<Users>, IEnumerable<UserMasterVM>>(users);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return userVMs.ToList();
        }

        public List<UserMasterVM> ReadUsersToPagingAdmin(IPaginationInfo iPaginationInfo, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            IEnumerable<Users> users = null;

            users = userRepository.ReadUsersToPagingAdmin(paginationInfo, condition, orderByStr);

            var userVMs = Mapper.Map<IEnumerable<Users>, IEnumerable<UserMasterVM>>(users);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return userVMs.ToList();
        }

        public List<UserMasterVM> ReadUsersToPagingInvestor(IPaginationInfo iPaginationInfo, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            IEnumerable<Users> users = null;

            users = userRepository.ReadUsersToPagingInvestor(paginationInfo, condition, orderByStr);

            var userVMs = Mapper.Map<IEnumerable<Users>, IEnumerable<UserMasterVM>>(users);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return userVMs.ToList();
        }

        public List<UserMasterVM> ReadUsersToPagingContractors(IPaginationInfo iPaginationInfo, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            IEnumerable<Users> users = null;

            users = userRepository.ReadUsersToPagingContractors(paginationInfo, condition, orderByStr);

            var userVMs = Mapper.Map<IEnumerable<Users>, IEnumerable<UserMasterVM>>(users);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return userVMs.ToList();
        }

        public async Task<UserMasterVM> ReadUserById(long? id)
        {
            var user = await userRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            var userVM = Mapper.Map<UserMasterVM>(user);

            return userVM;
        }

        public async Task<CompanyVM> ReadCompanyByUserId(long? userId)
        {
            var companyInfo = await _iCompanyRepository.GetAsync(x => x.UserId == userId && x.IsDeleted != true);

            if (companyInfo != null)
            {
                var companyVM = Mapper.Map<CompanyVM>(companyInfo);
                return companyVM;
            }

            return new CompanyVM();
        }

        #endregion

        #region Update
        public void UpdateUserById(UserMasterVM userMasterVM)
        {
            var dbCompany = userRepository.Get(x => x.Id == userMasterVM.Id && x.IsDeleted != true);
            // check data is deleted
            if (dbCompany.IsDeleted == true)
            {
                throw new CommonExceptions(CommonResource.MSG_ERROR_NOT_EXIST_RECORD);
            }

            // check data is out of date
            if (dbCompany.DateUpdated != userMasterVM.DateUpdated)
            {
                throw new CommonExceptions(CommonResource.MSG_ERROR_RECORD_OUT_OF_DATE);
            }

            // map data to update
            Mapper.Map(userMasterVM, dbCompany);

            // excute update
            userRepository.Update(dbCompany);

            // save data to database and verify data
            DbExecutionResult result = unitOfWork.Commit();
            switch (result.ResultType)
            {
                case EDbExecutionResult.EntityNotExist:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_NOT_EXIST_RECORD);
                case EDbExecutionResult.CommonError:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_SYSTEM);
            }
        }
        #endregion

        #region Delete
        public void DeleteUserById(long? id)
        {
            var oldUser = userRepository.ReadUserById(id); //check user exist
            if (oldUser != null)
            {
                oldUser.IsDeleted = true;
            }

            userRepository.Update(oldUser);

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = unitOfWork.Commit();
            CheckDbExecutionResultAndThrowIfAny(result);
        }
        #endregion

        #region Private Func

        private void UsersMasterVMToCompanyModel(ref Company company, UserMasterVM usersMasterVM)
        {

            company.CompanyAddress = usersMasterVM.CompanyAddress;
            company.CompanyPhoneNumber = usersMasterVM.CompanyPhoneNumber;
            company.AdvertisingIsOn = usersMasterVM.AdvertisingIsOn;
            company.IsOnOver = usersMasterVM.AdvertisingIsOn ? (byte)1 : (byte)0;
            company.Id = usersMasterVM.CompanyId;
        }

        #endregion
    }
}
