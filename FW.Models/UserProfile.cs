using System;

namespace FW.Models
{
    [Serializable]
    public class UserProfile
    {
        public long? UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public byte? Authority { get; set; }

        public byte? OldAuthority { get; set; }

        public string FullName { get; set; }

        public string CMND { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public bool? IsActive { get; set; }

        public string AvatarPath { get; set; }

        public string AvatarName { get; set; }
    }
}
