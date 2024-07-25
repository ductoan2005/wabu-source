using System;
using System.Collections.Generic;
using System.Web;

namespace FW.ViewModels.BiddingNewsRegistration
{
    public class BiddingNewsRegistrationVM
    {
        //common
        public long? ConstructionId { get; set; } // Công trình
        public long? UserId { get; set; } // Công trình

        public byte BiddingPackageType { get; set; } // Gói thầu
        public string BiddingPackageName { get; set; }

        public decimal BudgetImplementation { get; set; }
        
        public string BiddingPackageDescription { get; set; } // Mô tả gói thầu
        public byte ContractFormType { get; set; } // Hình thức hợp đồng

        public DateTime? DurationContract { get; set; } //Thời gian bắt đầu thực hiện
        public string DurationContractDateTime { get; set; }
        public int NumberBidder { get; set; } // so luong thau duoc phep dau gia
        public int NumberBidded { get; set; } // so luong thau da dau gia cho tin thau

        public DateTime? BidStartDate { get; set; }
        public DateTime? BidCloseDate { get; set; }
        public string BidStartDateTime { get; set; }
        public string BidCloseDateTime { get; set; }
        // Các tài liệu đính kèm
        public string ConstructionDrawingFilePath { get; set; }//Bản vẽ thi công
        public string EstimateVolumeFilePath { get; set; } //Bảng dự toán khối lượng
        public string RequireMaterialFilePath { get; set; } // Bảng vật tư yêu cầu  

        public string ConstructionDrawingFileName { get; set; }
        public string EstimateVolumeFileName { get; set; }
        public string RequireMaterialFileName { get; set; }

        public bool IsSelfMakeEstimateVolume { get; set; }
        public bool IsSelfMakeRequireMaterial { get; set; }

        public HttpPostedFileBase DrawingFile { get; set; }
        public HttpPostedFileBase EstimatesFile { get; set; }
        public HttpPostedFileBase MaterialFile { get; set; }

        //  Thông Tin Liên Hệ
        //public string NameContact { get; set; }
        //public string EmailContact { get; set; }
        //public string NumberPhoneContact { get; set; }

        // dataTCHL
        public bool? IsRegisEstablishment { get; set; } // Có đăng ký thành lập, hoạt động do cơ quan có thẩm quyền của nước mà nhà thầu, nhà đầu tư đang hoạt động cấp
        public bool? IsFinancial { get; set; } //Hạch toán tài chính độc lập
        public bool? IsDissolutionProcess { get; set; } //Không đang trong quá trình giải thể
        public bool? IsBankrupt { get; set; } //Không bị kết luận đang lâm vào tình trạng phá sản hoặc nợ không có khả năng chi trả theo quy định của pháp luật

        // dataNLKN
        public int NumberYearActivity { get; set; } //Số năm hoạt động trong lĩnh vực mời thầu
        public int NumberSimilarContract { get; set; } //Số lượng các hợp đồng tương tự đã thực hiện trong những năm qua
        public bool? IsContractNLKN { get; set; } //Hợp đồng
        public bool? IsLiquidation { get; set; } //Bản thanh lý hợp đồng hoặc biên bản nghiệm thu bàn giao công trình.
        public bool? IsBuildingPermit { get; set; } //Giấy phép xây dựng hoặc văn bản phê duyệt dự án

        // dataNLNS
        public bool? IsLaborContract { get; set; } //Hợp đồng lao động.
        public bool? IsDocumentRequest { get; set; } //Văn bằng yêu cầu
        public bool? IsDecision { get; set; } //Quyết định bổ nhiệm nhân sự (đối với những vị trí yêu cầu về số lượng công trình tương tự đã tham gia thực hiện).
        public List<BiddingNewsAbilityHRsVM> ListNLNS { get; set; }
        public string ListNLNSJson { get; set; }

        // dataNLTC
        public int NumYearOfTurnover { get; set; }// số năm doanh số
        public int Turnover2Year { get; set; } //doanh số vnđ
        public bool? IsFinanceSituation { get; set; } // check số năm tài chính
        public int NumYearFinanceSituation { get; set; }// số năm tài chính
        public bool? IsProtocol { get; set; } //Biên bản kiểm tra quyết toán của nhà thầu trong các năm tài chính nêu trên.
        public bool? IsDeclaration { get; set; } //Tờ khai tự quyết toán thuế
        public bool? IsDocument { get; set; } //Văn bản xác nhận của cơ quan quản lý thuế
        public bool? IsReport { get; set; } //Báo cáo kiểm toán.

        // dataNLMM
        public bool? IsContractNLMM { get; set; }
        public bool? IsProfile { get; set; }
        public List<BiddingNewsAbilityEquipmentsVM> ListNLMM { get; set; }
        public string ListNLMMJson { get; set; }

        // dataMKT
        public bool? IsProgressSchedule { get; set; }
        public bool? IsQuotation { get; set; }
        public bool? IsMaterialsUse { get; set; }
        public bool? IsDrawingConstruction { get; set; }
        public bool? IsWorkSafety { get; set; }
        public bool? IsEnvironmentalSanitation { get; set; }
        public bool? IsFireProtection { get; set; }
        public List<TechnicalOthersVM> ListMKT { get; set; }
        public string ListMKTJson { get; set; }

        public DateTime? NewsApprovalDate { get; set; }
        public Int16 NumberOfDaysImplement { get; set; }
    }
}
