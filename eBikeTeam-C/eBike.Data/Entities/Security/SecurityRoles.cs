using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.Entities.Security
{
    public static class SecurityRoles
    {
        public const string Webmaster = "Webmaster";
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string RegisteredUsers = "RegisteredUsers";

        public static List<string> DefaultSecurityRoles
        {
            get
            {
                List<string> value = new List<string>();
                value.Add(Webmaster);
                value.Add(Admin);
                value.Add(Staff);
                value.Add(RegisteredUsers);
                return value;
            }
        }
    }
}
