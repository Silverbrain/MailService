using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Areas.Identity.Data;
using MailService.Models;
using MailService.Services.Classes;
using MailService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(x =>
            {
                x.Password.RequiredLength = 1;
                x.Password.RequireDigit = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.User.RequireUniqueEmail = true;
                x.Lockout.AllowedForNewUsers = true;
                x.Lockout.MaxFailedAccessAttempts = 5;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.AddHttpContextAccessor();

            // dependency injection section
            services.AddSingleton<IMailTransferService, MailTransferService>();
            services.AddTransient<IMailTransferService, MailTransferService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MailServiceContext>()
                .AddDefaultTokenProviders();
            
            services.AddDbContext<MailServiceContext>();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<MailServiceContext>().Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseHttpMethodOverride();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Mail}/{action=Inbox}/{id?}");
            });
            app.UseCookiePolicy();

            CreateRolesAsync(serviceProvider).Wait();
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin" };
            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);

                if(!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var _adminUser = await _userManager.FindByNameAsync("Admin");
            if(_adminUser == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@admin.com",
                    Folders = new List<Folder>() {
                        new Folder() {Name = "Inbox"},
                        new Folder() {Name = "Sent"},
                        new Folder() {Name = "Drafts"},
                        new Folder() {Name = "Favorites"},
                        new Folder() {Name = "Trash"}
                    }
                };

                var createAdmin = await _userManager.CreateAsync(adminUser, "Admin");
                if (createAdmin.Succeeded)
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
