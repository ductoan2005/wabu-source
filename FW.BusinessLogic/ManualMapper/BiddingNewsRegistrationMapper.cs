using FW.Common.Enum;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Models;
using FW.ViewModels.BiddingNewsRegistration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace FW.BusinessLogic.ManualMapper
{
    public static class BiddingNewsRegistrationMapper
    {
        private static readonly string _dateTimeFormat = "dd/MM/yyyy";

        private static string GetBiddingPackageName(EBiddingPackageNameForCreateAndEdit value)
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                   as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }

        public static BiddingNews MappingBiddingNewsRegistrationVMToBiddingNews(BiddingNewsRegistrationVM biddingNewsRegistrationVM)
        {
            if (biddingNewsRegistrationVM == null)
            {
                return null;
            }

            var biddingNews = new BiddingNews();

            // info news
            biddingNews.ConstructionId = biddingNewsRegistrationVM.ConstructionId;

            biddingNews.BiddingPackage = new BiddingPackage();
            biddingNews.BiddingPackage.BiddingPackageType = biddingNewsRegistrationVM.BiddingPackageType;
            biddingNews.BiddingPackage.BiddingPackageName =
                string.IsNullOrEmpty(biddingNewsRegistrationVM.BiddingPackageName) ?
                GetBiddingPackageName((EBiddingPackageNameForCreateAndEdit)biddingNewsRegistrationVM.BiddingPackageType) : biddingNewsRegistrationVM.BiddingPackageName;

            biddingNews.BiddingPackage.DateInserted = biddingNews.BiddingPackage.DateUpdated = DateTime.Now;
            biddingNews.BiddingPackage.IsDeleted = false;

            biddingNews.ContractFormType = biddingNewsRegistrationVM.ContractFormType;
            biddingNews.BudgetImplementation = biddingNewsRegistrationVM.BudgetImplementation;
            biddingNews.BiddingPackageDescription = biddingNewsRegistrationVM.BiddingPackageDescription;
            biddingNews.NumberOfDaysImplement = biddingNewsRegistrationVM.NumberOfDaysImplement;

            //ngày mindate trong c# là: 01/01/0001 -> datetime2
            //ngày mindate trong sql là: 01/01/1753 -> datetime
            // vì vậy không dùng DateTime.MinValue để đại diện cho việc missing data của 1 property
            // thay vào đó việc dùng property Nullable<DateTime> (aka Datetime?) sẽ giải quyết triệt để được bài toán missing data(value null) cho field datetime


            if (!string.IsNullOrEmpty(biddingNewsRegistrationVM.DurationContractDateTime))
            {
                biddingNews.DurationContract = DateTimeUtils.ConvertStringToDataTime(biddingNewsRegistrationVM.DurationContractDateTime, _dateTimeFormat);
            }

            //biddingNews.BidStartDate = DateTimeUtils.ConvertStringToDataTime(biddingNewsRegistrationVM.BidStartDateTime, _dateTimeFormat);

            //biddingNews.BidCloseDate = DateTimeUtils.ConvertStringToDataTime(biddingNewsRegistrationVM.BidCloseDateTime, _dateTimeFormat);

            biddingNews.NumberBidder = 10;
            //biddingNews.NumberBidded = biddingNewsRegistrationVM.NumberBidded;

            biddingNews.StatusBiddingNews = 1;

            // Tài liệu đính kèm
            biddingNews.IsSelfMakeRequireMaterial = biddingNewsRegistrationVM.IsSelfMakeRequireMaterial;
            biddingNews.IsSelfMakeEstimateVolume = biddingNewsRegistrationVM.IsSelfMakeEstimateVolume;

            //Tư cách hợp lệ nhà thầu
            biddingNews.IsRegisEstablishmentTCHL = biddingNewsRegistrationVM.IsRegisEstablishment;
            biddingNews.IsFinancialTCHL = biddingNewsRegistrationVM.IsFinancial;
            biddingNews.IsDissolutionProcessTCHL = biddingNewsRegistrationVM.IsDissolutionProcess;
            biddingNews.IsBankruptTCHL = biddingNewsRegistrationVM.IsBankrupt;

            //Năng lực kinh nghiệm
            biddingNews.NumberYearActivityAbilityExp = biddingNewsRegistrationVM.NumberYearActivity;
            biddingNews.NumberSimilarContractAbilityExp = biddingNewsRegistrationVM.NumberSimilarContract;
            biddingNews.IsContractAbilityExp = biddingNewsRegistrationVM.IsContractNLKN;
            biddingNews.IsLiquidationAbilityExp = biddingNewsRegistrationVM.IsLiquidation;
            biddingNews.IsBuildingPermitAbilityExp = biddingNewsRegistrationVM.IsBuildingPermit;

            //Năng lực nhân sự
            biddingNewsRegistrationVM.ListNLNS = JsonConvert.DeserializeObject<List<BiddingNewsAbilityHRsVM>>(biddingNewsRegistrationVM.ListNLNSJson);
            if (biddingNewsRegistrationVM.ListNLNS != null && biddingNewsRegistrationVM.ListNLNS.Count > 0)
            {
                biddingNews.IsLaborContractAbilityHR = biddingNewsRegistrationVM.IsLaborContract;
                biddingNews.IsDocumentRequestAbilityHR = biddingNewsRegistrationVM.IsLaborContract;
                biddingNews.IsDecisionAbilityHR = biddingNewsRegistrationVM.IsLaborContract;

                biddingNews.BiddingNewsAbilityHRs = new List<BiddingNewsAbilityHR>();
                foreach (var item in biddingNewsRegistrationVM.ListNLNS)
                {
                    var biddingNewsAbilityHR = new BiddingNewsAbilityHR();
                    biddingNewsAbilityHR.JobPosition = item.JobPosition;
                    biddingNewsAbilityHR.QualificationRequired = item.QualificationRequired;
                    biddingNewsAbilityHR.NumberRequest = item.NumberRequest;
                    biddingNewsAbilityHR.YearExp = int.Parse(item.YearExp);
                    biddingNewsAbilityHR.SimilarProgram = item.SimilarProgram;

                    biddingNews.BiddingNewsAbilityHRs.Add(biddingNewsAbilityHR);
                }

            }

            //Năng lực tài chính
            biddingNews.YearOfTurnoverAbilityFinance = biddingNewsRegistrationVM.NumYearOfTurnover;
            biddingNews.Turnover2YearAbilityFinance = biddingNewsRegistrationVM.Turnover2Year;
            biddingNews.YearFinanceSituationAbilityFinance = biddingNewsRegistrationVM.NumYearFinanceSituation;
            biddingNews.IsProtocolAbilityFinance = biddingNewsRegistrationVM.IsProtocol;
            biddingNews.IsDeclarationAbilityFinance = biddingNewsRegistrationVM.IsDeclaration;
            biddingNews.IsDocumentAbilityFinance = biddingNewsRegistrationVM.IsDocument;
            biddingNews.IsReportAbilityFinance = biddingNewsRegistrationVM.IsReport;

            //Năng lực thiết bị, máy móc
            biddingNewsRegistrationVM.ListNLMM = JsonConvert.DeserializeObject<List<BiddingNewsAbilityEquipmentsVM>>(biddingNewsRegistrationVM.ListNLMMJson);
            if (biddingNewsRegistrationVM.ListNLMM != null && biddingNewsRegistrationVM.ListNLMM.Count > 0)
            {
                biddingNews.IsContractAbilityEquipment = biddingNewsRegistrationVM.IsContractNLMM;
                biddingNews.IsProfileAbilityEquipment = biddingNewsRegistrationVM.IsProfile;

                biddingNews.BiddingNewsAbilityEquipments = new List<BiddingNewsAbilityEquipment>();
                foreach (var item in biddingNewsRegistrationVM.ListNLMM)
                {
                    var biddingNewsAbilityEquipment = new BiddingNewsAbilityEquipment();
                    biddingNewsAbilityEquipment.EquipmentName = item.EquipmentName;
                    biddingNewsAbilityEquipment.IsAccreditation = item.IsAccreditation;
                    biddingNewsAbilityEquipment.QuantityEquipment = item.QuantityEquipment;
                    biddingNewsAbilityEquipment.PowerEquipment = item.PowerEquipment;

                    biddingNews.BiddingNewsAbilityEquipments.Add(biddingNewsAbilityEquipment);
                }
            }

            //Các tài liệu yêu cầu
            biddingNews.IsProgressScheduleMKT = biddingNewsRegistrationVM.IsProgressSchedule;
            biddingNews.IsQuotationMKT = biddingNewsRegistrationVM.IsQuotation;
            biddingNews.IsMaterialsUseMKT = biddingNewsRegistrationVM.IsMaterialsUse;
            biddingNews.IsDrawingConstructionMKT = biddingNewsRegistrationVM.IsDrawingConstruction;
            biddingNews.IsWorkSafetyMKT = biddingNewsRegistrationVM.IsWorkSafety;
            biddingNews.IsEnvironmentalSanitationMKT = biddingNewsRegistrationVM.IsEnvironmentalSanitation;
            biddingNews.IsFireProtectionMKT = biddingNewsRegistrationVM.IsFireProtection;

            biddingNewsRegistrationVM.ListMKT = JsonConvert.DeserializeObject<List<TechnicalOthersVM>>(biddingNewsRegistrationVM.ListMKTJson);
            if (biddingNewsRegistrationVM.ListMKT != null && biddingNewsRegistrationVM.ListMKT.Count > 0)
            {
                biddingNews.BiddingNewsTechnicalOthers = new List<BiddingNewsTechnicalOther>();
                foreach (var item in biddingNewsRegistrationVM.ListMKT)
                {
                    var biddingNewsTechnicalOther = new BiddingNewsTechnicalOther();
                    biddingNewsTechnicalOther.TechnicalRequirementName = item.TechnicalOtherName;
                    biddingNewsTechnicalOther.DateInserted = biddingNewsTechnicalOther.DateUpdated = DateTime.Now;
                    biddingNewsTechnicalOther.IsDeleted = false;

                    biddingNews.BiddingNewsTechnicalOthers.Add(biddingNewsTechnicalOther);
                }

            }

            biddingNews.NumberOfDaysImplement = biddingNewsRegistrationVM.NumberOfDaysImplement;

            return biddingNews;
        }

        private static string GetStoragePath(string biddingNewsId)
        {
            return Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetBiddingNewsFolderName, biddingNewsId);
        }

    }
}
