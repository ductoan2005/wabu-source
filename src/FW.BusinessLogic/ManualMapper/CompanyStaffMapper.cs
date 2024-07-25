using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BusinessLogic.ManualMapper
{
    public static class CompanyStaffMapper
    {
        public static CompanyStaff ToModel(this CompanyStaffVM companyStaffVM,long companyId)
        {
            return new CompanyStaff()
            {
                CompanyId = companyId,
                FullName = companyStaffVM.StaffName,
                PhoneNumber = companyStaffVM.StaffPhone,
                Position = companyStaffVM.StaffPosition
            };
        }
    }
}
