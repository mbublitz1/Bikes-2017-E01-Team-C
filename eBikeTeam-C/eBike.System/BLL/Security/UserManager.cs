using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.System.BLL.Security;
using eBike.Data.Entities.Security;
using eBike.Data.Entities;
using eBike.System.DAL;
using eBike.System.DAL.Security;
using eBike.Data.POCOs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
#endregion

namespace eBike.System.BLL.Security
{
    [DataObject]
    public class UserManager : UserManager<ApplicationUser>
    {
        #region Constants
        private const string STR_DEFAULT_PASSWORD = "Pa$$wordTeam3";
        /// <summary>Requires FirstName and LastName</summary>
        private const string STR_USERNAME_FORMAT = "{0}{1}";
        /// <summary>Requires UserName</summary>
        private const string STR_EMAIL_FORMAT = "{0}@ebikes.ca";
        private const string STR_WEBMASTER_USERNAME = "webmaster";

        private readonly string[] STR_TEAM_USERNAME = { "blong", "bkah", "ppadam", "mbublitz" };
        private readonly string[] STR_TEAM_PASSWORD = { "Pa$$wordLONG", "Pa$$wordKAH", "Pa$$wordPADAM", "Pa$$wordBUBLITZ" };
        #endregion


        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }

        public void AddWebMaster()
        {
            if(!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {

                var webmasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };

                this.Create(webmasterAccount, STR_DEFAULT_PASSWORD);

                this.AddToRole(webmasterAccount.Id, SecurityRoles.Webmaster);
            }
        }

        public void AddTeamMembers()
        {
            for(int i = 0; i < STR_TEAM_USERNAME.Length; i++)
            {
                string UserName = STR_TEAM_USERNAME[i];
                if (!Users.Any(u => u.UserName.Equals(UserName)))
                {
                    var teamMemberAccount = new ApplicationUser()
                    {
                        UserName = STR_TEAM_USERNAME[i],
                        Email = string.Format(STR_EMAIL_FORMAT, STR_TEAM_USERNAME[i])
                    };

                    this.Create(teamMemberAccount, STR_TEAM_PASSWORD[i]);

                    this.AddToRole(teamMemberAccount.Id, SecurityRoles.Admin);
                }
            }
        }

        public void AddDefaultUsers()
        {
            using (var context = new eBikesContext())
            {
                var employees = from x in context.Employees
                                select new EmployeeListPOCO
                                {
                                    EmployeeId = x.EmployeeID,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName
                                };
                var userEmployees = from x in Users.ToList()
                                    where x.EmployeeID.HasValue
                                    select new RegisteredEmployeePOCO
                                    {
                                        UserName = x.UserName,
                                        EmployeeId = int.Parse(x.EmployeeID.ToString())
                                    };

                foreach (var employee in employees)
                {
                    if (!userEmployees.Any(users => users.EmployeeId == employee.EmployeeId))
                    {
                        var newUserName = employee.FirstName.Substring(0, 1) + employee.LastName;

                        var userAccount = new ApplicationUser()
                        {
                            UserName = newUserName,
                            Email = string.Format(STR_EMAIL_FORMAT, newUserName),
                            EmailConfirmed = true
                        };

                        userAccount.EmployeeID = employee.EmployeeId;

                        IdentityResult result = this.Create(userAccount, STR_DEFAULT_PASSWORD);

                        if (!result.Succeeded)
                        {
                            newUserName = VerifyNewUserName(newUserName);
                            userAccount.UserName = newUserName;
                            this.Create(userAccount, STR_DEFAULT_PASSWORD);
                        }

                        this.AddToRole(userAccount.Id, SecurityRoles.Staff);
                    }
                }
            }
        }

        public string VerifyNewUserName(string suggestedUserName)
        {
            // get a list of all current user names (customers and employees)
            // that start with the suggested username
            // list of strings 
            // it will be in memory so that it can be used
            var allUserNames = from x in Users.ToList()
                               where x.UserName.StartsWith(suggestedUserName)
                               orderby x.UserName
                               select x.UserName;

            // set up the verified unique UserName
            var verifiedUserName = suggestedUserName;

            // the following for loop will continue to loop untill an unused UserName has been generated
            // the condition searches all current UserNames for the currently generated verifiedUserName (inside loop code)
            // if found the loop will generate a new verrified name based on the origional suggested username and the counter
            // This loop continues untill a unused UserName is found

            // OrdinalIgnoreCase : case no longer matters using this StringComparision
            for (int i = 1; allUserNames.Any(x => x.Equals(verifiedUserName, StringComparison.OrdinalIgnoreCase)); i++)
            {
                verifiedUserName = suggestedUserName + i.ToString();
            }

            return verifiedUserName;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UserProfile> ListAllUsers()
        {
            var rolemgr = new RoleManager();
            List<UserProfile> results = new List<UserProfile>();
            var tempResults = from person in Users.ToList()
                              select new UserProfile
                              {
                                  UserId = person.Id,
                                  UserName = person.UserName,
                                  Email = person.Email,
                                  EmailConfirmation = person.EmailConfirmed,
                                  EmployeeId = person.EmployeeID,
                                  CustomerId = person.CustomerID,
                                  RoleMemberships = person.Roles.Select(r => rolemgr.FindById(r.RoleId).Name)
                              };
            using (var context = new eBikesContext())
            {
                Employee tempEmployee;
                foreach(var person in tempResults)
                {
                    if(person.EmployeeId.HasValue)
                    {
                        tempEmployee = context.Employees.Find(person.EmployeeId);
                        if(tempEmployee != null)
                        {
                            person.FirstName = tempEmployee.FirstName;
                            person.LastName = tempEmployee.LastName;
                        }
                    }
                    results.Add(person);
                }
            }
            return results.ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddUser(UserProfile userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.EmployeeId.ToString()))
            {
                throw new Exception("Employee ID is missing. Remember Employee must be on file to get a user account.");
            }
            else
            {
                EmployeeController sysmgr = new EmployeeController();
                Employee exsisting = sysmgr.Employee_Get(int.Parse(userInfo.EmployeeId.ToString()));
                if (exsisting == null)
                {
                   throw new Exception("Employee must be on file to get a user account!");
                }
                else
                {
                    var userAccount = new ApplicationUser()
                    {
                        EmployeeID = userInfo.EmployeeId,
                        CustomerID = userInfo.CustomerId,
                        UserName = userInfo.UserName,
                        Email = userInfo.Email
                    };
                    IdentityResult result = this.Create(userAccount, string.IsNullOrEmpty(userInfo.RequestedPassword) ? STR_DEFAULT_PASSWORD : userInfo.RequestedPassword);
                    if(!result.Succeeded)
                    {
                        userAccount.UserName = VerifyNewUserName(userInfo.UserName);
                        this.Create(userAccount, STR_DEFAULT_PASSWORD);
                    }
                    foreach (var roleName in userInfo.RoleMemberships)
                    {
                        AddUserToRole(userAccount, roleName);
                    }
                }
            }
        }

        public void AddUserToRole(ApplicationUser userAccount, string roleName)
        {
            this.AddToRole(userAccount.Id, roleName);
        }

        public void RemoveUser(UserProfile userinfo)
        {
            this.Delete(this.FindById(userinfo.UserId));
        }
    }
}
