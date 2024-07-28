using FW.Common.Helpers;
using FW.Data.EFs.Configurations;
using FW.Data.Infrastructure;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FW.Data.EFs
{
    [DbConfigurationType(typeof(DbConfiguration))]
    public class MyFWDbContext : FWDbContext
    {
        public MyFWDbContext() : base(nameOrConnectionString: "FWDbContext")
        {
        }

        public MyFWDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //ConfigurationRegistrar ConfigurationRegistrar
            var companyConfiguration = new CompanyConfiguration();
            var companyAbilityEquipmentConfiguration = new CompanyAbilityEquipmentConfiguration();
            var companyAbilityExpConfiguration = new CompanyAbilityExpConfiguration();
            var companyAbilityFinanceConfiguration = new CompanyAbilityFinanceConfiguration();
            var companyAbilityHRConfiguration = new CompanyAbilityHRConfiguration();
            var companyAbilityHRDetailConfiguration = new CompanyAbilityHRDetailConfiguration();
            var companyStaffConfiguration = new CompanyStaffConfiguration();
            var areaConfiguration = new AreaConfiguration();
            var biddingNewsAbilityEquipmentConfiguration = new BiddingNewsAbilityEquipmentConfiguration();
            var biddingNewsAbilityHRConfiguration = new BiddingNewsAbilityHRConfiguration();
            var biddingNewsTechnicalOtherConfiguration = new BiddingNewsTechnicalOtherConfiguration();
            var biddingNewsConfiguration = new BiddingNewsConfiguration();
            var biddingPackageConfiguration = new BiddingPackageConfiguration();
            var biddingPackageOtherConfiguration = new BiddingPackageOtherConfiguration();
            var contractFormConfiguration = new ContractFormConfiguration();
            var contructionConfiguration = new ConstructionConfiguration();
            var workContentConfiguration = new WorkContentConfiguration();
            var workContentOtherConfiguration = new WorkContentOtherConfiguration();
            var usersConfiguration = new UsersConfiguration();
            var loginHisConfiguration = new LoginHistoryConfiguration();
            var companyProfileConfiguration = new CompanyProfileConfiguration();
            var biddingDetailConfiguration = new BiddingDetailConfiguration();
            var biddingDetailFilesConfiguration = new BiddingDetailFilesConfiguration();
            var biddingNewsBookmarkConfiguration = new BiddingNewsBookmarkConfiguration();
            var notificationConfiguration = new NotificationConfiguration();
            var postConfiguration = new PostConfiguration();

            modelBuilder.Configurations.Add(companyConfiguration);
            modelBuilder.Configurations.Add(companyAbilityEquipmentConfiguration);
            modelBuilder.Configurations.Add(companyAbilityExpConfiguration);
            modelBuilder.Configurations.Add(companyAbilityFinanceConfiguration);
            modelBuilder.Configurations.Add(companyAbilityHRConfiguration);
            modelBuilder.Configurations.Add(companyAbilityHRDetailConfiguration);
            modelBuilder.Configurations.Add(companyStaffConfiguration);
            modelBuilder.Configurations.Add(areaConfiguration);
            modelBuilder.Configurations.Add(biddingNewsAbilityEquipmentConfiguration);
            modelBuilder.Configurations.Add(biddingNewsTechnicalOtherConfiguration);
            modelBuilder.Configurations.Add(biddingNewsAbilityHRConfiguration);
            modelBuilder.Configurations.Add(biddingNewsConfiguration);
            modelBuilder.Configurations.Add(biddingPackageConfiguration);
            modelBuilder.Configurations.Add(biddingPackageOtherConfiguration);
            modelBuilder.Configurations.Add(contractFormConfiguration);
            modelBuilder.Configurations.Add(contructionConfiguration);
            modelBuilder.Configurations.Add(workContentConfiguration);
            modelBuilder.Configurations.Add(workContentOtherConfiguration);
            modelBuilder.Configurations.Add(usersConfiguration);
            modelBuilder.Configurations.Add(loginHisConfiguration);
            modelBuilder.Configurations.Add(companyProfileConfiguration);
            modelBuilder.Configurations.Add(biddingDetailConfiguration);
            modelBuilder.Configurations.Add(biddingDetailFilesConfiguration);
            modelBuilder.Configurations.Add(biddingNewsBookmarkConfiguration);
            modelBuilder.Configurations.Add(notificationConfiguration);
            modelBuilder.Configurations.Add(postConfiguration);
        }

        public override DbExecutionResult Commit()
        {
            try
            {
                DbExecutionResult.AffectedRows = -1;
                DbExecutionResult.AffectedRows = base.SaveChanges();
                DbExecutionResult.ResultType = EDbExecutionResult.Normal;
            }
            catch (DbUpdateConcurrencyException ex1)
            {
                //SysLogger.Error(CommonResource.LoggerException, ex1);
                DbExecutionResult.Exception = ex1;
                DbExecutionResult.ResultType = EDbExecutionResult.EntityNotExist;
            }
            catch (Exception ex)
            {
                //SysLogger.Error(CommonResource.LoggerException, ex);
                DbExecutionResult.Exception = ex;
                DbExecutionResult.ResultType = EDbExecutionResult.CommonError;

                while (ex != null)
                {
                    if (ex is SqlException)
                    {
                        switch ((ex as SqlException).Number)
                        {
                            case CommonConstants.SQL_DUPLICATE_PK_CODE:
                                DbExecutionResult.ResultType = EDbExecutionResult.DuplicatePrimaryKey;
                                break;
                            case CommonConstants.SQL_ID_IS_USE:
                                DbExecutionResult.ResultType = EDbExecutionResult.EntityIsUse;
                                break;
                        }
                    }
                    else if (ex.Message?.Contains(CommonConstants.SQL_DUPLICATE_PK_PHRASE) == true)
                    {
                        DbExecutionResult.ResultType = EDbExecutionResult.DuplicatePrimaryKey;
                        break;
                    }

                    ex = ex.InnerException;
                }
            }

            return DbExecutionResult;
        }

        public override async Task<DbExecutionResult> CommitAsync()
        {
            try
            {
                DbExecutionResult.AffectedRows = -1;
                DbExecutionResult.AffectedRows = await SaveChangesAsync();
                DbExecutionResult.ResultType = EDbExecutionResult.Normal;
            }
            catch (DbUpdateConcurrencyException ex1)
            {
                //SysLogger.Error(CommonResource.LoggerException, ex1);
                DbExecutionResult.Exception = ex1;
                DbExecutionResult.ResultType = EDbExecutionResult.EntityNotExist;
            }
            catch (Exception ex)
            {
                //SysLogger.Error(CommonResource.LoggerException, ex);
                DbExecutionResult.Exception = ex;
                DbExecutionResult.ResultType = EDbExecutionResult.CommonError;

                while (ex != null)
                {
                    if (ex is SqlException)
                    {
                        switch ((ex as SqlException).Number)
                        {
                            case CommonConstants.SQL_DUPLICATE_PK_CODE:
                                DbExecutionResult.ResultType = EDbExecutionResult.DuplicatePrimaryKey;
                                break;
                            case CommonConstants.SQL_ID_IS_USE:
                                DbExecutionResult.ResultType = EDbExecutionResult.EntityIsUse;
                                break;
                        }
                    }
                    else if (ex.Message?.Contains(CommonConstants.SQL_DUPLICATE_PK_PHRASE) == true)
                    {
                        DbExecutionResult.ResultType = EDbExecutionResult.DuplicatePrimaryKey;
                        break;
                    }

                    ex = ex.InnerException;
                }
            }

            return DbExecutionResult;
        }
    }
}
