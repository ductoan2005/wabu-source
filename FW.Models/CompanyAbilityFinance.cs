using System;

namespace FW.Models
{
    [Serializable]
    public class CompanyAbilityFinance : BaseEntity
    {
        public long? Id { get; set; }

        public int YearDeclare { get; set; } // nam ke khai
        public int TotalAssets { get; set; } // Tổng tài sản 
        public int TotalLiabilities { get; set; } // Tổng nợ phải trả 
        public int ShortTermAssets { get; set; } // Tài sản ngắn hạn
        public int TotalCurrentLiabilities { get; set; } // Tổng nợ ngắn hạn
        public int Revenue { get; set; } // Doanh thu
        public int ProfitBeforeTax { get; set; } // Lợi nhuận trước thuế
        public int ProfitAfterTax { get; set; } // Lợi nhuận sau thuế 

        public string EvidenceCheckSettlementFilePath { get; set; } //Biên bản kiểm tra quyết toán
        public string EvidenceDeclareTaxFilePath { get; set; } //Tờ khai tự quyết toán thuế
        public string EvidenceCertificationTaxFilePath { get; set; } //Văn bản xác nhận của cơ quan quản lý thuế
        public string EvidenceAuditReportFilePath { get; set; } //Báo cáo kiểm toán.

        public string EvidenceCheckSettlementFileName { get; set; } //Biên bản kiểm tra quyết toán
        public string EvidenceDeclareTaxFileName { get; set; } //Tờ khai tự quyết toán thuế
        public string EvidenceCertificationTaxFileName { get; set; } //Văn bản xác nhận của cơ quan quản lý thuế
        public string EvidenceAuditReportFileName { get; set; } //Báo cáo kiểm toán.

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
