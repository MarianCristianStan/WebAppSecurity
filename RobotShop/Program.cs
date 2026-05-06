using Microsoft.AspNetCore.Identity;
using RobotShop.Data;
using RobotShop.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProjectServices(builder.Configuration);
builder.Services.AddControllersWithViews();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Home/Error");
   app.UseHsts();
  
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

//await SeedRoles(app);

app.Run();

async Task SeedRoles(IHost app)
{
   using (var scope = app.Services.CreateScope())
   {
      var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      await ContextSeed.SeedRolesAsync(roleManager);
   }
}

