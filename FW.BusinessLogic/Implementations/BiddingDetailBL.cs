using FW.BusinessLogic.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FW.Common.Helpers;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;
using System.Transactions;
using FW.Common.Utilities;
using FW.Common.TemplateEmail;
using FW.Common.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;

namespace FW.BusinessLogic.Implementations
{
    public class BiddingDetailBL : BaseBL, IBiddingDetailBL
    {
        #region Constants

        private static readonly string[] _separators = { "," };
        private static readonly string _nullOptionValue = "<option value=''></option>";

        #endregion

        #region Fields
        private readonly ICompanyAbilityHrRepository _companyAbilityHrRepository;
        private readonly ICompanyAbilityEquipmentRepository _companyAbilityEquipmentRepository;
        private readonly ICompanyAbilityFinanceRepository _companyAbilityFinanceRepository;
        private readonly ICompanyAbilityExpRepository _companyAbilityExpRepository;
        private readonly IBiddingDetailFilesRepository _biddingDetailFilesRepository;
        private readonly IBiddingDetailRepository _biddingDetailRepository;
        private readonly IBiddingNewsRepository _biddingNewsRepository;
        private readonly ICompanyProfileRepository _companyProfileRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyBL _companyBL;
        private readonly ICompanyProfileBL _companyProfileBL;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public BiddingDetailBL(ICompanyAbilityHrRepository companyAbilityHrRepository, ICompanyAbilityEquipmentRepository companyAbilityEquipmentRepository,
            ICompanyAbilityFinanceRepository companyAbilityFinanceRepository, ICompanyAbilityExpRepository companyAbilityExpRepository,
            IBiddingDetailRepository biddingDetailRepository,
            IBiddingDetailFilesRepository biddingDetailFilesRepository,
            ICompanyProfileRepository companyProfileRepository,
            ICompanyRepository companyRepository,
            IUnitOfWork unitOfWork,
            ICompanyBL companyBL,
            ICompanyProfileBL companyProfileBL,
            IBiddingNewsRepository biddingNewsRepository,
            INotificationRepository notificationRepository
            )
        {
            _companyAbilityFinanceRepository = companyAbilityFinanceRepository;
            _companyAbilityEquipmentRepository = companyAbilityEquipmentRepository;
            _companyAbilityHrRepository = companyAbilityHrRepository;
            _companyAbilityExpRepository = companyAbilityExpRepository;
            _biddingDetailFilesRepository = biddingDetailFilesRepository;
            _biddingDetailRepository = biddingDetailRepository;
            _companyProfileRepository = companyProfileRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _companyBL = companyBL;
            _companyProfileBL = companyProfileBL;
            _biddingNewsRepository = biddingNewsRepository;
            _notificationRepository = notificationRepository;
        }

        #endregion

        #region Methods

