using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SitPlanner.Models;
using SitPlanner.Models.Enums;


namespace SitPlanner.Data
{
    public class SitPlannerContext : IdentityDbContext
    {
        public SitPlannerContext(DbContextOptions<SitPlannerContext> options)
            : base(options)
        {

        }

        public DbSet<SitPlanner.Models.Category> Category { get; set; }

        public DbSet<SitPlanner.Models.Invitee> Invitee { get; set; }

        public DbSet<SitPlanner.Models.Event> Event { get; set; }
        
        public DbSet<SitPlanner.Models.EventOption> EventOption { get; set; }

        public DbSet<SitPlanner.Models.AccessibilityRestriction> AccessibilityRestriction { get; set; }

        public DbSet<SitPlanner.Models.InviteeTable> InviteeTable { get; set; }

        public DbSet<SitPlanner.Models.PersonalRestriction> PersonalRestriction { get; set; }

        //public DbSet<SitPlanner.Models.Login> Login { get; set; }

        public DbSet<SitPlanner.Models.Role> Role { get; set; }

        public DbSet<SitPlanner.Models.Table> Table { get; set; }

        public DbSet<SitPlanner.Models.User> User { get; set; }

    }
}
