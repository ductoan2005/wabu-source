using AutoMapper;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;
using FW.ViewModels.Home;

namespace WABU.Mapping
{
    public class MappingViewModelToModel : Profile
    {
        public override string ProfileName
        {
            get { return "MappingViewModelToModel"; }
        }

        public MappingViewModelToModel()
        {
            CreateMap<UserMasterVM, Users>();
            CreateMap<LoginVM, Users>();
            CreateMap<BiddingNewsVM, BiddingNews>();
            CreateMap<BiddingPackageVM, BiddingPackage>();
            CreateMap<ConstructionVM, Construction>();
            CreateMap<CompanyProfileVM, CompanyProfile>();

            CreateMap<CompanyAbilityExpVM, CompanyAbilityExp>()
                .ForMember(m => m.EvidenceContractFileName, opts => opts.Condition((src) => src.EvidenceContractFile != null))
                .ForMember(m => m.EvidenceContractFilePath, opts => opts.Condition((src) => src.EvidenceContractFile != null))
                .ForMember(m => m.EvidenceContractLiquidationFileName, opts => opts.Condition((src) => src.EvidenceContractLiquidationFile != null))
                .ForMember(m => m.EvidenceContractLiquidationFilePath, opts => opts.Condition((src) => src.EvidenceContractLiquidationFile != null))
                .ForMember(m => m.EvidenceBuildingPermitFileName, opts => opts.Condition((src) => src.EvidenceBuildingPermitFile != null))
                .ForMember(m => m.EvidenceBuildingPermitFilePath, opts => opts.Condition((src) => src.EvidenceBuildingPermitFile != null));

            CreateMap<CompanyAbilityHRVM, CompanyAbilityHR>()
                .ForMember(m => m.EvidenceAppointmentStaffFileName, opts => opts.Condition((src) => src.EvidenceAppointmentStaffFile != null))
                .ForMember(m => m.EvidenceAppointmentStaffFilePath, opts => opts.Condition((src) => src.EvidenceAppointmentStaffFile != null))
                .ForMember(m => m.EvidenceLaborContractFileName, opts => opts.Condition((src) => src.EvidenceLaborContractFile != null))
                .ForMember(m => m.EvidenceLaborContractFilePath, opts => opts.Condition((src) => src.EvidenceLaborContractFile != null))
                .ForMember(m => m.EvidenceSimilarCertificatesFileName, opts => opts.Condition((src) => src.EvidenceSimilarCertificatesFile != null))
                .ForMember(m => m.EvidenceSimilarCertificatesFilePath, opts => opts.Condition((src) => src.EvidenceSimilarCertificatesFile != null));
                
            CreateMap<CompanyAbilityEquipmentVM, CompanyAbilityEquipment>()
                .ForMember(m => m.EvidenceInspectionRecordsFileName, opts => opts.Condition((src) => src.EvidenceInspectionRecordsFile != null))
                .ForMember(m => m.EvidenceInspectionRecordsFilePath, opts => opts.Condition((src) => src.EvidenceInspectionRecordsFile != null))
                .ForMember(m => m.EvidenceSaleContractFileName, opts => opts.Condition((src) => src.EvidenceSaleContractFile != null))
                .ForMember(m => m.EvidenceSaleContractFilePath, opts => opts.Condition((src) => src.EvidenceSaleContractFile != null));

            CreateMap<CompanyProfileLogoVM, Company>();
            CreateMap<CompanyMasterVM, Company>();
            CreateMap<BiddingNewsDetailVM, BiddingDetail>();
            CreateMap<BiddingDetailVM, BiddingDetail>();
            CreateMap<CompanyAbilityHrDetailVM, CompanyAbilityHRDetail>();
            CreateMap<BiddingNewsBookmarkVM, BiddingNewsBookmark>();
            CreateMap<CompanyVM, Company>();
            CreateMap<NotificationVM, Notification>();

            CreateMap<CompanyAbilityFinanceVM, CompanyAbilityFinance>()
                .ForMember(m => m.EvidenceAuditReportFileName, opts => opts.Condition((src) => src.EvidenceAuditReportFile != null))
                .ForMember(m => m.EvidenceAuditReportFilePath, opts => opts.Condition((src) => src.EvidenceAuditReportFile != null))
                .ForMember(m => m.EvidenceCertificationTaxFileName, opts => opts.Condition((src) => src.EvidenceCertificationTaxFile != null))
                .ForMember(m => m.EvidenceCertificationTaxFilePath, opts => opts.Condition((src) => src.EvidenceCertificationTaxFile != null))
                .ForMember(m => m.EvidenceCheckSettlementFileName, opts => opts.Condition((src) => src.EvidenceCheckSettlementFile != null))
                .ForMember(m => m.EvidenceCheckSettlementFilePath, opts => opts.Condition((src) => src.EvidenceCheckSettlementFile != null))
                .ForMember(m => m.EvidenceDeclareTaxFileName, opts => opts.Condition((src) => src.EvidenceDeclareTaxFile != null))
                .ForMember(m => m.EvidenceDeclareTaxFilePath, opts => opts.Condition((src) => src.EvidenceDeclareTaxFile != null));
        }
    }
}