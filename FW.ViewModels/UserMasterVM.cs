using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FW.ViewModels
{
    public class UserMasterVM
    {
        public long? Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public DateTime? PasswordChangedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
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

        public bool? IsDeleted { get; set; }

        public DateTime? DateInserted { get; set; }

        public DateTime? DateUpdated { get; set; }

        public HttpPostedFileBase AvatarFile { get; set; }

        public bool EmailConfirmed { get; set; }

        public string EmailConfirmToken { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        /// <summary>
        /// For user update with company information
        /// </summary>
        public long? UserId { get; set; }

        public string CompanyName { get; set; }

        public string Link { get; set; } //website

        public bool AdvertisingIsOn { get; set; } // trang thai quang cao la bat

        public string AdvertisingBackgroundImage { get; set; } // Link hinh nen quang cao

        public HttpPostedFileBase AdvertisingBackgroundImageFile { get; set; } // Link hinh nen quang cao

        public string CompanyAddress { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public long? CompanyId { get; set; }

    }
}
