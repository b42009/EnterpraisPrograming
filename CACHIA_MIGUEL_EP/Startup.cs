using System;
using CACHIA_MIGUEL_EP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CACHIA_MIGUEL_EP.Startup))]
namespace CACHIA_MIGUEL_EP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            creatRolesAndDefaultUsers();
        }

        private void creatRolesAndDefaultUsers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                using (RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                {
                    if (!roleManager.RoleExists("Admin"))
                    {
                        IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name="Admin";
                        roleManager.Create(role);
                     }
                    if (!roleManager.RoleExists("RegisteredUser"))
                    {
                        IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "RegisteredUser";
                        roleManager.Create(role);
                    }
                }
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    
                    if (userManager.FindByName("admin@yourEmailHost.com") == null)
                    {
                        // admin user does not exist - we can create it
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = "admin@yourEmailHost.com";
                        user.Email = "admin@yourEmailHost.com";

                        string userPWD = "P@ssw0rd_1234";


                        IdentityResult chkUser = userManager.Create(user, userPWD);

                        //Add the admin user to the Admin role, if it was successfully created
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = userManager.AddToRole(user.Id, "Admin");

                            if (!chkRole.Succeeded)
                            {
                                // admin user was not assigned to role, something went wrong!
                                // Log this information and handle it
                                Console.Error.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
                                Console.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
                            }
                        }
                        else
                        {   // admin user was not created, something went wrong!
                            // Log this information and handle it
                            Console.Error.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
                            Console.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
                        }
                    }
                }
            }
            
        }
    }
}
