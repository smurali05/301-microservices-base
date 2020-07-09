using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    [ExcludeFromCodeCoverage]
    public partial class RestaurantManagementContext : DbContext
    {
        private readonly string DbConnectionString;
        public RestaurantManagementContext()
        {
        }

        public RestaurantManagementContext(DbContextOptions<RestaurantManagementContext> options)
            : base(options)
        {
        }

        public RestaurantManagementContext(string connectionstring)
        {
            DbConnectionString = connectionstring;
        }

        public virtual DbSet<TblRestaurantReview> TblRestaurantReview { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=tcp:capstoneteam1server.database.windows.net,1433;Initial Catalog=RestuarantReviewManagement;Persist Security Info=False;user id=cpadmin;password=Mindtree@12;");
                optionsBuilder.UseSqlServer(DbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TblRestaurantReview>(entity =>
            {
                entity.ToTable("TblRestaurantReview");

                entity.Property(e => e.TblReviewId);

                entity.Property(e => e.TblUserComments)
                  .IsRequired()
                  .HasDefaultValueSql("('')");

                entity.Property(e => e.TblRating)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TblCustomerId)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasDefaultValueSql("((0))");
            });
        }
    }
}

