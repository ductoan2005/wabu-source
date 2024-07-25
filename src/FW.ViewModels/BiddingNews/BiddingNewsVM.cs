using System;
using System.Collections.Generic;
using FW.Common.Enum;
using FW.Models;
namespace FW.ViewModels
{
    public class BiddingNewsVM
    {
        public long? Id { get; set; }
        public string BiddingNewsCode { get; set; }
        public long? BiddingPackageId { get; set; }
        public byte BiddingPackageType { get; set; }
        public long? WorkContentId { get; set; }
        public long? WorkContentOtherId { get; set; }
        public long? ContructionId { get; set; }
        public decimal Budget { get; set; }
        public string BiddingPackageDescription { get; set; }
        public long? ContractFormId { get; set; }
        public DateTime DurationContract { get; set; }
        public int NumberBidder { get; set; }
        public int NumberBidded { get; set; }
        public bool? IsRegisEstablishment { get; set; }
        public bool? IsFinancial { get; set; }
        public bool? IsDissolutionProcess { get; set; }
        public bool? IsBuildingPermit { get; set; }
        public bool? IsConstructionDrawings { get; set; }
        public bool? IsVolumeEstimation { get; set; }
        public bool? IsCertificateUseLand { get; set; }
        public long? FileRequestId { get; set; }
        public string Image { get; set; }
        public string ContructionName { get; set; }
        public string BiddingPackageName { get; set; }
        public string InvestorName { get; set; }
        public byte StatusBiddingNews { get; set; } // tinh trang tin thau
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidCloseDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateInserted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public byte? Authority { get; set; }
        public StatusBidding EnumStatusBidding { get; set; }
        public EBiddingPackageName? EnumBiddingPackageName { get; set; }
        public long MaxTurnover2YearAbilityFinance { get; set; }
        public bool IsActived { get; set; } // tinh trang tin thau
        public byte ContractFormType { get; set; }

        // AbilityExp
        public int NumberYearActivityAbilityExp { get; set; }
        public int NumberSimilarContractAbilityExp { get; set; }

        public string ConstructionDrawingFilePath { get; set; }//Bản vẽ thi công
        public string EstimateVolumeFilePath { get; set; } //Bảng dự toán khối lượng
        public string RequireMaterialFilePath { get; set; } // Bảng vật tư yêu cầu  

        public string ConstructionDrawingFileName { get; set; }
        public string EstimateVolumeFileName { get; set; }
        public string RequireMaterialFileName { get; set; }
        public DateTime? NewsApprovalDate { get; set; }

        public ConstructionVM ConstructionVM { get; set; }
        public BiddingPackageVM BiddingPackageVM { get; set; }
        public BiddingPackage BiddingPackage { get; set; }
        public BiddingPackageOther BiddingPackageOther { get; set; }
        public Construction Construction { get; set; }
        public ContractForm ContractForm { get; set; }
        public WorkContent WorkContent { get; set; }
        public WorkContentOther WorkContentOther { get; set; }

        public List<BiddingNewsAbilityHR> BiddingNewsAbilityHRs { get; set; }
        public List<BiddingNewsAbilityEquipment> BiddingNewsAbilityEquipments { get; set; }
        public List<BiddingNewsTechnicalOther> BiddingNewsTechnicalOthers { get; set; }
        public List<BiddingDetail> BiddingDetails { get; set; }
        public List<BiddingNewsBookmark> BiddingNewsBookmarks { get; set; }
    }
}
