using System;

namespace FW.Models
{
    [Serializable]
    public class ContractForm : BaseEntity
    {
        public long? Id { get; set; }
        public string ContractFormName { get; set; }
    }
}
