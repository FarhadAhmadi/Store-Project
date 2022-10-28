using Microsoft.EntityFrameworkCore;
using Store.Aplication.Interface.Contexts;
using Store.Aplication.Services.Users.Queries.GetRoles;
using Store.Aplication.Services.Users.Queries.GetUser;
using Store.Presentaion.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDatabaseContext, DatabaeContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
