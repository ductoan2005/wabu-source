using FW.Data.Infrastructure;
using FW.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.EFs.Configurations
{
    public class CompanyStaffConfiguration : CustomEntityTypeConfiguration<CompanyStaff>
    {
        public CompanyStaffConfiguration() : base()
        {
            ToTable("tbl_company_ability_staff");

            //Id
            HasKey(c => c.Id);

            Property(c => c.CompanyId);
            Property(c => c.FullName);
            Property(c => c.Position);
            Property(c => c.PhoneNumber);
        }
    }
}
