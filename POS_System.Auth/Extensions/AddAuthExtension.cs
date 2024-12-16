using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using POS_System.Auth.Models;
using POS_System.Auth.Services;
using POS_System.Auth.Settings;
using System;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace POS_System.Auth.Extensions
{
    public static class AddAuthExtension
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services, string connectionString, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWT").Get<JWTSettings>()
                ?? throw new Exception("Failed to find JWT Settings section");

            var lockoutSettings = configuration.GetSection("Lockout").Get<LockOutSettings>()
                ?? throw new Exception("Failed to find Lockout Settings section");

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

        //services.AddScoped<IPersonalDataProtector, DefaultPersonalDataProtector>();

        services.AddSingleton<IJWTSettings>(jwtSettings);

            services.AddScoped<IUserBuilder, UserBuilder>();

            // Register the ILookupProtectorKeyRing service
            //services.AddSingleton<ILookupProtectorKeyRing, LookupProtectorKeyRing>();

            services.AddIdentity<ApplicationUser, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;

                options.Stores.ProtectPersonalData = false; //Detta bör sättas till true vid senare skede

                options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;

                options.Lockout.MaxFailedAccessAttempts = lockoutSettings.MaxFailedAccessAttempts;
                options.Lockout.DefaultLockoutTimeSpan = lockoutSettings.DefaultLockoutTimeSpan;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = jwtSettings.ExpirationTime;
                options.SlidingExpiration = true;
                options.ClaimsIssuer = jwtSettings.Issuer;
            });

            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });



            var sp = services.BuildServiceProvider();

            Task.Run(async () =>
            {
                using var scope = sp.CreateScope();
                await ApplyMigrations(scope.ServiceProvider);
            }).Wait();

            Task.Run(async () =>
            {
                using var scope = sp.CreateScope();
                await AddDefaultRoles(scope.ServiceProvider);
            }).Wait();

            return services;
        }


        //Method to add default roles
        public static async Task AddDefaultRoles(IServiceProvider services)
        {
            try
            {
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                //Add default roles
                var roles = new List<string> { "SuperAdmin", "Admin", "User" };
                foreach (var role in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(role);
                    if (!roleExist)
                    {
                        var newRole = new Role
                        {
                            Name = role
                        };
                        await roleManager.CreateAsync(newRole);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add default roles", ex);
            }

        }

        public static async Task ApplyMigrations(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            var migrations = context.Database.GetMigrations();

            if (migrations.Count() > 0)
            {
                try
                {
                    await context.Database.MigrateAsync();

                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to apply migrations", ex);
                }
            }
        }
    }
}
