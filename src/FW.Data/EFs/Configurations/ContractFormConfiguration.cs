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
    public class ContractFormConfiguration : CustomEntityTypeConfiguration<ContractForm>
    {
        public ContractFormConfiguration() : base()
        {
            ToTable("tbl_contract_form");
            EntityTypeComment("table contract_form");

            //Id
            HasKey(c => c.Id);
            PropertyComment(c => c.Id, "ID contract_form de quan ly DB");

            //ContractFormId
            Property(p => p.ContractFormName);
            PropertyComment(p => p.ContractFormName, "hinh thuc hop dong");
        }
    }
}
