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

        //public DbSet<SitPlanner.Models.AccessibilityRestriction> AccessibilityRestriction { get; set; }

        public DbSet<SitPlanner.Models.InviteeCategory> InviteeCategory { get; set; }

        //public DbSet<SitPlanner.Models.InviteeTable> InviteeTable { get; set; }

        //public DbSet<SitPlanner.Models.PersonalRestriction> PersonalRestriction { get; set; }

        //public DbSet<SitPlanner.Models.Login> Login { get; set; }

        public DbSet<SitPlanner.Models.Role> Role { get; set; }

        public DbSet<SitPlanner.Models.Table> Table { get; set; }

        public DbSet<SitPlanner.Models.User> User { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //Configure Invitee-Table Many-To-Many
        //    modelBuilder.Entity<InviteeTable>()
        //        .HasKey(it => new { it.InviteeId, it.TableId });
        //    modelBuilder.Entity<InviteeTable>()
        //        .HasOne(i => i.Invitee)
        //        .WithMany(it => it.InviteeTables)
        //        .HasForeignKey(i => i.InviteeId);
        //    modelBuilder.Entity<InviteeTable>()
        //        .HasOne(t => t.Table)
        //        .WithMany(it => it.InviteeTables)
        //        .HasForeignKey(t => t.TableId);

        //    //Configure Invitee-Category Many-To-Many
        //    modelBuilder.Entity<InviteeCategory>()
        //        .HasKey(ic => new { ic.InviteeId, ic.CategoryId });
        //    modelBuilder.Entity<InviteeCategory>()
        //        .HasOne(i => i.Invitee)
        //        .WithMany(ic => ic.InviteeCategories)
        //        .HasForeignKey(i => i.InviteeId);
        //    modelBuilder.Entity<InviteeCategory>()
        //        .HasOne(c => c.Category)
        //        .WithMany(ic => ic.InviteeCategories)
        //        .HasForeignKey(c => c.CategoryId);

        //    //Configure User-Event Many-To-Many
        //    modelBuilder.Entity<UserEvent>()
        //        .HasKey(ue => new { ue.UserId, ue.EventId });
        //    modelBuilder.Entity<UserEvent>()
        //        .HasOne(u => u.User)
        //        .WithMany(ue => ue.UserEvents)
        //        .HasForeignKey(u => u.UserId);
        //    modelBuilder.Entity<UserEvent>()
        //        .HasOne(e => e.Event)
        //        .WithMany(ue => ue.UserEvents)
        //        .HasForeignKey(e => e.EventId);

        //    //Configure Invitee-Restriction Many-To-Many
        //    modelBuilder.Entity<InviteeRestriction>()
        //        .HasKey(ir => new { ir.InviteeId, ir.RestrictionId });
        //    modelBuilder.Entity<InviteeRestriction>()
        //        .HasOne(i => i.Invitee)
        //        .WithMany(ir => ir.InviteeRestrictions)
        //        .HasForeignKey(i => i.InviteeId);
        //    modelBuilder.Entity<InviteeRestriction>()
        //        .HasOne(r => r.Restriction)
        //        .WithMany(ir => ir.InviteeRestrictions)
        //        .HasForeignKey(r => r.RestrictionId);

        //    //Configure Table-Restriction Many-To-Many
        //    modelBuilder.Entity<TableRestriction>()
        //        .HasKey(tr => new { tr.TableId, tr.RestrictionId });
        //    modelBuilder.Entity<TableRestriction>()
        //        .HasOne(t => t.Table)
        //        .WithMany(tr => tr.TableRestrictions)
        //        .HasForeignKey(t => t.TableId);
        //    modelBuilder.Entity<TableRestriction>()
        //        .HasOne(r => r.Restriction)
        //        .WithMany(tr => tr.TableRestrictions)
        //        .HasForeignKey(r => r.RestrictionId);

        //    //Configure Arrangement-Event One-To-Many
        //    modelBuilder.Entity<Arrangement>()
        //    .HasOne(e => e.Event)
        //    .WithMany(a => a.Arrangements)
        //    .HasForeignKey(e => e.EventId);
        //}
    }
}
