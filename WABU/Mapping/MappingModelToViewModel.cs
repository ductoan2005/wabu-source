using AutoMapper;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;
using FW.ViewModels.Home;

namespace WABU.Mapping
{
    public class MappingModelToViewModel : Profile
    {
        public override string ProfileName => "MappingModelToViewModel";

        public MappingModelToViewModel()
        {
            CreateMap<Users, UserMasterVM>();
            CreateMap<BiddingNews, BiddingNewsVM>();
            CreateMap<BiddingPackage, BiddingPackageVM>();
            CreateMap<WorkContent, WorkContentVM>();
            CreateMap<ContractForm, ContractFormVM>();
            CreateMap<Construction, ConstructionVM>();
            CreateMap<CompanyProfile, CompanyProfileVM>();
            CreateMap<CompanyAbilityExp, CompanyAbilityExpVM>();
            CreateMap<CompanyAbilityFinance, CompanyAbilityFinanceVM>();
            CreateMap<CompanyAbilityHR, CompanyAbilityHRVM>();
            CreateMap<CompanyAbilityEquipment, CompanyAbilityEquipmentVM>();
            CreateMap<Company, CompanyProfileLogoVM>();
            CreateMap<Company, CompanyMasterVM>();
            CreateMap<BiddingDetail, BiddingNewsDetailVM>();
            CreateMap<BiddingNews, BiddingNewsBidContractionDetailVM>();
            CreateMap<BiddingDetail, BiddingDetailVM>();
            CreateMap<BiddingNews, BiddingNewsVM>();
            CreateMap<CompanyAbilityHRDetail, CompanyAbilityHrDetailVM>();
            CreateMap<BiddingNewsBookmark, BiddingNewsBookmarkVM>();
            CreateMap<Company, CompanyVM>();
            CreateMap<Notification, NotificationVM>();
        }
    }
}