using FW.Common.Enum;
using FW.Models;
using System.Web;

namespace WABU.Utilities
{
    public class SessionObjects
    {
        public static UserProfile UserProfile
        {
            get
            {
                var context = HttpContext.Current;
                return context.Session[SessionObjectKey.UserProfile.ToString()] as UserProfile;
            }

            set { HttpContext.Current.Session[SessionObjectKey.UserProfile.ToString()] = value; }
        }
    }
}

