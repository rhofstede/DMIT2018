using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Data.Entity;
using WebApp.Models;
using ChinookSystem.BLL;
#endregion

namespace WebApp.Security
{
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Seed the roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });

            //roles taken from database - Employee.Title
            EmployeeController controller = new EmployeeController();
            List<string> employeeRoles = controller.Employees_GetTitles();
            foreach (var role in employeeRoles)
            {
                roleManager.Create(new IdentityRole { Name = role });
            }
            #endregion

            #region Seed the users
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail
            }, adminPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);

            //hard-coded new user
            string userPassword = ConfigurationManager.AppSettings["newUserPassword"];
          
            result = userManager.Create(new ApplicationUser
            {
                UserName = "HansenB",
                Email = "HansenB@hotmail.somewhere.ca",
                CustomerID = 4
            }, userPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName("HansenB").Id, "Customers");

            //to seed employees from employee table;
            //retrieve list<Employee> and use foreach to create 
            //usernames (lastname first initial, maybe number)
            //emails - find the email or add emails to the usernames (username + domain)
            //id - get the id of the employee
            //newuserpassword will be default password -they will change this themselves
            //on succeeded, role will come from employee record or position table
            #endregion

            // ... etc. ...

            base.Seed(context);
        }
    }
}