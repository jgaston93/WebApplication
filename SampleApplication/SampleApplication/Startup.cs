using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SampleApplication.Models;

[assembly: OwinStartupAttribute(typeof(SampleApplication.Startup))]
namespace SampleApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);



                //Here we create a Admin super user who will maintain the website                  

                var user1 = new ApplicationUser();
                user1.UserName = "Admin";
                user1.Email = "admin@admin.com";

                string user1PWD = "Admin1@3$";

                var chkUser1 = UserManager.Create(user1, user1PWD);

                //Add default User to Role Admin   
                if (chkUser1.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user1.Id, "Admin");

                }
            }
        }
    }
}
