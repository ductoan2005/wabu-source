using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyStaffBL
    {
        IEnumerable<CompanyStaffVM> GetCompanyStaffByCompanyId(long id);
        void UpdateCompanyStaff(IEnumerable<CompanyStaffVM> staffs,long companyId);
        void DeleteCompanyStaff(IEnumerable<long?> staffIds);
    }
}
