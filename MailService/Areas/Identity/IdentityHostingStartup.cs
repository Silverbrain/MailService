﻿using System;
using System.Threading.Tasks;
using MailService.Areas.Identity.Data;
using MailService.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(MailService.Areas.Identity.IdentityHostingStartup))]
namespace MailService.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MailServiceContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MailServiceContextConnection")));

                //services.AddDefaultIdentity<ApplicationUser>()
                //    .AddEntityFrameworkStores<MailServiceContext>();

                services.ConfigureApplicationCookie(x =>
                {
                    x.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = y =>
                        {
                            y.Response.Redirect("Account/Login");
                            return Task.CompletedTask;
                        }
                    };
                });
            });
        }
    }
}