        public async Task<JsonResult> ChooseContractorBidding(long? biddingNewsId, long? companyProfileId)
        {
            if (_biddingDetailRepository.CheckInvestorIsSelectContractor(biddingNewsId))
            {
                return new JsonResult
                {
                    Data = new
                    {
                        succeed = "1"
                    }
                };
            }
            else
            {
                var biddingDetail = await _biddingDetailRepository.GetByBiddingNewsAndComapanyProfile(biddingNewsId, companyProfileId);
                biddingDetail.InvestorSelected = true;
                biddingDetail.BiddingNews.DateInvestorSelected = DateTime.Now.AddDays(1);
                biddingDetail.BiddingNews.StatusBiddingNews = 2;

                // excute update
                _biddingDetailRepository.Update(biddingDetail);

                // save data to database and verify data
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                var emailVM = await _companyProfileRepository.GetUserRole3ByCompanyProfileId(companyProfileId, biddingNewsId);

                string subject = ConfigurationManager.AppSettings["SubjectEmailResult"];
                string htmlEmail = TemplateEmail.EmailResultBidding2;
                htmlEmail = htmlEmail.Replace("{{company}}", emailVM.CompanyName);
                htmlEmail = htmlEmail.Replace("{{ConstructionName}}", emailVM.ConstructionName.ToUpper());
                htmlEmail = htmlEmail.Replace("{{BiddingPackageName}}", emailVM.BiddingPackageName);
                htmlEmail = htmlEmail.Replace("{{BiddingNewsId}}", "WB" + emailVM.BiddingNewsId.Value.ToString("000000"));
                htmlEmail = htmlEmail.Replace("{{ContructionId}}", emailVM.contructionid.ToString());
                htmlEmail = htmlEmail.Replace("{{InvestorName}}", emailVM.InvestorName);
                htmlEmail = htmlEmail.Replace("{{name}}", emailVM.FullName);
                htmlEmail = htmlEmail.Replace("{{InvestorEmail}}", emailVM.InvestorEmail);
                htmlEmail = htmlEmail.Replace("{{InvestorPhone}}", emailVM.InvestorPhone);
                bool checksendemail = await CommonEmails.SendEmailCompany(emailVM.Email, htmlEmail, subject).ConfigureAwait(false);

                if (checksendemail)
                {
                    _notificationRepository.Add(new Notification()
                    {
                        Message = @"Hệ thống sẽ tự động gửi đến nhà thầu Thông báo trúng thầu và thông tin liên hệ của bạn. Nhà thầu sẽ liên hệ trực tiếp với bạn trong thời gian sớm nhất để tiến hành thương thảo hợp đồng.
                                    Quý khách vui lòng xác nhận lại đơn vị trúng thầu trên hệ thống của chúng tôi, sau khi hợp đồng được ký kết nhằm đảm bảo quyền lợi của quý khách.",
                        UserId = emailVM.UserIdRole2
                    });
                    CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                    return new JsonResult
                    {
                        Data = new
                        {
                            succeed = "0"
                        }
                    };
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new
                        {
                            succeed = "-1"
                        }
                    };
                }
            }
        }

        /// <summary>
        /// xác nhận hợp đồng
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        public async Task<JsonResult> ConfirmContractorBidding(long? biddingNewsId, long? companyProfileId)
        {
            var biddingDetail = await _biddingDetailRepository.GetByBiddingNewsAndComapanyProfile(biddingNewsId, companyProfileId);
            biddingDetail.BiddingNews.StatusBiddingNews = 3;

            var companyProfile = await _companyProfileRepository.GetByIdAsync(companyProfileId);
            var company = await _companyRepository.GetByIdAsync(companyProfile.CompanyId);
            company.ProjectImplemented = company.ProjectImplemented + 1;

            // excute update

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // excute update
                _biddingDetailRepository.Update(biddingDetail);
                _companyRepository.Update(company);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                transactionScope.Complete();
            }

