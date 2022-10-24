using A_Little_Extra_System.Models;
using A_Little_Extra_System.Data.Static;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace A_Little_Extra_System.Data
{
    public class AppDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

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

                string email = "Nyasha@alittleextra.com";
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = " Nyasha",
                        LastName = " Mhandu",
                        PatnerName = "",
                        UserName = "Nyasha",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,
                    };

                    await userManager.CreateAsync(newAppUser, "Nya08@");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }
                var actor = await userManager.FindByEmailAsync(email);

                context.Activity.AddRange(new List<Activity>(){
                    new Activity()
                    {
                            UserId = actor.Id,
                            Name = "Winter Drive",
                            Description = "Participants donate winter cloths, boots and blankets to old people's homes and orphanages. For more information, contact Gift on Nyasha@alittleextra.com",
                            Points = 5,
                            Active = false,
                            StartDate = DateTime.Today.AddDays(15),
                            EndDate = DateTime.Today.AddDays(20),
                        },
                        new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Winter Drive",
                            Description = "Participants donate winter cloths, boots and blankets to old people's homes and orphanages. For more information, contact Gift on Nyasha@alittleextra.com",
                            Points = 5,
                            Active = false,
                            StartDate = DateTime.Today.AddDays(15),
                            EndDate = DateTime.Today.AddDays(20),
                        },
                    });
                context.SaveChanges();


                email = "Charm@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = " Charmaine",
                        LastName = " Zhuwau",
                        PatnerName = "",
                        UserName = "Charmaine",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,
                    };

                    await userManager.CreateAsync(newAppUser, "Charm@1234");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }
                actor = await userManager.FindByEmailAsync(email);

                context.Activity.AddRange(new List<Activity>(){
                        new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Math Olympiad",
                            Description = "Math Olympiads are held at the university to recognize children who excel in math. The test is offered to all university students and it provides them with numerous benefits. For more details, email Charmaine at Charm@alittleextra.com",
                            Points = 5,
                            Active = true,
                            StartDate = DateTime.Today.AddDays(-2),
                            EndDate = DateTime.Today.AddDays(8),
                        },
                        new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Bread tags collection",
                            Description = "Participants will be collecting bread tags, even broken ones, and plastic lids. This makes a difference to our environment too as they are recycled. For more information, contact Charm on Charm@alittleextra.com",
                            Points = 5,
                            Active = true,
                            StartDate = DateTime.Today.AddDays(3),
                            EndDate = DateTime.Today.AddDays(7),
                        },
                    });
                context.SaveChanges();

                email = "Tash@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = " Tash",
                        LastName = " Mun",
                        PatnerName = "",
                        UserName = "Tash",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,
                    };

                    await userManager.CreateAsync(newAppUser, "Tash@1234");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }
                actor = await userManager.FindByEmailAsync(email);

                context.Activity.AddRange(new List<Activity>(){
                    new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Water saving campaign",
                            Description = "Participants enter in an essay competition about water saving. Participants with the most pleasing essay and a large following on the campaign are awarded a prize. For more information, contact Gift on Tash@alittleextra.com",
                            Points = 20,
                            Active = false,
                            StartDate = DateTime.Today.AddDays(-14),
                            EndDate = DateTime.Today.AddDays(-10),
                        },
                    });
                context.SaveChanges();


                email = "OldMutual@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = "",
                        LastName = "",
                        PatnerName = "Old Mutual",
                        UserName = "Old Mutual",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Mutual98$");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.UniversityPartner);
                }
                actor = await userManager.FindByEmailAsync(email);



                email = "Santam@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = "",
                        LastName = "",
                        PatnerName = "Santam",
                        UserName = "Santam",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,

                    };

                    await userManager.CreateAsync(newAppUser, "Santam01");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.UniversityPartner);
                }
                actor = await userManager.FindByEmailAsync(email);

                email = "Lynn@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = "Lynn",
                        LastName = "Goash",
                        PatnerName = "",
                        UserName = "Lynn",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,
                    };

                    await userManager.CreateAsync(newAppUser, "Goash50");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.DepartmentStaff);
                }
                actor = await userManager.FindByEmailAsync(email);

                // USER 07
                email = "Ben@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = "Ben",
                        LastName = "Bim",
                        PatnerName = "",
                        UserName = "Ben",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,

                    };

                    await userManager.CreateAsync(newAppUser, "BenBim");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }
                actor = await userManager.FindByEmailAsync(email);

                context.Activity.AddRange(new List<Activity>(){
                    new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Food drive",
                            Description = "Food drive is a program where participants donate food stuffs to homeless people and disadvantaged families. For more information, contact Tatenda on Ben@alittleextra.com",
                            Points = 10,
                            Active = true,
                            StartDate = DateTime.Today.AddDays(0),
                            EndDate = DateTime.Today.AddDays(5),
                        },
                    });
                context.SaveChanges();

                // USER 08
                email = "JClair@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = "Jean",
                        LastName = "Clair",
                        PatnerName = "",
                        UserName = "Jean",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,

                    };

                    await userManager.CreateAsync(newAppUser, "JC8905hj");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.DepartmentStaff);
                }
                actor = await userManager.FindByEmailAsync(email);

                // USER 09
                email = "student1@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = "Student1",
                        LastName = "Mandela",
                        PatnerName = "UniPatner",
                        UserName = "student-1",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }
                actor = await userManager.FindByEmailAsync(email);

                context.Activity.AddRange(new List<Activity>(){
                new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Sanitary wear drive",
                            Description = "Sanitary wear drive is a campaign to collect sanitary pads for girls in Port Elizabeth who cannot afford to buy it themselves, allowing them to continue their education without being exposed to hygienic risks, discomfort, shame or loss of dignity. For more information contact Craig at student1@alittleextra.com",
                            Points = 10,
                            Active = false,
                            StartDate = DateTime.Today.AddDays(-45),
                            EndDate = DateTime.Today.AddDays(-30),
                        },
                    });
                context.SaveChanges();

                // USER 10
                email = "student2@alittleextra.com";
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var newAppUser = new User()

                    {
                        ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                        FirstName = "Staff",
                        LastName = "Mandela",
                        PatnerName = "UniPatner",
                        UserName = "staff-user",
                        Email = email,
                        EmailConfirmed = true,
                        isActive = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.DepartmentStaff);
                }
                actor = await userManager.FindByEmailAsync(email);

                context.Activity.AddRange(new List<Activity>(){
                        new Activity()
                        {

                            UserId = actor.Id,
                            Name = "Online tutoring",
                            Description = "The Accounting 1 department is looking for student tutors. If you are interested, contact Nompilo on staff@alittleextra.com",
                            Points = 25,
                            Active = true,
                            StartDate = DateTime.Today.AddDays(12),
                            EndDate = DateTime.Today.AddDays(30),
                        },
                        new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Breast cancer awareness",
                            Description = "The breast cancer awareness is an activity where participants walk with banners and flyers, talking to women about breast cancer. For more information, contact Nompilo on staff@alittleextra.com",
                            Points = 15,
                            Active = true,
                            StartDate = DateTime.Today.AddDays(5),
                            EndDate = DateTime.Today.AddDays(10),
                        },
                        new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Plastic collection for recycling",
                            Description = "Plastic collection for recycling is an activity where participants collect bread plastics, empty plastic bottles and bottle lids for recycling purposes. for more information, contact Nompilo on staff@alittleextra.com",
                            Points = 5,
                            Active = true,
                            StartDate = DateTime.Today.AddDays(-2),
                            EndDate = DateTime.Today.AddDays(5),
                        },
                        new Activity()
                        {
                            UserId = actor.Id,
                            Name = "Ceanup campaign",
                            Description = "A cleanup campaign is a program where students and department staff can participate in. Activities included are removing rubbish from local waterways, wetlands, beaches, parks, forests, mountains, or bush lands. For more information contact Craig at student2@alittleextra.com",
                            Points = 10,
                            Active = false,
                            StartDate = DateTime.Today.AddDays(-30),
                            EndDate = DateTime.Today.AddDays(-25),
                        },
                    });
                context.SaveChanges();
            }
        }

        public static async Task ScoreParticipants(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                var activities = context.Activity.Where(a => a.Active == true).ToList();

                foreach (Activity activity in activities)
                {
                    if (DateTime.Today < activity.EndDate)
                    {
                        activity.Active = false;
                        context.Update(activity);
                        await context.SaveChangesAsync();

                        var participants = context.User.Where(n => n.ActivityParticipation.Any(m => m.ActivityId == activity.Id)).ToList();

                        foreach (User user in participants)
                        {
                            user.Points = user.Points + activity.Points;
                            context.Update(user);
                            await context.SaveChangesAsync();
                        }

                    }
                }
            }
        }

    }
}
