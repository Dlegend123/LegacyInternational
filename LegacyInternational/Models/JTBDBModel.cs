using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LegacyInternational.Models
{
    public partial class JTBDBModel : DbContext
    {
        public JTBDBModel()
            : base("name=JTBDBConnectionString")
        {
            this.Configuration.ProxyCreationEnabled = true;
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<airlinelist> airlinelists { get; set; }
        public virtual DbSet<airportlist> airportlists { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<bookcruise> bookcruises { get; set; }
        public virtual DbSet<bookflight> bookflights { get; set; }
        public virtual DbSet<cruiseline> cruiselines { get; set; }
        public virtual DbSet<cruiselist> cruiselists { get; set; }
        public virtual DbSet<cruiseroom> cruiserooms { get; set; }
        public virtual DbSet<flightlist> flightlists { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<portlist> portlists { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admin>()
                .Property(e => e.username)
                .IsFixedLength();

            modelBuilder.Entity<airlinelist>()
                .HasMany(e => e.flightlists)
                .WithRequired(e => e.airlinelist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<cruiselist>()
                .Property(e => e.cost)
                .IsFixedLength();

            modelBuilder.Entity<cruiselist>()
                .Property(e => e.cruise_length)
                .IsFixedLength();

            modelBuilder.Entity<flightlist>()
                .HasMany(e => e.bookflights)
                .WithRequired(e => e.flightlist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<location>()
                .HasMany(e => e.airportlists)
                .WithRequired(e => e.location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<location>()
                .HasMany(e => e.portlists)
                .WithRequired(e => e.location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .Property(e => e.contact_num)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .HasMany(e => e.bookflights)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);
        }
    }
}
