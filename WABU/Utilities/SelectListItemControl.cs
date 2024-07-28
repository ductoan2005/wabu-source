using FW.Common.Enum;
using FW.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WABU.Utilities
{
    public static class SelectListItemControl
    {
        public static List<SelectListItem> ListBiddingPackageNameForCreateAndEdit()
        {
            var selectList = new List<SelectListItem>();

            var enumConstruction = Enum.GetValues(typeof(EBiddingPackageNameForCreateAndEdit)) as EBiddingPackageNameForCreateAndEdit[];
            if (enumConstruction == null)
                return null;

            foreach (var enumValue in enumConstruction)
            {
                // Create a new SelectListItem element and set its 
                // Value and Text to the enum value and description.
                selectList.Add(new SelectListItem
                {
                    Value = ((int)enumValue).ToString(),
                    // GetIndustryName just returns the Display.Name value
                    // of the enum - check out the next chapter for the code of this function.
                    Text = GetBiddingPackageName(enumValue)
                });
            }

            return selectList;
        }

        private static string GetBiddingPackageName(EBiddingPackageNameForCreateAndEdit value)
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                   as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }

        public static List<SelectListItem> ListBiddingPackageNameForView()
        {
            var selectList = new List<SelectListItem>();

            var enumConstruction = Enum.GetValues(typeof(EBiddingPackageName)) as EBiddingPackageName[];
            if (enumConstruction == null)
                return null;

            foreach (var enumValue in enumConstruction)
            {
                // Create a new SelectListItem element and set its 
                // Value and Text to the enum value and description.
                selectList.Add(new SelectListItem
                {
                    Value = ((int)enumValue).ToString(),
                    // GetIndustryName just returns the Display.Name value
                    // of the enum - check out the next chapter for the code of this function.
                    Text = GetBiddingPackageName(enumValue)
                });
            }

            return selectList;
        }

        private static string GetBiddingPackageName(EBiddingPackageName value)
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                   as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }

        internal static object ListContractFormForCreateAndEdit()
        {
            var selectList = new List<SelectListItem>();

            var enumConstruction = Enum.GetValues(typeof(EContractFormForCreateAndEdit)) as EContractFormForCreateAndEdit[];
            if (enumConstruction == null)
                return null;

            foreach (var enumValue in enumConstruction)
            {
                // Create a new SelectListItem element and set its 
                // Value and Text to the enum value and description.
                selectList.Add(new SelectListItem
                {
                    Value = ((int)enumValue).ToString(),
                    // GetIndustryName just returns the Display.Name value
                    // of the enum - check out the next chapter for the code of this function.
                    Text = GetContractFormName(enumValue)
                });
            }

            return selectList;
        }

        private static string GetContractFormName(EContractFormForCreateAndEdit value)
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                   as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }

        public static List<SelectListItem> GetListConstructionFormForView()
        {
            var selectList = new List<SelectListItem>();

            var enumConstruction = Enum.GetValues(typeof(EConstructionForm)) as EConstructionForm[];
            if (enumConstruction == null)
                return null;

            foreach (var enumValue in enumConstruction)
            {
                // Create a new SelectListItem element and set its 
                // Value and Text to the enum value and description.
                selectList.Add(new SelectListItem
                {
                    Value = ((int)enumValue).ToString(),
                    // GetIndustryName just returns the Display.Name value
                    // of the enum - check out the next chapter for the code of this function.
                    Text = GetFromName(enumValue)
                });
            }

            selectList.Insert(0, new SelectListItem
            {
                Text = "Vui lòng chọn hình thức xây dựng",
                Value = "",
                Selected = true
            });
            return selectList;
        }

        private static string GetFromName<T>(T value)
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                   as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }
    }
}