using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BusinessLogic.ManualMapper
{
    public static class CompanyMapper
    {
        public static ContractorInformationVM ToViewModel(Company company)
        {
            if (company != null)
            {
                return new ContractorInformationVM()
                {
                    Id = company.Id,
                    UserId = company.UserId.Value,
                    Addresscompany = company.CompanyAddress,
                    Contentcompany = company.Introduction,
                    Invalidcapital = company.Capital,
                    Legalrepresentative = company.RepresentativeName,
                    Namecompany = company.CompanyName,
                    Phonecompany = company.CompanyPhoneNumber,
                    Position = company.Position,
                    Taxid = company.TaxCode,
                    Yearestablish = company.FoundedYear,
                    BussinessLicense = company.NoBusinessLicense,
                    OrganizationalChartPath = company.OrganizationalChartPath
                };
            }
            return new ContractorInformationVM();
        }

        public static Company ToModel(this ContractorInformationVM model)
        {
            return new Company
            {
                Id = model.Id,
                UserId = model.UserId,
                Capital = model.Invalidcapital,
                CompanyAddress = model.Addresscompany,
                CompanyName = model.Namecompany,
                CompanyPhoneNumber = model.Phonecompany,
                Introduction = model.Contentcompany,
                RepresentativeName = model.Legalrepresentative,
                FoundedYear = model.Yearestablish,
                Position = model.Position,
                TaxCode = model.Taxid
            };
        }

        public static void ToModel(ref Company model, CompanyVM vm)
        {
            if (model == null)
                model = new Company();

            model.UserId = vm.UserId;
            model.Capital = vm.Capital;
            model.CompanyAddress = vm.CompanyAddress;
            model.CompanyName = vm.CompanyName;
            model.CompanyPhoneNumber = vm.CompanyPhoneNumber;
            model.Introduction = vm.Introduction;
            model.RepresentativeName = vm.RepresentativeName;
            model.FoundedYear = vm.FoundedYear;
            model.Position = vm.Position;
            model.TaxCode = vm.TaxCode;
            model.NoBusinessLicense = vm.NoBusinessLicense;
            model.Link = vm.Link;
        }

        //public static void UpdateModel(Company source, Company target)
        //{
        //    target.UserId = source.UserId;
        //    target.Capital = source.Capital;
        //    target.CompanyAddress = source.CompanyAddress;
        //    target.CompanyName = source.CompanyName;
        //    target.CompanyPhoneNumber = source.CompanyPhoneNumber;
        //    target.Introduction = source.Introduction;
        //    target.RepresentativeName = source.RepresentativeName;
        //    target.FoundedYear = source.FoundedYear;
        //    target.Position = source.Position;
        //    target.TaxCode = source.TaxCode;
        //}
    }
}
