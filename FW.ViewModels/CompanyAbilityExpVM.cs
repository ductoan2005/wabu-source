using System.Web;

namespace FW.ViewModels
{
    public class CompanyAbilityExpVM
    {
        public long? Id { get; set; }

        public string ProjectName { get; set; }
        public string InvestorName { get; set; }
        public string InvestorAddress { get; set; }
        public string InvestorPhoneNumber { get; set; }
        public string ContructionType { get; set; } //Loại, cấp công trình
        public string ProjectScale { get; set; } //Quy mô thực hiện
        public string ContractName { get; set; }
        public string ContractSignDate { get; set; }
        public string ContractCompleteDate { get; set; }
        public string ContractPrices { get; set; } //Giá hợp đồng
        public string ProjectDescription { get; set; }

        public string EvidenceContractFileName { get; set; }
        public string EvidenceContractLiquidationFileName { get; set; }
        public string EvidenceBuildingPermitFileName { get; set; }

        public string EvidenceContractFilePath { get; set; }
        public string EvidenceContractLiquidationFilePath { get; set; }
        public string EvidenceBuildingPermitFilePath { get; set; }

        public HttpPostedFileBase EvidenceContractFile { get; set; }
        public HttpPostedFileBase EvidenceContractLiquidationFile { get; set; }
        public HttpPostedFileBase EvidenceBuildingPermitFile { get; set; }

        public long? CompanyId { get; set; }
    }
}
