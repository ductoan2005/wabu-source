namespace FW.Data.Migrations
{
    using FW.Data.EFs.Configurations;
    using FW.Data.Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FW.Data.EFs.MyFWDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FW.Data.EFs.MyFWDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            DoSeed(context);
        }

        private void DoSeed(FW.Data.EFs.MyFWDbContext context)
        {
            CompanyConfiguration companyConfiguration = new CompanyConfiguration();
            CompanyAbilityEquipmentConfiguration companyAbilityEquipmentConfiguration = new CompanyAbilityEquipmentConfiguration();
            CompanyAbilityExpConfiguration companyAbilityExpConfiguration = new CompanyAbilityExpConfiguration();
            CompanyAbilityFinanceConfiguration companyAbilityFinanceConfiguration = new CompanyAbilityFinanceConfiguration();
            CompanyAbilityHRConfiguration companyAbilityHRConfiguration = new CompanyAbilityHRConfiguration();
            CompanyAbilityHRDetailConfiguration companyAbilityHRDetailConfiguration = new CompanyAbilityHRDetailConfiguration();
            CompanyStaffConfiguration companyStaffConfiguration = new CompanyStaffConfiguration();
            AreaConfiguration areaConfiguration = new AreaConfiguration();
            BiddingNewsAbilityEquipmentConfiguration biddingNewsAbilityEquipmentConfiguration = new BiddingNewsAbilityEquipmentConfiguration();
            BiddingNewsTechnicalOtherConfiguration biddingNewsTechnicalOtherConfiguration = new BiddingNewsTechnicalOtherConfiguration();
            BiddingNewsAbilityHRConfiguration biddingNewsAbilityHRConfiguration = new BiddingNewsAbilityHRConfiguration();
            BiddingNewsConfiguration biddingNewsConfiguration = new BiddingNewsConfiguration();
            BiddingPackageConfiguration biddingPackageConfiguration = new BiddingPackageConfiguration();
            BiddingPackageOtherConfiguration biddingPackageOtherConfiguration = new BiddingPackageOtherConfiguration();
            ContractFormConfiguration contractFormConfiguration = new ContractFormConfiguration();
            ConstructionConfiguration contructionConfiguration = new ConstructionConfiguration();
            WorkContentConfiguration workContentConfiguration = new WorkContentConfiguration();
            WorkContentOtherConfiguration workContentOtherConfiguration = new WorkContentOtherConfiguration();
            UsersConfiguration usersConfiguration = new UsersConfiguration();
            LoginHistoryConfiguration loginHisConfiguration = new LoginHistoryConfiguration();
            CompanyProfileConfiguration companyProfileConfiguration = new CompanyProfileConfiguration();
            BiddingDetailConfiguration biddingDetailConfiguration = new BiddingDetailConfiguration();
            BiddingDetailFilesConfiguration biddingDetailFilesConfiguration = new BiddingDetailFilesConfiguration();
            BiddingNewsBookmarkConfiguration biddingNewsBookmarkConfiguration = new BiddingNewsBookmarkConfiguration();

            Dictionary<string, Dictionary<string, string>> mapOfCommentMaps = new Dictionary<string, Dictionary<string, string>>();

            mapOfCommentMaps.Add("StaticCommentMap", CustomEntityTypeConfigurationData.StaticCommentMap);
            mapOfCommentMaps.Add(typeof(CompanyConfiguration).Name, companyConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(CompanyAbilityEquipmentConfiguration).Name, companyAbilityEquipmentConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(CompanyAbilityExpConfiguration).Name, companyAbilityExpConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(CompanyAbilityFinanceConfiguration).Name, companyAbilityFinanceConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(CompanyAbilityHRConfiguration).Name, companyAbilityHRConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(CompanyAbilityHRDetailConfiguration).Name, companyAbilityHRDetailConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(CompanyStaffConfiguration).Name, companyStaffConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(AreaConfiguration).Name, areaConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingNewsAbilityEquipmentConfiguration).Name, biddingNewsAbilityEquipmentConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingNewsTechnicalOtherConfiguration).Name, biddingNewsTechnicalOtherConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingNewsAbilityHRConfiguration).Name, biddingNewsAbilityHRConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingNewsConfiguration).Name, biddingNewsConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingPackageConfiguration).Name, biddingPackageConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingPackageOtherConfiguration).Name, biddingPackageOtherConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(ContractFormConfiguration).Name, contractFormConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(ConstructionConfiguration).Name, contructionConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(WorkContentConfiguration).Name, workContentConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(WorkContentOtherConfiguration).Name, workContentOtherConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(UsersConfiguration).Name, usersConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(LoginHistoryConfiguration).Name, loginHisConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(CompanyProfileConfiguration).Name, companyProfileConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingDetailConfiguration).Name, biddingDetailConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingDetailFilesConfiguration).Name, biddingDetailFilesConfiguration.CommentMap);
            mapOfCommentMaps.Add(typeof(BiddingNewsBookmarkConfiguration).Name, biddingNewsBookmarkConfiguration.CommentMap);

            context.Commit();
        }
    }
}
