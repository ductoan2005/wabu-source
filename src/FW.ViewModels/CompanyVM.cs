using FW.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace FW.ViewModels
{
    public class CompanyVM
    {
        public long? Id { get; set; }

        public long? UserId { get; set; }

        public string CompanyName { get; set; }

        public string Introduction { get; set; } // gioi thieu cong ty

        public string RepresentativeName { get; set; } // ten nguoi dai dien

        public string Position { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public HttpPostedFileBase NoBusinessLicenseFile { get; set; } // GPKD File

        public string NoBusinessLicense { get; set; } // GPKD

        public string NoBusinessLicensePath { get; set; } // GPKD file path

        public string NoBusinessLicenseName { get; set; }// GPKD file name

        public string FoundedYear { get; set; } // nam thanh lap

        public string Capital { get; set; } // von dieu le

        public string TaxCode { get; set; } // ma so thue

        public HttpPostedFileBase OrganizationalChartFile { get; set; } // so do to chuc file

        public string OrganizationalChartName { get; set; } // so do to chuc file name

        public string OrganizationalChartPath { get; set; } // so do to chuc file path    

        public string ContactName { get; set; } // nguoi lien he

        public string ContactPhoneNumber { get; set; } // sdt lien he

        public string Logo { get; set; } // Logo cty
        public HttpPostedFileBase LogoFile { get; set; }

        public int OneStar { get; set; }

        public int TwoStar { get; set; }

        public int ThreeStar { get; set; }

        public int FourStar { get; set; }

        public int FiveStar { get; set; }

        //public bool? TermOfUse { get; set; }

        public string Link { get; set; } //website

        public byte IsOnOver { get; set; }

        public int TotalNewsBidded { get; set; }

        public int NumberNewsBidded { get; set; }

        public bool AdvertisingIsOn { get; set; } // trang thai quang cao la bat

        public string AdvertisingBackgroundImage { get; set; } // Link hinh nen quang cao

        public HttpPostedFileBase AdvertisingBackgroundImageFile { get; set; }

        public Users Users { get; set; }

        public List<CompanyStaff> CompanyStaffs { get; set; }

        public List<CompanyAbilityExpVM> CompanyAbilityExps { get; set; }

        public List<CompanyAbilityFinanceVM> CompanyAbilityFinances { get; set; }

        public List<CompanyAbilityHRVM> CompanyAbilityHRs { get; set; }

        public List<CompanyAbilityEquipmentVM> CompanyAbilityEquipments { get; set; }

        public List<CompanyProfileVM> CompanyProfiles { get; set; }
    }
}
