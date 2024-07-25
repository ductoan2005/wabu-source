using System;
using System.Collections.Generic;
using FW.Models;

namespace FW.ViewModels.BiddingNews
{
    public class  BiddingNewsBidContractionDetailVM
    {
        public long? Id { get; set; }
        public long? BiddingPackageId { get; set; }
        public long? BiddingPackageOtherId { get; set; }
        public long? WorkContentId { get; set; }
        public long? WorkContentOtherId { get; set; }
        public long? ContructionId { get; set; }
        public decimal BudgetImplementation { get; set; }
        public string BiddingPackageDescription { get; set; }
        public long? ContractFormId { get; set; }
        public DateTime DurationContract { get; set; }
        public int NumberBidder { get; set; } // so luong thau duoc phep dau gia
        public int NumberBidded { get; set; } // so luong thau da dau gia cho tin thau
        public DateTime BidStartDate { get; set; }
        public DateTime BidCloseDate { get; set; }
        public byte ContractFormType { get; set; }
        public bool IsOwnerBiddingNews { get; set; }
        public byte StatusBiddingNews { get; set; }

        // Thông tin liên hệ
        public string NameContact { get; set; }
        public string EmailContact { get; set; }
        public string NumberPhoneContact { get; set; }
        public bool? IsDisplayContact { get; set; }

        // Tư cách hợp lệ nhà thầu.
        public bool? IsRegisEstablishmentTCHL { get; set; }
        public bool? IsFinancialTCHL { get; set; }
        public bool? IsDissolutionProcessTCHL { get; set; }
        public bool? IsBankruptTCHL { get; set; }

        // AbilityExp
        public int NumberYearActivityAbilityExp { get; set; }
        public int NumberSimilarContractAbilityExp { get; set; }
        public bool? IsContractAbilityExp { get; set; }
        public bool? IsLiquidationAbilityExp { get; set; }
        public bool? IsBuildingPermitAbilityExp { get; set; }

        // AbilityHR
        public bool? IsLaborContractAbilityHR { get; set; }
        public bool? IsDocumentRequestAbilityHR { get; set; }
        public bool? IsDecisionAbilityHR { get; set; }

        // AbilityFinance
        public int YearOfTurnoverAbilityFinance { get; set; }
        public long Turnover2YearAbilityFinance { get; set; }
        public bool? IsFinanceSituationAbilityFinance { get; set; }
        public int YearFinanceSituationAbilityFinance { get; set; }
        public bool? IsProtocolAbilityFinance { get; set; }
        public bool? IsDeclarationAbilityFinance { get; set; }
        public bool? IsDocumentAbilityFinance { get; set; }
        public bool? IsReportAbilityFinance { get; set; }

        // AbilityEquipment
        public bool? IsContractAbilityEquipment { get; set; }
        public bool? IsProfileAbilityEquipment { get; set; }

        // MKT
        public bool? IsProgressScheduleMKT { get; set; }
        public bool? IsQuotationMKT { get; set; }
        public bool? IsMaterialsUseMKT { get; set; }
        public bool? IsDrawingConstructionMKT { get; set; }
        public bool? IsWorkSafetyMKT { get; set; }
        public bool? IsEnvironmentalSanitationMKT { get; set; }
        public bool? IsFireProtectionMKT { get; set; }

        // Các tài liệu đính kèm
        public byte[] BuildingPermitTLDK { get; set; }
        public byte[] ConstructionDrawingsTLDK { get; set; }
        public byte[] VolumeEstimationTLDK { get; set; }
        public byte[] CertificateUseLandTLDK { get; set; }

        public string BuildingPermitTLDKFileName { get; set; }
        public string ConstructionDrawingsTLDKFileName { get; set; }
        public string VolumeEstimationTLDKFileName { get; set; }
        public string CertificateUseLandTLDKFileName { get; set; }

        public string ConstructionDrawingFilePath { get; set; }//Bản vẽ thi công
        public string EstimateVolumeFilePath { get; set; } //Bảng dự toán khối lượng
        public string RequireMaterialFilePath { get; set; } // Bảng vật tư yêu cầu  

        public string ConstructionDrawingFileName { get; set; }
        public string EstimateVolumeFileName { get; set; }
        public string RequireMaterialFileName { get; set; }

        public bool IsSelfMakeRequireMaterial { get; set; }
        public bool IsSelfMakeEstimateVolume { get; set; }

        public DateTime? NewsApprovalDate { get; set; }
        public Int16 NumberOfDaysImplement { get; set; }

        public BiddingPackage BiddingPackage { get; set; }
        public Construction Construction { get; set; }

        public List<BiddingNewsAbilityHR> BiddingNewsAbilityHRs { get; set; }
        public List<BiddingNewsAbilityEquipment> BiddingNewsAbilityEquipments { get; set; }
        public List<BiddingNewsTechnicalOther> BiddingNewsTechnicalOthers { get; set; }
        public List<BiddingDetail> BiddingDetails { get; set; }
        public string ContructionName { get; set; }
        public IEnumerable<CompanyProfile> CompanyProfile { get; set; }
        public List<BiddingNewsBookmark> BiddingNewsBookmarks { get; set; }
    }
}
