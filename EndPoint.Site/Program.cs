using Bugeto_Store.Application.Services.Users.Commands.UserLogin;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Store.Aplication.Interface.Contexts;
using Store.Aplication.Interface.FacadPatterns;
using Store.Aplication.Services.Products.FacadPattern;
using Store.Aplication.Services.Users.Commands.ChangeUserStatus;
using Store.Aplication.Services.Users.Commands.EditUser;
using Store.Aplication.Services.Users.Commands.RegisterUser;
using Store.Aplication.Services.Users.Commands.RemoveUser;
using Store.Aplication.Services.Users.Queries.GetRoles;
using Store.Aplication.Services.Users.Queries.GetUser;
using Store.Presentaion.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Authentication/Signin");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
    options.AccessDeniedPath = new PathString("/Authentication/Signin");
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDatabaseContext, DatabaeContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IChangeUserStatusService, ChangeUserStatusService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();

//Inject Product facad
builder.Services.AddScoped<iproductFacad, productFacad>();


string ConnectionString = "Data Source=.;initial catalog = StoreDb  ; integrated security = true ";
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DatabaeContext>(a => a.UseSqlServer(ConnectionString));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
