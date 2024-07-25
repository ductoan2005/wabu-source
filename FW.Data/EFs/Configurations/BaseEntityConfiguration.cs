using FW.Data.Infrastructure;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.EFs.Configurations
{
    public class BaseEntityConfiguration : CustomEntityTypeConfiguration<BaseEntity>
    {
        public BaseEntityConfiguration() : base()
        {
            Property(c => c.IsDeleted).IsRequired();
            PropertyComment(c => c.IsDeleted, "check delete");

            Property(p => p.DateInserted).HasPrecision(3);
            CommonPropertyComment(p => p.DateInserted, "DateInserted");

            Property(p => p.DateUpdated).HasPrecision(3);
            CommonPropertyComment(p => p.DateUpdated, "DateUpdated");
        }
    }
}