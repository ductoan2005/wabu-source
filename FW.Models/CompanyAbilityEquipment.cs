using System;

namespace FW.Models
{
    [Serializable]
    public class CompanyAbilityEquipment : BaseEntity
    {
        public long? Id { get; set; }

        public string EquipmentType { get; set; }
        public int Quantity { get; set; }
        public string Capacity { get; set; } //Công suất
        public string Function { get; set; }
        public string NationalProduction { get; set; } //Nước sản xuất
        public string YearManufacture { get; set; } //Năm sản xuất
        public int QualityUse { get; set; } //Chất lượng sử dụng hiện nay
        public string Source { get; set; } //Nguồn gốc

        public string EvidenceSaleContractFilePath { get; set; } //Hợp đồng mua bán
        public string EvidenceInspectionRecordsFilePath { get; set; } //Hồ sơ kiểm định

        public string EvidenceSaleContractFileName { get; set; } //Hợp đồng mua bán
        public string EvidenceInspectionRecordsFileName { get; set; } //Hồ sơ kiểm định

        public bool IsBiddingOwner { get; set; }
        public bool IsBiddingHire { get; set; }
        public bool IsBiddingSpecialManufacture { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
