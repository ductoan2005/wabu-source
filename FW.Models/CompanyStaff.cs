using System;

namespace FW.Models
{
    [Serializable]
    public class CompanyStaff : BaseEntity
    {
        public long? Id { get; set; }
        public long? CompanyId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; } // sdt lien he
        public virtual Company Company { get; set; }
    }
}
