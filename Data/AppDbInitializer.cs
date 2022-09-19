using A_Little_Extra_System.Models;
using A_Little_Extra_System.Data.Static;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace A_Little_Extra_System.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                // Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.Student))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Student));

                if (!await roleManager.RoleExistsAsync(UserRoles.DepartmentStaff))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.DepartmentStaff));

                if (!await roleManager.RoleExistsAsync(UserRoles.UniversityPartner))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.UniversityPartner));

                // Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                // Admin dummy user
                string adminUserEmail = "admin@alittleextra.com";
                var user = await userManager.FindByEmailAsync(adminUserEmail);
                if (user == null)
                {
                    var newAdminUser = new User()
                    {
                        FirstName = "Admin",
                        LastName = "User",
                        PatnerName="UniPatner",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                // Student dummy users
                string student1Email = "student1@alittleextra.com";
                user = await userManager.FindByEmailAsync(student1Email);
                if (user == null)
                {
                    var newAppUser = new User()
                    {
                        FirstName = "Student1",
                        LastName = "Mandela",
                        PatnerName="UniPatner",
                        UserName = "student-1",
                        Email = student1Email,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }

                string student2Email = "student2@alittleextra.com";
                user = await userManager.FindByEmailAsync(student2Email);
                if (user == null)
                {
                    var newAppUser = new User()
                    {
                        FirstName = "Student2",
                        LastName = "Mandela",
                        PatnerName="UniPatner",
                        UserName = "student-2",
                        Email = student2Email,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }

                // Department staff dummy user
                string staffEmail = "staff@alittleextra.com";
                user = await userManager.FindByEmailAsync(staffEmail);
                if (user == null)
                {
                    var newAppUser = new User()
                    {
                        FirstName = "Staff",
                        LastName = "Mandela",
                        PatnerName="UniPatner",
                        UserName = "staff-user",
                        Email = staffEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.DepartmentStaff);
                }


                // University partner dummy user
                string partnerEmail = "partner@alittleextra.com";
                user = await userManager.FindByEmailAsync(partnerEmail);
                if (user == null)
                {
                    var newAppUser = new User()
                    {
                        FirstName = "",
                        LastName = "",
                        PatnerName="UniPatner",
                        UserName = "patner-user",
                        Email = partnerEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.UniversityPartner);
                }

            }
        }
    }
}
