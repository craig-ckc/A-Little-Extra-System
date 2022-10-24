using A_Little_Extra_System.Models;
using A_Little_Extra_System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using A_Little_Extra_System.Data.Service;

var builder = WebApplication.CreateBuilder(args);

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Services configuration
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ISupervisorService,SupervisorService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Authentication and authorization
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed database
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

Task.Delay(new TimeSpan(1, 0, 0)).ContinueWith(o => {
    AppDbInitializer.ScoreParticipants(app).Wait();
    Console.WriteLine("Working!!");
});

app.Run();