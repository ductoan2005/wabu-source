using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.ViewModels
{
    public class LoginVM
    {
        public long? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        public byte? Authority { get; set; }
        public bool? IsPerson { get; set; }
        public bool? IsAgreeTerm { get; set; }
        public string returnURL { get; set; }
        public string AddResultStatus { get; set; }
    }
}
