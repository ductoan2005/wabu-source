using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.ManualMapper;
using FW.Data.EFs.Repositories;
using FW.Data.RepositoryInterfaces;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class CompanyStaffBL : BaseBL, ICompanyStaffBL
    {
        private ICompanyStaffRepository _companyStaffRepository;
        public CompanyStaffBL(ICompanyStaffRepository companyStaffRepository)
        {
            _companyStaffRepository = companyStaffRepository;
        }

        public void UpdateCompanyStaff(IEnumerable<CompanyStaffVM> staffs, long companyId)
        {
            var oldStaffs = _companyStaffRepository.GetMany(c => c.CompanyId == companyId && ((c.IsDeleted.HasValue && !c.IsDeleted.Value) || (!c.IsDeleted.HasValue)))
                                                    .Select(c => c.Id);
            var deleteStaff = oldStaffs.Where(oldstaff => !staffs.Select(s => s.StaffNum).Any(newstaff => newstaff == oldstaff));

            var newStaff = staffs.Where(s => !s.StaffNum.HasValue);

            foreach (var staff in newStaff)
            {
                var companyStaff = staff.ToModel(companyId);
                _companyStaffRepository.Add(companyStaff);
            }
            DeleteCompanyStaff(deleteStaff);
        }

        public void DeleteCompanyStaff(IEnumerable<long?> staffIds)
        {
            foreach (var staff in staffIds)
            {
                var companyStaff = _companyStaffRepository.GetById(staff.Value);
                if (companyStaff != null)
                {
                    companyStaff.IsDeleted = true;
                    _companyStaffRepository.Update(companyStaff);
                }
            }
        }

        public IEnumerable<CompanyStaffVM> GetCompanyStaffByCompanyId(long id)
        {
            return _companyStaffRepository.GetMany(c => c.CompanyId == id && ((c.IsDeleted.HasValue && !c.IsDeleted.Value) || !c.IsDeleted.HasValue))
                                    .Select(r => new CompanyStaffVM
                                    {
                                        StaffNum = r.Id,
                                        CompanyId = r.CompanyId,
                                        StaffName = r.FullName,
                                        StaffPhone = r.PhoneNumber,
                                        StaffPosition = r.Position
                                    });
        }
    }
}
