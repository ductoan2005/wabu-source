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
    public class CompanyAbilityHRDetailConfiguration : CustomEntityTypeConfiguration<CompanyAbilityHRDetail>
    {
        public CompanyAbilityHRDetailConfiguration() : base()
        {
            ToTable("tbl_company_ability_hr_detail");

            //Id
            HasKey(c => c.Id);

            Property(c => c.FromYear);
            Property(c => c.ToYear);
            Property(c => c.ProjectSimilar);
            Property(c => c.PositionSimilar);
            Property(c => c.ExpTechnical);
            Property(c => c.CompanyAbilityHRId);
        }
    }
}
