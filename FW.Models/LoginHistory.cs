using System;

namespace FW.Models
{
    [Serializable]
    public class LoginHistory : BaseEntity
    {
        public long? Id { get; set; }
        public int? LoginFailedTimes { get; set; }
        public DateTime? FirstLoginFailedTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
