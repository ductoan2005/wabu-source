using System;
using System.Collections.Generic;

namespace FW.Models
{
    [Serializable]
    public class Users : BaseEntity
    {
        public long? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordChangedDate { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public byte? Authority { get; set; }
        public string FullName { get; set; }
        public byte? Gender { get; set; }      
        public string CMND { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public string AvatarName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPerson { get; set; }
        public bool? IsAgreeTerm { get; set; }
        public bool EmailConfirmed { get; set; }
        public string EmailConfirmToken { get; set; }

        public virtual ICollection<Construction> Constructions { get; set; }
    }
}
