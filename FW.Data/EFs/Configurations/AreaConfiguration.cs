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
    public class AreaConfiguration : CustomEntityTypeConfiguration<Area>
    {
        public AreaConfiguration() : base()
        {
            ToTable("tbl_area");
            EntityTypeComment("table area");

            //Id
            HasKey(c => c.Id);
            PropertyComment(c => c.Id, "ID area de quan ly DB");

            //AreaName
            Property(p => p.AreaName).IsRequired();
            PropertyComment(p => p.AreaName, "ten khu vuc");
        }
    }
}
