using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.ViewModels
{
    public class CompanyMasterVM
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(3)]
        public string CompanyCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string CompanyName { get; set; }

        [Required]
        public bool? IsDeleted { get; set; }

        public string CompanyAddress { get; set; }

        public string Logo { get; set; } // Logo cty

        public string Link { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
