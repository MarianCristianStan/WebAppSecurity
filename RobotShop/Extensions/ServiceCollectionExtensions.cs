using System.Net.Security;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RobotShop.Models;
using RobotShop.Repositories;
using RobotShop.Repositories.Interfaces;
using RobotShop.Services;
using RobotShop.Services.Interfaces;

namespace RobotShop.Extensions
{
   public static class ServiceCollectionExtensions
   {
      public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
      {
         // Add controllers and views
         services.AddControllersWithViews();
         services.AddHttpContextAccessor();

         // Repository registrations
         services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<IUserAddressRepository, UserAddressRepository>();
         services.AddScoped<IProductRepository, ProductRepository>();
         services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
         services.AddScoped<ICartRepository, CartRepository>();
         services.AddScoped<ICartProductRepository, CartProductRepository>();
         services.AddScoped<IOrderRepository, OrderRepository>();
         services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();
         services.AddScoped<IOrderProductRepository, OrderProductRepository>();
         services.AddScoped<IFeatureRepository, FeatureRepository>();

         // Service registrations
         services.AddScoped<IUserService, UserService>();
         services.AddScoped<IUserAddressService, UserAddressService>();
         services.AddScoped<IProductService, ProductService>();
         services.AddScoped<IProductCategoryService, ProductCategoryService>();
         services.AddScoped<ICartService, CartService>();
         services.AddScoped<ICartProductService, CartProductService>();
         services.AddScoped<IOrderService, OrderService>();
         services.AddScoped<IProductFeatureService, ProductFeatureService>();
         services.AddScoped<IOrderProductService, OrderProductService>();
         services.AddScoped<IFeatureService, FeatureService>();
         services.AddScoped<IPdfService, PdfService>();
			services.AddScoped<ITFIDFSearchService, TFIDFSearchService>();
         services.AddScoped<ILuceneIndexService, LuceneIndexService>();


			// Authentication and Identity
			services.AddScoped<IAuthService, AuthService>();
         services.AddScoped<SignInManager<User>>();
         services.AddScoped<UserManager<User>>();

         // Add Razor Pages and Response Compression
         services.AddRazorPages();
         services.AddResponseCompression(options =>
         {
            options.EnableForHttps = true;
         });

         // Identity and database configuration
         services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<RobotShopContext>();

         services.AddDbContext<RobotShopContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("RobotShopDB")));

         // Identity options configuration
         services.Configure<IdentityOptions>(options =>
         {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 6;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.Stores.MaxLengthForKeys = 128;
         });

         return services;
      }
   }
}
