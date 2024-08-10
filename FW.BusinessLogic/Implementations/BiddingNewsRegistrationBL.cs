using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.ManualMapper;
using FW.BusinessLogic.Services;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.BiddingNewsRegistration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class BiddingNewsRegistrationBL : BaseBL, IBiddingNewsRegistrationBL
    {
        #region Property
        private readonly IBiddingNewsRegistrationRepository _biddingNewsRegistrationRepository;
        private readonly IBiddingPackageRepository _biddingPackageRepository;
        private readonly IWorkContentRepository _workContentRepository;
        private readonly IContractFormRepository _contractFormRepository;
        private readonly IBiddingDetailRepository _biddingDetailRepository;
        private readonly IBiddingDetailFilesRepository _biddingDetailFilesRepository;
        private readonly IUserMasterRepository _userMasterRepository;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public BiddingNewsRegistrationBL(IBiddingNewsRegistrationRepository biddingNewsRegistrationRepository,
            IBiddingPackageRepository biddingPackageRepository,
            IWorkContentRepository workContentRepository,
            IContractFormRepository contractFormRepository,
            IBiddingDetailRepository biddingDetailRepository,
            IBiddingDetailFilesRepository biddingDetailFilesRepository,
            IUserMasterRepository userMasterRepository,
            IUnitOfWork unitOfWork,
            IAttachmentsToDOServices attachmentsToDOServices)
        {
            _biddingNewsRegistrationRepository = biddingNewsRegistrationRepository;
            _biddingPackageRepository = biddingPackageRepository;
            _workContentRepository = workContentRepository;
            _contractFormRepository = contractFormRepository;
            _biddingDetailRepository = biddingDetailRepository;
            _biddingDetailFilesRepository = biddingDetailFilesRepository;
            _unitOfWork = unitOfWork;
            _userMasterRepository = userMasterRepository;
            _attachmentsToDOServices = attachmentsToDOServices;
        }
        #endregion

        #region Method

        #region Create
        public async Task<JsonResult> CreateBiddingNewsReturnBiddingNewsId(BiddingNewsRegistrationVM biddingNewsRegistrationVM, UserProfile userProfile)
        {
            if (!_userMasterRepository.CheckUserInfoIsRegisCompleted(userProfile.UserID))
            {
                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_TWO,
                        message = CommonConstants.NEED_UPDATE_USER_INFO,
                        userId = userProfile.UserID
                    }
                };
            }

            // insert tbl_bidding_news (main data)
            // map biddingNewsRegistrationVM -> BiddingNews (VM -> model)
            var biddingNews = BiddingNewsRegistrationMapper.MappingBiddingNewsRegistrationVMToBiddingNews(biddingNewsRegistrationVM);
            biddingNews.UserId = userProfile.UserID;

            // tra ve ket qua sau khi thao tac DB

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _biddingNewsRegistrationRepository.Add(biddingNews);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                bool hasFile = false;
                if (biddingNewsRegistrationVM.DrawingFile != null)
                {
                    IFormFile iFormFile = FileUtils.ConvertToIFormFile(biddingNewsRegistrationVM.DrawingFile);
                    string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                    biddingNews.ConstructionDrawingFileName = biddingNewsRegistrationVM.DrawingFile.FileName;
                    biddingNews.ConstructionDrawingFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;

                    hasFile = true;
                }

                if (biddingNewsRegistrationVM.EstimatesFile != null)
                {
                    IFormFile iFormFile = FileUtils.ConvertToIFormFile(biddingNewsRegistrationVM.EstimatesFile);
                    string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                    biddingNews.EstimateVolumeFileName = biddingNewsRegistrationVM.EstimatesFile.FileName;
                    biddingNews.EstimateVolumeFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;

                    hasFile = true;
                }

                if (biddingNewsRegistrationVM.MaterialFile != null)
                {
                    IFormFile iFormFile = FileUtils.ConvertToIFormFile(biddingNewsRegistrationVM.MaterialFile);
                    string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                    biddingNews.RequireMaterialFileName = biddingNewsRegistrationVM.MaterialFile.FileName;
                    biddingNews.RequireMaterialFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;

                    hasFile = true;
                }

                if (hasFile)
                {
                    _biddingNewsRegistrationRepository.Update(biddingNews);
                    CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));
                }

                transactionScope.Complete();
            }

            return new JsonResult
            {
                Data = new
                {
                    code = CommonConstants.STR_ZERO,
                    message = CommonConstants.ADD_SUCCESS,
                    biddingNewsId = biddingNews.Id.Value
                }
            };
        }

        #endregion // create

        #region Read


        public IEnumerable<BiddingPackageVM> ReadAllBiddingPackage()
        {
            var biddingPackage = _biddingPackageRepository.GetAll();
            var biddingPackageVM = Mapper.Map<IEnumerable<BiddingPackage>, IEnumerable<BiddingPackageVM>>(biddingPackage);
            return biddingPackageVM;
        }

        public IEnumerable<WorkContentVM> ReadAllWorkContentByBiddingPackage(long? Id)
        {
            var workContent = _workContentRepository.ReadAllWorkContentByBiddingPackage(Id);
            var biddingPackageVM = Mapper.Map<IEnumerable<WorkContent>, IEnumerable<WorkContentVM>>(workContent);
            return biddingPackageVM;
        }

        public IEnumerable<ContractFormVM> ReadAllContractForm()
        {
            var contractForm = _contractFormRepository.GetAll();
            var contractFormVM = Mapper.Map<IEnumerable<ContractForm>, IEnumerable<ContractFormVM>>(contractForm);
            return contractFormVM;
        }
        #endregion // read

        #endregion // method

        private static string GetStoragePath(string companyName) => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetPrintBiddingFolderName, companyName);

    }
}