            return new JsonResult
            {
                Data = new
                {
                    succeed = "0"
                }
            };
        }

        /// <summary>
        /// Hủy xác nhận hợp đồng
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        public async Task<JsonResult> CancelContractorBidding(long? biddingNewsId, long? companyProfileId)
        {
            var biddingDetail = await _biddingDetailRepository.GetByBiddingNewsAndComapanyProfile(biddingNewsId, companyProfileId);
            biddingDetail.InvestorSelected = false;
            biddingDetail.BiddingNews.StatusBiddingNews = 1;

            var companyProfile = await _companyProfileRepository.GetByIdAsync(companyProfileId);
            var company = await _companyRepository.GetByIdAsync(companyProfile.CompanyId);
            company.ProjectImplemented = company.ProjectImplemented - 1;

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // excute update
                _biddingDetailRepository.Update(biddingDetail);
                _companyRepository.Update(company);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                transactionScope.Complete();
            }

            return new JsonResult
            {
                Data = new
                {
                    succeed = "0"
                }
            };
        }

        /// <summary>
        /// nghiệm thu công trình
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        public async Task<JsonResult> CompletedContractorBidding(long? biddingNewsId)
        {
            var companyProfile = await _biddingDetailRepository.GetCompanyprofileByBiddingnewsId(biddingNewsId);
            var company = await _companyRepository.GetByIdAsync(companyProfile.CompanyProfile.CompanyId);
            company.ProjectsComplete = company.ProjectsComplete + 1;

            var biddingDetail = await _biddingDetailRepository.GetByBiddingNewsAndComapanyProfile(biddingNewsId, companyProfile.CompanyProfileId);
            biddingDetail.BiddingNews.StatusBiddingNews = 4;

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // excute update
                _biddingDetailRepository.Update(biddingDetail);
                _companyRepository.Update(company);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                transactionScope.Complete();
            }

            return new JsonResult
            {
                Data = new
                {
                    succeed = "0"
                }
            };
        }

        /// <summary>
        /// GetBiddingDetailToEditById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BiddingDetailVM> GetBiddingDetailToEditById(long? id, long? biddingNewsId, long? userId)
        {
            BiddingDetailVM biddingDetailVm;
            var biddingDetail = await _biddingDetailRepository.GetAsync(x => x.Id == id && x.IsDeleted != true).ConfigureAwait(false);
            biddingDetailVm = biddingDetail.IsDeleted != true ? Mapper.Map<BiddingDetail, BiddingDetailVM>(biddingDetail) : null;
            if (biddingDetailVm != null)
                biddingDetailVm.CompanyProfiles = (await _companyRepository.GetWithConditionAsync(x => x.UserId == userId)).CompanyProfiles;
            return biddingDetailVm;
        }

        /// <summary>
        /// Create bidding detail and detail files
        /// </summary>
        /// <param name="printInfoBiddingVm"></param>
        public async Task CreateBiddingDetail(PrintInfoBiddingVM printInfoBiddingVm)
        {
            var biddingDetail = new BiddingDetail
            {
                BiddingNewsId = printInfoBiddingVm.BiddingNewsId,
                Price = printInfoBiddingVm.Price,
                NumberOfDaysImplement = printInfoBiddingVm.NumberOfDaysImplement,
                CompanyProfileId = printInfoBiddingVm.CompanyProfileId,
                InvestorSelected = false,
                BiddingDate = DateTime.Now,
                IsDeleted = false
            };

            //get company by CompanyProfileId
            var companyProfile = await _companyProfileBL.GetCompanyProfileById(biddingDetail.CompanyProfileId);
            var company = await _companyRepository.GetByIdAsync(companyProfile.CompanyId);
            company.TotalBiddedNews = company.TotalBiddedNews + 1;

            //get BiddingNews
            var biddingNews = await _biddingNewsRepository.GetByIdAsync(biddingDetail.BiddingNewsId);
            biddingNews.NumberBidded = biddingNews.NumberBidded + 1;

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _biddingDetailRepository.Add(biddingDetail);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                var biddingDetailFiles = await StoreBiddingFilesToServer(printInfoBiddingVm, biddingDetail.Id.ToString()).ConfigureAwait(false);
                biddingDetailFiles.ForEach(x =>
                {
                    x.BiddingDetail = biddingDetail;
                    x.BiddingDetailId = biddingDetail.Id;
                });

                _biddingDetailFilesRepository.AddRange(biddingDetailFiles);
                _biddingNewsRepository.Update(biddingNews);
                _companyRepository.Update(company);
                CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// UpdateBiddingInfoDetail
        /// </summary>
        /// <param name="printInfoBiddingVm"></param>
        public async Task UpdateBiddingInfoDetail(PrintInfoBiddingVM printInfoBiddingVm)
        {
            var biddingDetail = await _biddingDetailRepository.GetBiddingNewsDetail(printInfoBiddingVm.Id);

            // Case just change profile but file uploaded is stil keep
            if (biddingDetail.CompanyProfileId != printInfoBiddingVm.CompanyProfileId)
            {
                if (biddingDetail.BiddingDetailFiles.Any())
                {
                    FileUtils.MoveDirectory(GetStoragePath(printInfoBiddingVm.CompanyName, biddingDetail.CompanyProfile.NameProfile, biddingDetail.Id.ToString()),
                    GetStoragePath(printInfoBiddingVm.CompanyName, printInfoBiddingVm.NameProfile, biddingDetail.Id.ToString()));
                    FileUtils.DeleteFileInDirectory(GetStoragePath(printInfoBiddingVm.CompanyName, biddingDetail.CompanyProfile.NameProfile, biddingDetail.Id.ToString()));
                    //Update path in DB
                    biddingDetail.BiddingDetailFiles.ToList().ForEach(x =>
                    {
                        x.FilePath = x.FilePath.Replace(biddingDetail.CompanyProfile.NameProfile, printInfoBiddingVm.NameProfile);
                    });
                }
                biddingDetail.CompanyProfileId = printInfoBiddingVm.CompanyProfileId;
            }

            biddingDetail.Price = printInfoBiddingVm.Price;
            biddingDetail.BiddingDetailFiles = await UpdateBiddingDetailFilesModel(biddingDetail.BiddingDetailFiles.ToList(), printInfoBiddingVm);
            biddingDetail.NumberOfDaysImplement = printInfoBiddingVm.NumberOfDaysImplement;
            _biddingDetailRepository.Update(biddingDetail);
            _biddingDetailFilesRepository.BulkUpdate(biddingDetail.BiddingDetailFiles.ToList());
            CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// DeleteBiddingInfoDetail
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteBiddingInfoDetail(long? id)
        {
            var biddingDetail = await _biddingDetailRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            biddingDetail.IsDeleted = true;
            if (biddingDetail.BiddingDetailFiles != null)
            {
                //Clear all file in folder
                FileUtils.DeleteFileInDirectory(GetStoragePath(biddingDetail.CompanyProfile.Company.CompanyName, biddingDetail.CompanyProfile.NameProfile, biddingDetail.Id.ToString()));
                biddingDetail.BiddingDetailFiles.ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                    x.BiddingDetail.BiddingNews.StatusBiddingNews = 1;
                    x.DateUpdated = DateTime.Now;
                });
            }

            var biddingNews = biddingDetail.BiddingNews;
            biddingNews.NumberBidded = biddingNews.NumberBidded - 1;

            _biddingDetailRepository.Update(biddingDetail);
            _biddingNewsRepository.Update(biddingNews);
            _biddingDetailFilesRepository.BulkUpdate(biddingDetail.BiddingDetailFiles.ToList());
            CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync());
        }

        public async Task<BiddingDetailVM> GetBiddingDetailByProfileId(long? biddingNewsId, long? companyProfileId)
        {
            BiddingDetailVM biddingDetailVm;
            var biddingDetail = await _biddingDetailRepository.GetWithConditionAsync(x => x.IsDeleted != true && x.BiddingNewsId == biddingNewsId && x.CompanyProfileId == companyProfileId).ConfigureAwait(false);
            if (biddingDetail == null)
            {
                return null;
            }

            biddingDetailVm = biddingDetail.IsDeleted != true ? Mapper.Map<BiddingDetail, BiddingDetailVM>(biddingDetail) : null;
            return biddingDetailVm;
        }

        public async Task<BiddingDetailVM> GetBiddingNewsById(long? biddingNewsId)
        {
            BiddingDetailVM biddingDetailVm;
            var biddingDetail = await _biddingDetailRepository.GetWithConditionAsync(x => x.IsDeleted != true && x.BiddingNewsId == biddingNewsId).ConfigureAwait(false);
            if (biddingDetail == null)
            {
                return null;
            }

            biddingDetailVm = Mapper.Map<BiddingDetail, BiddingDetailVM>(biddingDetail);
            return biddingDetailVm;
        }

        public string GetAllCompanyAbilityExpByListId(string listId)
        {
            if (string.IsNullOrEmpty(listId))
                return _nullOptionValue;

            var arrId = SplitStringWithSeparators(listId).Select(x => CommonMethods.ToNullableLong(x)).ToList();
            var listCompanyAbilityExp = _companyAbilityExpRepository.GetMany(x => arrId.Contains(x.Id) && x.IsDeleted != true);
            if (listCompanyAbilityExp != null)
            {
                var result = new StringBuilder(string.Empty);
                foreach (var companyAbilityExp in listCompanyAbilityExp)
                {
                    result.Append("<option value='" + companyAbilityExp.Id + "'>" + companyAbilityExp.ProjectName + "</option>");
                }

                return result.ToString();
            }

            return _nullOptionValue;
        }

        public string GetAllCompanyAbilityFinanceByListId(string listId)
        {
            if (string.IsNullOrEmpty(listId))
                return _nullOptionValue;

            var arrId = SplitStringWithSeparators(listId).Select(x => CommonMethods.ToNullableLong(x)).ToList();
            var listCompanyAbilityFinance = _companyAbilityFinanceRepository.GetMany(x => arrId.Contains(x.Id) && x.IsDeleted != true);
            if (listCompanyAbilityFinance != null)
            {
                var result = new StringBuilder(string.Empty);
                foreach (var companyAbilityFinance in listCompanyAbilityFinance)
                {
                    result.Append("<option value='" + companyAbilityFinance.Id + "'>" + companyAbilityFinance.YearDeclare + "</option>");
                }

                return result.ToString();
            }

            return _nullOptionValue;
        }

        public string GetAllCompanyAbilityHrsByListId(string listId)
        {
            if (string.IsNullOrEmpty(listId))
                return _nullOptionValue;

            var arrId = SplitStringWithSeparators(listId).Select(x => CommonMethods.ToNullableLong(x)).ToList();
            var listCompanyAbilityHrs = _companyAbilityHrRepository.GetMany(x => arrId.Contains(x.Id) && x.IsDeleted != true);
            if (listCompanyAbilityHrs != null)
            {
                var result = new StringBuilder(string.Empty);
                foreach (var companyAbilityHrs in listCompanyAbilityHrs)
                {
                    result.Append("<option value='" + companyAbilityHrs.Id + "'>" + companyAbilityHrs.FullName + "</option>");
                }

                return result.ToString();
            }

            return _nullOptionValue;
        }

        public string GetAllCompanyAbilityEquipmentByListId(string listId)
        {
            if (string.IsNullOrEmpty(listId))
                return _nullOptionValue;

            var arrId = SplitStringWithSeparators(listId).Select(x => CommonMethods.ToNullableLong(x)).ToList();
            var listCompanyAbilityEquipment = _companyAbilityEquipmentRepository.GetMany(x => arrId.Contains(x.Id) && x.IsDeleted != true);
            if (listCompanyAbilityEquipment != null)
            {
                var result = new StringBuilder(string.Empty);
                foreach (var companyAbilityEquipment in listCompanyAbilityEquipment)
                {
                    result.Append("<option value='" + companyAbilityEquipment.Id + "'>" + companyAbilityEquipment.EquipmentType + "</option>");
                }

                return result.ToString();
            }

            return _nullOptionValue;
        }

        #endregion

        #region Private Method

        /// <summary>
        /// CreateBiddingDetailFilesModel
        /// </summary>
        /// <param name="printInfoBidding"></param>
        /// <returns></returns>
        private static Task<List<BiddingDetailFiles>> UpdateBiddingDetailFilesModel(List<BiddingDetailFiles> biddingDetailFile, PrintInfoBiddingVM printInfoBidding)
        {
            if (printInfoBidding.FileAttachDrawingConstructionMKT != null)
            {
                var biddingFile = biddingDetailFile.FirstOrDefault(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.DrawingConstruction);
                FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFile.FilePath));
                biddingFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachDrawingConstructionMKT,
                  GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFile.BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                biddingFile.FileName = printInfoBidding.FileAttachDrawingConstructionMKT.FileName;
            }
            if (printInfoBidding.FileAttachEnvironmentalSanitationMKT != null)
            {
                var biddingFile = biddingDetailFile.FirstOrDefault(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.EnvironmentalSanitation);
                FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFile.FilePath));
                biddingFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachEnvironmentalSanitationMKT,
                  GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFile.BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                biddingFile.FileName = printInfoBidding.FileAttachEnvironmentalSanitationMKT.FileName;
            }
            if (printInfoBidding.FileAttachFireProtectionMKT != null)
            {
                var biddingFile = biddingDetailFile.FirstOrDefault(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.FireProtection);
                FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFile.FilePath));
                biddingFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachFireProtectionMKT,
                  GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFile.BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                biddingFile.FileName = printInfoBidding.FileAttachFireProtectionMKT.FileName;
            }
            if (printInfoBidding.FileAttachMaterialsUseMKT != null)
            {
                var biddingFile = biddingDetailFile.FirstOrDefault(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.MaterialsUse);
                FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFile.FilePath));
                biddingFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachMaterialsUseMKT,
                  GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFile.BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                biddingFile.FileName = printInfoBidding.FileAttachMaterialsUseMKT.FileName;
            }
            if (printInfoBidding.FileAttachProgressScheduleMKT != null)
            {
                var biddingFile = biddingDetailFile.FirstOrDefault(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.ProgressSchedule);
                FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFile.FilePath));
                biddingFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachProgressScheduleMKT,
                  GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFile.BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                biddingFile.FileName = printInfoBidding.FileAttachProgressScheduleMKT.FileName;
            }
            if (printInfoBidding.FileAttachQuotationMKT != null)
            {
                var biddingFile = biddingDetailFile.FirstOrDefault(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.Quotation);
                FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFile.FilePath));
                biddingFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachQuotationMKT,
                  GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFile.BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                biddingFile.FileName = printInfoBidding.FileAttachQuotationMKT.FileName;
            }
            if (printInfoBidding.FileAttachWorkSafetyMKT != null)
            {
                var biddingFile = biddingDetailFile.FirstOrDefault(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.WorkSafety);
                FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFile.FilePath));
                biddingFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachWorkSafetyMKT,
                  GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFile.BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                biddingFile.FileName = printInfoBidding.FileAttachWorkSafetyMKT.FileName;
            }
            if (printInfoBidding.OtherFiles?.Count > 0)
            {
                var biddingFiles = biddingDetailFile.Where(x => x.BiddingNewsFileType == (byte)EBiddingNewsFileType.Other).ToList();
                for (int i = 0; i < printInfoBidding.OtherFiles.Count; i++)
                {
                    if (printInfoBidding.OtherFiles[i] != null)
                    {
                        if (printInfoBidding.TechnicalRequirementNameList[i] == biddingFiles.ElementAt(i).BiddingNewsFileTypeName)
                        {
                            FileUtils.DeleteFileIfExists(StringUtils.GetAbsolutePath(biddingFiles.ElementAt(i).FilePath));
                            biddingFiles.ElementAt(i).FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.OtherFiles[i],
                                GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingFiles[i].BiddingDetailId.ToString())), FileUtils.GetDomainAppPathPath());
                            biddingFiles.ElementAt(i).FileName = printInfoBidding.OtherFiles[i].FileName;
                        }
                    }
                }
            }

            biddingDetailFile.ForEach(x =>
            {
                x.DateUpdated = DateTime.Now;
            });
            return Task.FromResult(biddingDetailFile);
        }

        /// <summary>
        /// Store Bidding Files To Server
        /// </summary>
        /// <param name="printInfoBidding"></param>
        /// <param name="biddingDetailId"></param>
        /// <returns></returns>
        private Task<List<BiddingDetailFiles>> StoreBiddingFilesToServer(PrintInfoBiddingVM printInfoBidding, string biddingDetailId)
        {
            var biddingDetailFiles = new List<BiddingDetailFiles>();

            //Clear all file in folder
            FileUtils.DeleteFileInDirectory(GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId));

            if (printInfoBidding.FileAttachDrawingConstructionMKT?.ContentLength > 0)
            {
                var biddingDetailFile = new BiddingDetailFiles();
                biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachDrawingConstructionMKT,
                    GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                biddingDetailFile.FileName = printInfoBidding.FileAttachDrawingConstructionMKT.FileName;
                biddingDetailFile.BiddingNewsFileTypeName = EBiddingNewsFileType.DrawingConstruction.ToString();
                biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.DrawingConstruction.GetHashCode();
                biddingDetailFiles.Add(biddingDetailFile);
            }

            if (printInfoBidding.FileAttachEnvironmentalSanitationMKT?.ContentLength > 0)
            {
                var biddingDetailFile = new BiddingDetailFiles();
                biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachEnvironmentalSanitationMKT,
                    GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                biddingDetailFile.FileName = printInfoBidding.FileAttachEnvironmentalSanitationMKT.FileName;
                biddingDetailFile.BiddingNewsFileTypeName = EBiddingNewsFileType.EnvironmentalSanitation.ToString();
                biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.EnvironmentalSanitation.GetHashCode();
                biddingDetailFiles.Add(biddingDetailFile);
            }

            if (printInfoBidding.FileAttachFireProtectionMKT?.ContentLength > 0)
            {
                var biddingDetailFile = new BiddingDetailFiles();
                biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachFireProtectionMKT,
                    GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                biddingDetailFile.FileName = printInfoBidding.FileAttachFireProtectionMKT.FileName;
                biddingDetailFile.BiddingNewsFileTypeName = EBiddingNewsFileType.FireProtection.ToString();
                biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.FireProtection.GetHashCode();
                biddingDetailFiles.Add(biddingDetailFile);
            }

            if (printInfoBidding.FileAttachMaterialsUseMKT?.ContentLength > 0)
            {
                var biddingDetailFile = new BiddingDetailFiles();
                biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachMaterialsUseMKT,
                    GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                biddingDetailFile.FileName = printInfoBidding.FileAttachMaterialsUseMKT.FileName;
                biddingDetailFile.BiddingNewsFileTypeName = EBiddingNewsFileType.MaterialsUse.ToString();
                biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.MaterialsUse.GetHashCode();
                biddingDetailFiles.Add(biddingDetailFile);
            }

            if (printInfoBidding.FileAttachProgressScheduleMKT?.ContentLength > 0)
            {
                var biddingDetailFile = new BiddingDetailFiles();
                biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachProgressScheduleMKT,
                    GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                biddingDetailFile.FileName = printInfoBidding.FileAttachProgressScheduleMKT.FileName;
                biddingDetailFile.BiddingNewsFileTypeName = EBiddingNewsFileType.ProgressSchedule.ToString();
                biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.ProgressSchedule.GetHashCode();
                biddingDetailFiles.Add(biddingDetailFile);
            }

            if (printInfoBidding.FileAttachQuotationMKT?.ContentLength > 0)
            {
                var biddingDetailFile = new BiddingDetailFiles();
                biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachQuotationMKT,
                    GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                biddingDetailFile.FileName = printInfoBidding.FileAttachQuotationMKT.FileName;
                biddingDetailFile.BiddingNewsFileTypeName = EBiddingNewsFileType.Quotation.ToString();
                biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.Quotation.GetHashCode();
                biddingDetailFiles.Add(biddingDetailFile);
            }

            if (printInfoBidding.FileAttachWorkSafetyMKT?.ContentLength > 0)
            {
                var biddingDetailFile = new BiddingDetailFiles();
                biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.FileAttachWorkSafetyMKT,
                    GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                biddingDetailFile.FileName = printInfoBidding.FileAttachWorkSafetyMKT.FileName;
                biddingDetailFile.BiddingNewsFileTypeName = EBiddingNewsFileType.WorkSafety.ToString();
                biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.WorkSafety.GetHashCode();
                biddingDetailFiles.Add(biddingDetailFile);
            }

            if (printInfoBidding.OtherFiles != null && printInfoBidding.OtherFiles.Count > 0)
            {
                for (int i = 0; i < printInfoBidding.OtherFiles.Count; i++)
                {
                    var biddingDetailFile = new BiddingDetailFiles();
                    biddingDetailFile.FilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(printInfoBidding.OtherFiles[i],
                        GetStoragePath(printInfoBidding.CompanyName, printInfoBidding.NameProfile, biddingDetailId)), FileUtils.GetDomainAppPathPath());
                    biddingDetailFile.FileName = printInfoBidding.OtherFiles[i].FileName;
                    biddingDetailFile.BiddingNewsFileTypeName = printInfoBidding.TechnicalRequirementNameList[i];
                    biddingDetailFile.TechnicalOtherId = printInfoBidding.TechnicalOtherIdList[i];
                    biddingDetailFile.BiddingNewsFileType = (byte)EBiddingNewsFileType.Other.GetHashCode();
                    biddingDetailFiles.Add(biddingDetailFile);
                }
            }

            return Task.FromResult(biddingDetailFiles);
        }

        private static string GetStoragePath(string companyName, string companyProfile, string biddingDetailId)
            => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetPrintBiddingFolderName, companyName, companyProfile, biddingDetailId);

        private static string[] SplitStringWithSeparators(string str)
            => str.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

        #endregion

    }
}
