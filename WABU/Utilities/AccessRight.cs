using FW.Common.Enum;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Resources;
using System;
using System.Collections.Generic;

namespace WABU.Utilities
{
    public static class AccessRight
    {
        private static readonly Dictionary<string, List<EAuthority>> MapRoles = new Dictionary<string, List<EAuthority>>();

        static AccessRight()
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                MapRoles.Add(CommonConstants.SCREEN_ACCOUNT,
                    new List<EAuthority> {
                        EAuthority.Root,
                        EAuthority.Administrator,
                        EAuthority.Investor,
                        EAuthority.Contractor});

                MapRoles.Add(CommonConstants.SCREEN_BIDDING_NEWS_REGISTRATION,
                    new List<EAuthority> {
                        EAuthority.Root,
                        EAuthority.Administrator,
                        EAuthority.Investor});

                MapRoles.Add(CommonConstants.SCREEN_BID_CONTRACTION_DETAIL,
                    new List<EAuthority> {
                        EAuthority.Root,
                        EAuthority.Administrator,
                        EAuthority.Investor,
                        EAuthority.Contractor
                    });
                MapRoles.Add(CommonConstants.SCREEN_FILTER_BIDDINGNEWS,
                    new List<EAuthority> {
                        EAuthority.Root,
                        EAuthority.Administrator
                    });
                MapRoles.Add(CommonConstants.SCREEN_FILTER_POST,
                    new List<EAuthority> {
                        EAuthority.Root,
                        EAuthority.Administrator});

                MapRoles.Add(CommonConstants.SCREEN_CONSTRUCTION,
                   new List<EAuthority> {
                        EAuthority.Investor
                   });

                MapRoles.Add(CommonConstants.SCREEN_BIDDING_NEWS_BOOKMARK,
                   new List<EAuthority> {
                        EAuthority.Contractor
                   });

                MapRoles.Add(CommonConstants.SCREEN_ACCOUNT_PAGECONTRACT,
                   new List<EAuthority> {
                        EAuthority.Contractor
                   });

                MapRoles.Add(CommonConstants.SCREEN_PAGECONTRACTBID,
                  new List<EAuthority> {
                        EAuthority.Contractor
                  });
            }
            catch (Exception ex)
            {
                SysLogger.Error(CommonResource.LoggerException, ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        public static bool CheckRole(EAuthority authority, string function)
        {
            if (!String.IsNullOrWhiteSpace(function) && MapRoles?.ContainsKey(function) == true)
            {
                IList<EAuthority> listRole = MapRoles[function];
                return listRole?.IndexOf(authority) >= 0;
            }

            return false;
        }
    }
}