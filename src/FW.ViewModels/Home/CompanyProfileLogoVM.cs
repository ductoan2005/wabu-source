using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.ViewModels.Home
{
    public class CompanyProfileLogoVM
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string CompanyName { get; set; }
        public string Introduction { get; set; } // gioi thieu cong ty
        public string RepresentativeName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string Logo { get; set; } // Logo cty
        public string Link { get; set; }
        public byte IsOnOver { get; set; }

        public bool AdvertisingIsOn { get; set; } // trang thai quang cao la bat

        public string AdvertisingBackgroundImage { get; set; } // Link hinh nen quang cao

    }
}
