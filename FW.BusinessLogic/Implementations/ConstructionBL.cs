using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.Services;
using FW.Common.Helpers;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Common.Utilities;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class ConstructionBL : BaseBL, IConstructionBL
    {
        private readonly IConstructionRepository _iContructionRepository;
        private readonly IBiddingNewsRepository _iBiddingNewsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        internal const string ORDER_BY_DEFAULT = "DateUpdated";

        public ConstructionBL(
                        IConstructionRepository iContructionRepository,
                        IBiddingNewsRepository iBiddingNewsRepository,
                        IAreaManageRepository iAreaRepository,
                        IUnitOfWork unitOfWork,
                        IAttachmentsToDOServices attachmentsToDOServices)
        {
            _iContructionRepository = iContructionRepository;
            _iBiddingNewsRepository = iBiddingNewsRepository;
            _unitOfWork = unitOfWork;
            _attachmentsToDOServices = attachmentsToDOServices;
        }

        #region Create

        public async Task AddNewConstruction(ConstructionVM contructionVm)
        {
            string format = "dd/MM/yyyy";

            if (!string.IsNullOrEmpty(contructionVm.BuildingPermitDateTime))
            {
                DateTime buildingPermitDate = DateTime.MinValue;
                DateTime.TryParseExact(contructionVm.BuildingPermitDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out buildingPermitDate);
                contructionVm.BuildingPermitDate = buildingPermitDate;
            }

            var contruction = Mapper.Map<Construction>(contructionVm);
            contruction.IsDeleted = false;

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _iContructionRepository.Add(contruction);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                var contructionEntity = await StoreConstructionImageToServer(contruction, contructionVm).ConfigureAwait(false);

                _iContructionRepository.Update(contructionEntity);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));
                transactionScope.Complete();
            }
        }

        #endregion

        #region Read

        public IEnumerable<ConstructionVM> GetAllConstructionHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            var contructionList = _iContructionRepository.GetAllConstructionHasPagingByUserId(paginationInfo, userId);
            var contructionListVM =
                Mapper.Map<IEnumerable<Construction>, IEnumerable<ConstructionVM>>(contructionList);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return contructionListVM;
        }

        public async Task<ConstructionVM> GetConstructionToEditById(long id)
        {
            ConstructionVM contructionVmDetail;
            var contructionDetail = await _iContructionRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            contructionVmDetail = contructionDetail.IsDeleted != true ? Mapper.Map<Construction, ConstructionVM>(contructionDetail) : null;

            if (contructionVmDetail == null)
                return new ConstructionVM();

            return contructionVmDetail;
        }

        public ConstructionVM GetConstructionDetailById(long idContraction)
        {
            ConstructionVM contructionVmDetail;
            var contructionDetail = _iContructionRepository.Get(x => x.Id == idContraction && x.IsDeleted != true);
            contructionVmDetail = contructionDetail.IsDeleted != true
                ? Mapper.Map<Construction, ConstructionVM>(contructionDetail) : null;

            return contructionVmDetail;
        }

        public string GetImageConstructionById(long? id)
        {
            return _iContructionRepository.GetImageConstructionById(id).Image1FilePath;
        }

        public IEnumerable<SelectListItem> GetSelectListConstruction(long? userId)
        {
            return _iContructionRepository.GetAll().Where(a => a.IsDeleted != true && a.UserId == userId)
                .Select(a =>
                    new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.ConstructionName.ToUpper()
                    }).Distinct();
        }

        public async Task<IEnumerable<ConstructionVM>> FilterConstructionByCondition(IPaginationInfo iPaginationInfo,
            UserProfile userProfile, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            var constructionList = await _iContructionRepository.FilterConstructionByCondition(paginationInfo, userProfile, condition, orderByStr).ConfigureAwait(false);

            var contructionVMList = Mapper.Map<IEnumerable<Construction>, IEnumerable<ConstructionVM>>(constructionList);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return contructionVMList;
        }

        #endregion

        #region Update

        public async Task UpdateConstruction(ConstructionVM contructionVm)
        {

            var dbCompany = await _iContructionRepository.GetAsync(x => x.Id == contructionVm.Id && x.IsDeleted != true);
            // check data is deleted
            if (dbCompany.IsDeleted == true)
            {
                throw new CommonExceptions(CommonResource.MSG_ERROR_RECORD_IS_DELETED);
            }

            // Update data
            string format = "dd/MM/yyyy";

            if (!string.IsNullOrEmpty(contructionVm.BuildingPermitDateTime))
            {
                DateTime buildingPermitDate = DateTime.MinValue;
                DateTime.TryParseExact(contructionVm.BuildingPermitDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out buildingPermitDate);
                contructionVm.BuildingPermitDate = buildingPermitDate;
            }

            dbCompany.AcreageBuild = contructionVm.AcreageBuild.Value;
            dbCompany.AddressBuild = contructionVm.AddressBuild;
            dbCompany.AreaId = contructionVm.AreaId;
            dbCompany.BuildingPermit = contructionVm.BuildingPermit;
            dbCompany.ConstructionDescription = contructionVm.ConstructionDescription;
            dbCompany.ConstructionName = contructionVm.ConstructionName.ToUpper();
            dbCompany.ContactEmail = contructionVm.ContactEmail;
            dbCompany.ContactName = contructionVm.ContactName;
            dbCompany.ContactPhoneNumber = contructionVm.ContactPhoneNumber;
            dbCompany.IsDisplayContact = contructionVm.IsDisplayContact;
            dbCompany.Scale = contructionVm.Scale;
            dbCompany.InvestorName = contructionVm.InvestorName;
            dbCompany.ConstructionForm = contructionVm.ConstructionForm;
            dbCompany.Basement = contructionVm.Basement;

            var contructionEntity = await UpdateConstructionImageToServer(dbCompany, contructionVm).ConfigureAwait(false);

            // excute update
            _iContructionRepository.Update(contructionEntity);

            // save data to database and verify data
            CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));
        }

        #endregion

        #region Delete

        public async Task DeleteConstructionById(long id)
        {
            var contruction = await _iContructionRepository.GetAsync(x => x.Id == id && x.IsDeleted != true); //check contruction exist
            if (contruction != null && !CheckConstructionHasBidding(id))
            {
                contruction.IsDeleted = true;
            }

            _iContructionRepository.Update(contruction);

            // tra ve ket qua sau khi thao tac DB
            CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));
        }

        #endregion

        public bool CheckConstructionHasBidding(long id)
        {
            return _iBiddingNewsRepository.Get(x => x.ConstructionId == id && x.IsDeleted != true) != null;
        }

        #region Private Method

        private async Task<Construction> StoreConstructionImageToServer(Construction construction, ConstructionVM contructionVm)
        {
            if (contructionVm.ImageFile1?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(contructionVm.ImageFile1);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                construction.Image1FileName = contructionVm.ImageFile1.FileName;
                construction.Image1FilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (contructionVm.ImageFile2?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(contructionVm.ImageFile2);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                construction.Image2FileName = contructionVm.ImageFile2.FileName;
                construction.Image2FilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (contructionVm.ImageFile3?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(contructionVm.ImageFile3);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                construction.Image3FileName = contructionVm.ImageFile3.FileName;
                construction.Image3FilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            return await Task.FromResult(construction);
        }

        private async Task<Construction> UpdateConstructionImageToServer(Construction construction, ConstructionVM contructionVm)
        {
            if (contructionVm.ImageFile1?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(contructionVm.ImageFile1);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                construction.Image1FileName = contructionVm.ImageFile1.FileName;
                construction.Image1FilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (contructionVm.ImageFile2?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(contructionVm.ImageFile2);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                construction.Image2FileName = contructionVm.ImageFile2.FileName;
                construction.Image2FilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (contructionVm.ImageFile3?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(contructionVm.ImageFile3);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                construction.Image3FileName = contructionVm.ImageFile3.FileName;
                construction.Image3FilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            return await Task.FromResult(construction);
        }

        private static string GetStoragePath(string constructionId)
        {
            return Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetConstructionFolderName, constructionId);
        }

        #endregion
    }
}
