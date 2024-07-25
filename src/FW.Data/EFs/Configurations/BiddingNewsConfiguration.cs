using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingNewsConfiguration : CustomEntityTypeConfiguration<BiddingNews>
    {
        public BiddingNewsConfiguration() : base()
        {
            ToTable("tbl_bidding_news");
            EntityTypeComment("table tin dau thau");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "tin thau ID");

            //BiddingPackageId
            Property(p => p.BiddingPackageId);
            PropertyComment(p => p.BiddingPackageId, "id goi thau");

            //BiddingPackageId
            Property(p => p.UserId);
            PropertyComment(p => p.UserId, "id user");

            //ContructionId
            Property(p => p.ConstructionId);
            PropertyComment(p => p.ConstructionId, "id cong trinh");

            //BiddingPackageDescription
            Property(p => p.BiddingPackageDescription);
            PropertyComment(p => p.BiddingPackageDescription, "thong tin goi thau");

            //ContractFormType
            Property(p => p.ContractFormType);
            PropertyComment(p => p.ContractFormType, "hinh thuc hop dong");

            //DurationContract
            Property(p => p.DurationContract);
            PropertyComment(p => p.DurationContract, "thoi han hop dong");

            //NumberBidder
            Property(p => p.NumberBidder);
            PropertyComment(p => p.NumberBidder, "so luong nha dau thau");

            //NumberBidded
            Property(p => p.NumberBidded);

            //BidStartDate
            Property(p => p.BidStartDate);
            PropertyComment(p => p.BidStartDate, "ngay mo thau");

            //BidCloseDate
            Property(p => p.BidCloseDate);
            PropertyComment(p => p.BidCloseDate, "ngay dong thau");

            //Người liên hệ
            Property(p => p.NameContact);

            //Email
            Property(p => p.EmailContact);

            //Số điện thoại
            Property(p => p.NumberPhoneContact);

            // Hiện thông tin người liên hệ
            Property(p => p.IsDisplayContact);

            //IsRegisEstablishmentTCHL
            Property(p => p.IsRegisEstablishmentTCHL);

            //IsFinancialTCHL
            Property(p => p.IsFinancialTCHL);

            //IsDissolutionProcessTCHL
            Property(p => p.IsDissolutionProcessTCHL);

            //IsBankruptTCHL
            Property(p => p.IsBankruptTCHL);

            //NumberYearActivityAbilityExp
            Property(p => p.NumberYearActivityAbilityExp);

            //NumberSimilarContractAbilityExp
            Property(p => p.NumberSimilarContractAbilityExp);

            //IsContractAbilityExp
            Property(p => p.IsContractAbilityExp);

            //IsLiquidationAbilityExp
            Property(p => p.IsLiquidationAbilityExp);

            //IsBuildingPermitAbilityExp
            Property(p => p.IsBuildingPermitAbilityExp);

            //IsLaborContractAbilityHR
            Property(p => p.IsLaborContractAbilityHR);

            //IsDocumentRequestAbilityHR
            Property(p => p.IsDocumentRequestAbilityHR);

            //IsDecisionAbilityHR
            Property(p => p.IsDecisionAbilityHR);

            //Turnover2YearAbilityFinance
            Property(p => p.Turnover2YearAbilityFinance);

            //IsFinanceSituationAbilityFinance
            Property(p => p.IsFinanceSituationAbilityFinance);

            //IsProtocolAbilityFinance
            Property(p => p.IsProtocolAbilityFinance);

            //IsDeclarationAbilityFinance
            Property(p => p.IsDeclarationAbilityFinance);

            //IsDocumentAbilityFinance
            Property(p => p.IsDocumentAbilityFinance);

            //IsReportAbilityFinance
            Property(p => p.IsReportAbilityFinance);

            //IsContractAbilityEquipment
            Property(p => p.IsContractAbilityEquipment);

            //IsProfileAbilityEquipment
            Property(p => p.IsProfileAbilityEquipment);

            //IsProgressScheduleMKT
            Property(p => p.IsProgressScheduleMKT);

            //IsQuotationMKT
            Property(p => p.IsQuotationMKT);

            //IsMaterialsUseMKT
            Property(p => p.IsMaterialsUseMKT);

            //IsDrawingConstructionMKT
            Property(p => p.IsDrawingConstructionMKT);

            //IsWorkSafetyMKT
            Property(p => p.IsWorkSafetyMKT);

            //IsEnvironmentalSanitationMKT
            Property(p => p.IsEnvironmentalSanitationMKT);

            //IsFireProtectionMKT
            Property(p => p.IsFireProtectionMKT);

            Property(p => p.ConstructionDrawingFilePath);
            Property(p => p.EstimateVolumeFilePath);
            Property(p => p.RequireMaterialFilePath);

            Property(p => p.ConstructionDrawingFileName);
            Property(p => p.EstimateVolumeFileName);
            Property(p => p.RequireMaterialFileName);

            Property(p => p.IsSelfMakeRequireMaterial);
            Property(p => p.IsSelfMakeEstimateVolume);

            //StatusBiddingNews
            Property(p => p.StatusBiddingNews); // trang thai tin thau

            //IsActived
            Property(p => p.IsActived); // tinh thai tin thau

            //BudgetImplementation
            Property(p => p.BudgetImplementation); // ngan sach thuc hien
            
            //BudgetImplementation
            Property(p => p.DateInvestorSelected); // Ngày chủ đầu tư chọn nhà thầu, dùng countdown 24h kể từ khi chọn nhà thầu

            //NewsApprovalDate
            Property(p => p.NewsApprovalDate); // Ngay duyet tin thau

            //NumberOfDaysImplement
            Property(p => p.NumberOfDaysImplement); // So ngay thuc hien tin thau

            //Collection AbilityHRs
            HasMany(fr => fr.BiddingNewsAbilityHRs)
               .WithRequired(u => u.BiddingNews)
               .HasForeignKey(u => u.BiddingNewsId)
               .WillCascadeOnDelete(false);

            //Collection AbilityEquipment
            HasMany(fr => fr.BiddingNewsAbilityEquipments)
               .WithRequired(u => u.BiddingNews)
               .HasForeignKey(u => u.BiddingNewsId)
               .WillCascadeOnDelete(false);

            //Collection TechnicalOthers
            HasMany(fr => fr.BiddingNewsTechnicalOthers)
               .WithRequired(u => u.BiddingNews)
               .HasForeignKey(u => u.BiddingNewsId)
               .WillCascadeOnDelete(false);

            //Collection BiddingDetails
            HasMany(fr => fr.BiddingDetails)
               .WithRequired(u => u.BiddingNews)
               .HasForeignKey(u => u.BiddingNewsId)
               .WillCascadeOnDelete(false);

            //Collection BiddingNewsBookmark
            HasMany(fr => fr.BiddingNewsBookmarks)
               .WithRequired(u => u.BiddingNews)
               .HasForeignKey(u => u.BiddingNewsId)
               .WillCascadeOnDelete(false);
        }
    }
}
