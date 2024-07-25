using System;

namespace FW.Models
{
    [Serializable]
    public abstract class BaseEntity
    {
        // delete logic
        public bool? IsDeleted { get; set; }
        // ngay insert
        public virtual DateTime? DateInserted { get; set; }
        // ngay update
        public virtual DateTime? DateUpdated { get; set; }
    }
}
