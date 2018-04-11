using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.Entities;
using eBike.System.DAL;
#endregion

namespace eBike.System.BLL
{
    public class EmployeeController
    {
        public Employee Employee_Get(int employeeId)
        {
            using (var context = new eBikesContext())
            {
                return context.Employees.Find(employeeId);
            }
        }
    }
}
