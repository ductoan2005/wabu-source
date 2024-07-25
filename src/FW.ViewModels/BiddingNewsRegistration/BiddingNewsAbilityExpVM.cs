using System;

namespace FW.ViewModels.BiddingNewsRegistration
{
    public class BiddingNewsAbilityExpVM
    {
        public long Id { get; set; }
        public string ProjectName { get; set; }
        public string InvestorName { get; set; }
        public string InvestorAddress { get; set; }
        public string InvestorPhoneNumber { get; set; }
        public string ContructionType { get; set; }
        public string ProjectScale { get; set; }
        public string ContractName { get; set; }
        public string ContractSignDate { get; set; }
        public string ContractCompleteDate { get; set; }
        public string ContractPrices { get; set; }
        public string ProjectDescription { get; set; }
        public string EvidenceContract { get; set; }
        public string EvidenceContractLiquidation { get; set; }
        public string EvidenceBuildingPermit { get; set; }
        public long CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateInserted { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
