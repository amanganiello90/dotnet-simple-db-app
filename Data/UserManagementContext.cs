using dotnet_tutorial_app.Models;
using Microsoft.EntityFrameworkCore;

using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace dotnet_tutorial_app.Data {
    public class UserManagementContext : DbContext {

        public DbSet<UserManagement> Users { get; set; }

        public UserManagementContext (DbContextOptions<UserManagementContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            // Map table names
            modelBuilder.Entity<UserManagement> ().ToTable ("User");
            modelBuilder.Entity<UserManagement> (entity => {
                entity.HasKey (e => e.UserManagementID);

            });

            base.OnModelCreating (modelBuilder);
        }
    }
}