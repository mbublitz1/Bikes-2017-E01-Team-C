using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.Entities.Security;
using Microsoft.AspNet.Identity.EntityFramework;
#endregion

namespace eBike.System.DAL.Security
{
    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
