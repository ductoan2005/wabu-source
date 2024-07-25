using System;
using FW.Common.Consts;
using System.Configuration;

namespace FW.Common.Helpers
{
    public static class CommonSettings
    {
        public static int ItemsPerPage => GetInt32FromAppSetting2("ItemsPerPage", 0, 10);

        internal static int GetInt32FromAppSetting2(string appSettingKey, int thresholdValue, int defaultValue)
        {
            int.TryParse(ConfigurationManager.AppSettings[appSettingKey], out var result);

            if (result <= thresholdValue)
            {
                result = defaultValue;
            }

            return result;
        }
        public static int PeriodAttemptLoginByMinutes => GetInt32FromAppSetting2("PeriodAttemptLoginByMinutes", 5, 5);

        public static int PeroidTimeKeepLogFile => GetInt32FromAppSetting2("PeroidTimeKeepLogFile", 0, -90);

        internal static string GetAppSetting2(string appSettingKey, string defaultValue) => string.IsNullOrEmpty(ConfigurationManager.AppSettings[appSettingKey]) ? defaultValue : ConfigurationManager.AppSettings[appSettingKey];

        public static string GetServerStoragePath => GetAppSetting2(SystemSettingConst.ServerStoragePath, "UploadFolder");

        public static string GetPrintBiddingFolderName => GetAppSetting2(SystemSettingConst.PrintBiddingFolderName, "BiddingDetailFiles");

        public static string GetCompanyAbilityExpFolderName => GetAppSetting2(SystemSettingConst.CompanyAbilityExpFolderName, "CompanyAbilityExpFiles");

        public static string GetCompanyAbilityFinanceFolderName => GetAppSetting2(SystemSettingConst.CompanyAbilityFinanceFolderName, "CompanyAbilityFinanceFiles");

        public static string GetCompanyAbilityHrFolderName => GetAppSetting2(SystemSettingConst.CompanyAbilityHrFolderName, "CompanyAbilityHrFiles");

        public static string GetCompanyAbilityEquipmentFolderName => GetAppSetting2(SystemSettingConst.CompanyAbilityEquipmentFolderName, "CompanyAbilityEquipmentFiles");

        public static string GetConstructionFolderName => GetAppSetting2(SystemSettingConst.PrintBiddingFolderName, "ConstructionFiles");

        public static string GetBiddingNewsFolderName => GetAppSetting2(SystemSettingConst.BiddingNewsFolderName, "BiddingNewsFiles");

        public static string GetUserAvatarFolderName => GetAppSetting2(SystemSettingConst.UserAvatarFolderName, "Users/Avatars");

        public static string GetBusinessLicenseFolderName => GetAppSetting2(SystemSettingConst.BusinessLicenseFolderName, "BusinessLicense");

        public static string GetCompanyOrganizationFolderName => GetAppSetting2(SystemSettingConst.CompanyOrganizationFolderName, "CompanyOrganization");

    }
}
