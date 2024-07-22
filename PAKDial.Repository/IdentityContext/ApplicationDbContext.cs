using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Repository.IdentityContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DesignationId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PassportNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.ZoneManagerId).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Designation");
            });

            modelBuilder.Entity<EmployeeAddress>(entity =>
            {
                entity.ToTable("Employee_Address");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CityAreaId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CityId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CountryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmpAddress)
                    .HasColumnName("Emp_Address")
                    .HasMaxLength(500);

                entity.Property(e => e.EmployeeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProvinceId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            });

            modelBuilder.Entity<EmployeeContact>(entity =>
            {
                entity.ToTable("Employee_Contact");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });
            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.ReportingTo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeAddress> EmployeeAddress { get; set; }
        public virtual DbSet<EmployeeContact> EmployeeContact { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }

    }
}
