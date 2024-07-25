using System;
using System.Collections.Generic;

namespace FW.Models
{
    [Serializable]
    public class Company : BaseEntity
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string CompanyName { get; set; }
        public string Introduction { get; set; } // gioi thieu cong ty
        public string RepresentativeName { get; set; } // ten nguoi dai dien
        public string Position { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string NoBusinessLicense { get; set; } // GPKD
        public string NoBusinessLicensePath { get; set; } // GPKD file path
        public string NoBusinessLicenseName { get; set; }// GPKD file name
        public string FoundedYear { get; set; } // nam thanh lap
        public string Capital { get; set; } // von dieu le
        public string TaxCode { get; set; } // ma so thue
        public string OrganizationalChartName { get; set; } // so do to chuc file name
        public string OrganizationalChartPath { get; set; } // so do to chuc file path
        public string ContactName { get; set; } // nguoi lien he
        public string ContactPhoneNumber { get; set; } // sdt lien he
        public string Logo { get; set; } // Logo cty
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }
        //public bool? TermOfUse { get; set; }
        public string Link { get; set; } //website
        public byte IsOnOver { get; set; }
        public int TotalBiddedNews { get; set; } //tổng số tin thầu đã đấu
        public int ProjectImplemented { get; set; } // tổng số tin thầu được chủ đầu tư bấm xác nhận hợp đồng
        public int ProjectsComplete { get; set; } //tổng số tin thầu đã nghiệm thu(hoàn thành)
        public string StringHtml { get; set; }
        public bool AdvertisingIsOn { get; set; } // trang thai quang cao la bat
        public string AdvertisingBackgroundImage { get; set; } // Link hinh nen quang cao

        public virtual Users User { get; set; }
        public virtual ICollection<CompanyStaff> CompanyStaffs { get; set; }
        public virtual ICollection<CompanyAbilityExp> CompanyAbilityExps { get; set; }
        public virtual ICollection<CompanyAbilityFinance> CompanyAbilityFinances { get; set; }
        public virtual ICollection<CompanyAbilityHR> CompanyAbilityHRs { get; set; }
        public virtual ICollection<CompanyAbilityEquipment> CompanyAbilityEquipments { get; set; }
        public virtual ICollection<CompanyProfile> CompanyProfiles { get; set; }
    }
}
