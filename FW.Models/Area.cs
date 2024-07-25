using System;

namespace FW.Models
{
    [Serializable]
    public class Area : BaseEntity
    {
        public long? Id { get; set; }
        public string AreaName { get; set; }
    }
}
