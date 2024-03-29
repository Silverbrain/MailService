﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MailService.Models
{
    public class MailServiceContext : IdentityDbContext<ApplicationUser>
    {
        public MailServiceContext(DbContextOptions<MailServiceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
     
        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailFolder> MailFolders { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Flowchart> Flowcharts { get; set; }
    }
}
