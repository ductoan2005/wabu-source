using System;
using System.Collections.Generic;
using FW.Common.Pagination.Interfaces;
using FW.ViewModels;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Data.Infrastructure;
using FW.BusinessLogic.Interfaces;
using AutoMapper;
using FW.Models;
using FW.Common.Helpers;
using FW.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class CompanyMasterBL : ICompanyMasterBL
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;

        public CompanyMasterBL(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            this.companyRepository = companyRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateCompany(CompanyMasterVM companyMasterVM)
        {
            //try
            //{
            //    DbExecutionResult result = null;
            //    var company = Mapper.Map<Company>(companyMasterVM);
            //    var oldCompany = companyRepository.GetCompanyByCode(companyMasterVM.CompanyCode);
            //    if (oldCompany == null)
            //    {
            //        company.IsDeleted = false;
            //        companyRepository.Add(company);
            //        result = unitOfWork.Commit();
            //    }

            //    switch (result.ResultType)
            //    {
            //        case EDbExecutionResult.DuplicatePrimaryKey:
            //            throw new CommonExceptions(CommonResource.MSG_ERROR_EXIST_RECORD);
            //        case EDbExecutionResult.CommonError:
            //            throw new CommonExceptions(CommonResource.MSG_ERROR_SYSTEM);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public void DeleteCompany(long? id)
        {
            var companyToDelete = companyRepository.GetById(id);
            if (companyToDelete != null)
            {
                companyToDelete.IsDeleted = false;
            }
            companyRepository.Update(companyToDelete);
            DbExecutionResult result = unitOfWork.Commit();
            switch (result.ResultType)
            {
                case EDbExecutionResult.EntityNotExist:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_NOT_EXIST_RECORD);
                case EDbExecutionResult.EntityIsUse:
                //throw new CommonExceptions(CommonResource.MSG_RECORD_WAS_USING);
                case EDbExecutionResult.CommonError:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_SYSTEM);
            }
        }

        public IEnumerable<CompanyMasterVM> GetAll()
        {
            var companies = companyRepository.GetAll();
            var companyMasterVMs = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyMasterVM>>(companies);
            return companyMasterVMs.ToList();
        }

        public List<CompanyMasterVM> GetCompanies(IPaginationInfo paginationInfo, string orderByStr = null, string companyCode = null)
        {
            throw new NotImplementedException();
        }

        public async Task<CompanyMasterVM> GetCompany(long? id)
        {
            var company = await companyRepository.GetByIdAsync(id);
            var companyMasterVM = Mapper.Map<CompanyMasterVM>(company);
            return companyMasterVM;
        }

        public async Task<CompanyMasterVM> GetCompanyByUserId(long? id)
        {
            var company = companyRepository.Get(com=>com.UserId == id);
            var companyMasterVM = Mapper.Map<CompanyMasterVM>(company);
            return companyMasterVM;
        }

        public CompanyMasterVM GetCompanyByCode(string companyCode)
        {
            //var company = companyRepository.GetCompanyByCode(companyCode);
            //var companyMasterVM = Mapper.Map<CompanyMasterVM>(company);
            //return companyMasterVM;
            return null;
        }

        public void UpdateCompany(CompanyMasterVM companyMasterVM)
        {
            var dbCompany = companyRepository.GetById(companyMasterVM.Id);
            // check data is deleted
            if (dbCompany.IsDeleted == true)
            {
                throw new CommonExceptions(CommonResource.MSG_ERROR_NOT_EXIST_RECORD);
            }

            // check data is out of date
            if (dbCompany.DateUpdated != companyMasterVM.DateUpdated)
            {
                throw new CommonExceptions(CommonResource.MSG_ERROR_RECORD_OUT_OF_DATE);
            }

            // map data to update
            Mapper.Map(companyMasterVM, dbCompany);

            // excute update
            companyRepository.Update(dbCompany);

            // save data to database and verify data
            DbExecutionResult result = unitOfWork.Commit();
            switch (result.ResultType)
            {
                case EDbExecutionResult.EntityNotExist:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_NOT_EXIST_RECORD);
                case EDbExecutionResult.CommonError:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_SYSTEM);
            }
        }
    }
}
