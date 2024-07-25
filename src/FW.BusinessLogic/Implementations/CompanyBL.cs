using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.ManualMapper;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class CompanyBL : BaseBL, ICompanyBL
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly ICompanyStaffBL _companyStaff;
        private readonly ICompanyProfileBL _companyProfileBL;

        private static string GetStoragePath(string id) => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetUserAvatarFolderName, id);

        public CompanyBL(IUnitOfWork iUnitOfWork, ICompanyRepository iCompanyRepository, ICompanyStaffBL companyStaff, ICompanyProfileBL companyProfileBL)
        {
            _iUnitOfWork = iUnitOfWork;
            _iCompanyRepository = iCompanyRepository;
            _companyStaff = companyStaff;
            _companyProfileBL = companyProfileBL;
        }

        public Task<string> GetCompanyNameFromUserId(long? userId)
            => _iCompanyRepository.GetCompanyNameFromUserId(userId);

        public ContractorInformationVM GetContractorInformation(long userId)
        {
            var companyInfor = _iCompanyRepository.Get(c => c.UserId == userId);
            List<CompanyStaffVM> companyStaffs = new List<CompanyStaffVM>();
            if (companyInfor != null)
            {
                companyStaffs = _companyStaff.GetCompanyStaffByCompanyId(companyInfor.Id.Value).ToList();
            }
            return CompanyMapper.ToViewModel(companyInfor);
        }

        public async Task<long?> UpdateContractorInformation(CompanyVM viewModel)
        {
            var company = await _iCompanyRepository.GetAsync(x => x.Id == viewModel.Id && x.IsDeleted != true);

            if (company == null)
            {
                CompanyMapper.ToModel(ref company, viewModel);
                //if (viewModel.CompanyStaffs != null && viewModel.CompanyStaffs.Any())
                //{
                //    viewModel.CompanyStaffs.ToList().ForEach(x =>
                //    {
                //        x.DateInserted = x.DateUpdated = DateTime.Now;
                //        x.IsDeleted = false;
                //    });
                //    company.CompanyStaffs = viewModel.CompanyStaffs;
                //}

                _iCompanyRepository.Add(company);
                DbExecutionResult addResult = await _iUnitOfWork.CommitAsync();
                company.Logo = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(viewModel.LogoFile, GetStoragePath(company.UserId.ToString())), FileUtils.GetDomainAppPathPath());
                _iCompanyRepository.Update(company);
            }
            else
            {
                CompanyMapper.ToModel(ref company, viewModel);
                //if (viewModel.CompanyStaffs != null && viewModel.CompanyStaffs.Any())
                //{
                //    if (company.CompanyStaffs.Any(x => x.IsDeleted != true))
                //    {
                //        var updateStaffs = new List<CompanyStaff>();

                //        company.CompanyStaffs.ToList().ForEach(model =>
                //        {
                //            // Case delete staff
                //            if (!viewModel.CompanyStaffs.Any(vm => vm.Id == model.Id))
                //            {
                //                model.IsDeleted = true;
                //                model.DateUpdated = DateTime.Now;
                //            }
                //            //Case update
                //            viewModel.CompanyStaffs.ForEach(vm =>
                //            {
                //                if (vm.Id == model.Id)
                //                {
                //                    model.PhoneNumber = vm.PhoneNumber;
                //                    model.Position = vm.Position;
                //                    model.FullName = vm.FullName;
                //                    model.DateUpdated = DateTime.Now;
                //                    updateStaffs.Add(vm);
                //                }
                //            });
                //        });

                //        if(updateStaffs.Any())
                //        {
                //            viewModel.CompanyStaffs = viewModel.CompanyStaffs.Except(updateStaffs).ToList();
                //        }

                //        // Add new staff
                //        viewModel.CompanyStaffs.ForEach(x => {
                //            x.DateInserted = x.DateUpdated = DateTime.Now;
                //            x.IsDeleted = false;
                //        });
                //        company.CompanyStaffs.AddRange(viewModel.CompanyStaffs);
                //    }
                //    else
                //    {
                //        //Create new staff
                //        viewModel.CompanyStaffs.ForEach(x =>
                //        {
                //            x.DateInserted = x.DateUpdated = DateTime.Now;
                //            x.IsDeleted = false;
                //        });
                //        company.CompanyStaffs.AddRange(viewModel.CompanyStaffs);
                //    }
                //}
                //else
                //{
                //    //Case delete all staff
                //    if (company.CompanyStaffs != null && company.CompanyStaffs.Any())
                //    {
                //        company.CompanyStaffs.ToList().ForEach(model =>
                //        {
                //            model.IsDeleted = true;
                //            model.DateUpdated = DateTime.Now;
                //        });
                //    }
                //}
                //Check avatar is update
                if (viewModel.LogoFile?.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(company.Logo))
                    {
                        company.Logo = StringUtils.GetAbsolutePath(company.Logo);
                        FileUtils.DeleteFileIfExists(company.Logo);
                    }

                    company.Logo = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(viewModel.LogoFile, GetStoragePath(company.UserId.ToString())), FileUtils.GetDomainAppPathPath());
                }
                UpdateFileToServer(company, viewModel);
                _iCompanyRepository.Update(company);
            }
            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _iUnitOfWork.CommitAsync();
            CheckDbExecutionResultAndThrowIfAny(result);
            return company.Id;
        }

        public void UpdateContractFileUpload(ContractorInformationVM model)
        {
            var company = _iCompanyRepository.GetById(model.Id);
            company.NoBusinessLicense = model.BussinessLicense;
            company.OrganizationalChartName = model.OrganizationalChartName;
            _iCompanyRepository.Update(company);

            DbExecutionResult result = _iUnitOfWork.Commit();
            CheckDbExecutionResultAndThrowIfAny(result);
        }

        private void UpdateFileToServer(Company model, CompanyVM viewModel)
        {
            if (viewModel.NoBusinessLicenseFile?.ContentLength > 0)
            {
                if (!string.IsNullOrEmpty(model.NoBusinessLicensePath))
                {
                    model.NoBusinessLicensePath = StringUtils.GetAbsolutePath(model.NoBusinessLicensePath);
                    FileUtils.DeleteFileIfExists(model.NoBusinessLicensePath);
                }

                model.NoBusinessLicensePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(viewModel.NoBusinessLicenseFile, GetBusinessLicenseStoragePath(model.Id)), FileUtils.GetDomainAppPathPath());
                model.NoBusinessLicenseName = viewModel.NoBusinessLicenseFile.FileName;
            }

            if (viewModel.OrganizationalChartFile?.ContentLength > 0)
            {
                if (!string.IsNullOrEmpty(model.OrganizationalChartPath))
                {
                    model.OrganizationalChartPath = StringUtils.GetAbsolutePath(model.OrganizationalChartPath);
                    FileUtils.DeleteFileIfExists(model.OrganizationalChartPath);
                }

                model.OrganizationalChartPath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(viewModel.OrganizationalChartFile, GetOrganizationalChartStoragePath(model.Id)), FileUtils.GetDomainAppPathPath());
                model.OrganizationalChartName = viewModel.OrganizationalChartFile.FileName;
            }
        }

        private void StoreFileToServer(Company model, CompanyVM viewModel)
        {
            if (viewModel.NoBusinessLicenseFile?.ContentLength > 0)
            {
                model.NoBusinessLicensePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(viewModel.NoBusinessLicenseFile, GetBusinessLicenseStoragePath(model.Id)), FileUtils.GetDomainAppPathPath());
                model.NoBusinessLicenseName = viewModel.NoBusinessLicenseFile.FileName;
            }

            if (viewModel.OrganizationalChartFile?.ContentLength > 0)
            {
                model.OrganizationalChartPath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(viewModel.OrganizationalChartFile, GetOrganizationalChartStoragePath(model.Id)), FileUtils.GetDomainAppPathPath());
                model.OrganizationalChartName = viewModel.OrganizationalChartFile.FileName;
            }
        }

        private static string GetBusinessLicenseStoragePath(long? companyId)
            => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetBusinessLicenseFolderName, companyId.ToString());

        private static string GetOrganizationalChartStoragePath(long? companyId)
           => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetCompanyOrganizationFolderName, companyId.ToString());

        public async Task<CompanyProfileVM> GetCompanyByCompanyProfileId(long companyProfileId)
        {
            return await _companyProfileBL.GetCompanyProfileById(companyProfileId);
        }

        public async Task RatingStarForCompany(long companyProfileId, byte star)
        {
            var companyProfile = await GetCompanyByCompanyProfileId(companyProfileId);
            // get company by id
            var company = _iCompanyRepository.GetById(companyProfile.CompanyId);
            switch (star)
            {
                case 1:
                    company.OneStar = company.OneStar + 1;
                    break;
                case 2:
                    company.TwoStar = company.TwoStar + 1;
                    break;
                case 3:
                    company.ThreeStar = company.ThreeStar + 1;
                    break;
                case 4:
                    company.FourStar = company.FourStar + 1;
                    break;
                case 5:
                    company.FiveStar = company.FiveStar + 1;
                    break;
                default:
                    break;
            }

            _iCompanyRepository.Update(company);

            DbExecutionResult result = _iUnitOfWork.Commit();
            CheckDbExecutionResultAndThrowIfAny(result);
        }
    }
}
