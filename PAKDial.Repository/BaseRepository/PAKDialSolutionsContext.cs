using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PAKDial.Domains.DomainModels;

namespace PAKDial.Repository.BaseRepository
{
    public partial class PAKDialSolutionsContext : DbContext
    {
        public PAKDialSolutionsContext()
        {
        }

        public PAKDialSolutionsContext(DbContextOptions<PAKDialSolutionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveZone> ActiveZone { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AssignedEmpAreas> AssignedEmpAreas { get; set; }
        public virtual DbSet<AssignedEmpCategory> AssignedEmpCategory { get; set; }
        public virtual DbSet<BusinessTypes> BusinessTypes { get; set; }
        public virtual DbSet<CategoryTypes> CategoryTypes { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CityArea> CityArea { get; set; }
        public virtual DbSet<CompanyListingProfile> CompanyListingProfile { get; set; }
        public virtual DbSet<CompanyListingRating> CompanyListingRating { get; set; }
        public virtual DbSet<CompanyListings> CompanyListings { get; set; }
        public virtual DbSet<CompanyListingTimming> CompanyListingTimming { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeAddress> EmployeeAddress { get; set; }
        public virtual DbSet<EmployeeContact> EmployeeContact { get; set; }
        public virtual DbSet<HomeSecMainMenuCat> HomeSecMainMenuCat { get; set; }
        public virtual DbSet<HomeSectionCategory> HomeSectionCategory { get; set; }
        public virtual DbSet<ListingAddress> ListingAddress { get; set; }
        public virtual DbSet<ListingCategory> ListingCategory { get; set; }
        public virtual DbSet<ListingGallery> ListingGallery { get; set; }
        public virtual DbSet<ListingLandlineNo> ListingLandlineNo { get; set; }
        public virtual DbSet<ListingMobileNo> ListingMobileNo { get; set; }
        public virtual DbSet<ListingPackages> ListingPackages { get; set; }
        public virtual DbSet<ListingPaymentsMode> ListingPaymentsMode { get; set; }
        public virtual DbSet<ListingPremium> ListingPremium { get; set; }
        public virtual DbSet<ListingQueryCycle> ListingQueryCycle { get; set; }
        public virtual DbSet<ListingQueryTrack> ListingQueryTrack { get; set; }
        public virtual DbSet<ListingsBusinessTypes> ListingsBusinessTypes { get; set; }
        public virtual DbSet<ListingServices> ListingServices { get; set; }
        public virtual DbSet<ListingSocialMedia> ListingSocialMedia { get; set; }
        public virtual DbSet<ListingTypes> ListingTypes { get; set; }
        public virtual DbSet<MainMenuCategory> MainMenuCategory { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<OutSourceAdvertisment> OutSourceAdvertisment { get; set; }
        public virtual DbSet<PackagePrices> PackagePrices { get; set; }
        public virtual DbSet<PackageTransfer> PackageTransfer { get; set; }
        public virtual DbSet<PaymentModes> PaymentModes { get; set; }
        public virtual DbSet<PremiumManageStatus> PremiumManageStatus { get; set; }
        public virtual DbSet<PremiumStatus> PremiumStatus { get; set; }
        public virtual DbSet<RoleBasedPermission> RoleBasedPermission { get; set; }
        public virtual DbSet<RouteControls> RouteControls { get; set; }
        public virtual DbSet<SearchBehaviour> SearchBehaviour { get; set; }
        public virtual DbSet<SocialMediaModes> SocialMediaModes { get; set; }
        public virtual DbSet<StateProvince> StateProvince { get; set; }
        public virtual DbSet<SubMenuCategory> SubMenuCategory { get; set; }
        public virtual DbSet<TypeOfServices> TypeOfServices { get; set; }
        public virtual DbSet<UserBasedPermission> UserBasedPermission { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<VerificationTypes> VerificationTypes { get; set; }
        public virtual DbSet<VerifiedListing> VerifiedListing { get; set; }
        public virtual DbSet<Zones> Zones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=192.168.10.254;Database=PAKDialSolutions;user id=sa; password=123;Trusted_Connection=false;");
                // optionsBuilder.UseSqlServer("Server=192.168.10.194;Initial Catalog=PAKDialSolutions;user id=sa; password=PakServer1; Trusted_Connection=false;");
                //optionsBuilder.UseSqlServer("Server=WIN-8SIVGKPEMEI\\SQLEXPRESS;Initial Catalog=PakDialProduction;user id=sa; password=PakServer1; Trusted_Connection=false;");
                //Production  
                //optionsBuilder.UseSqlServer("Server=EC2AMAZ-DL1RN31;Initial Catalog=PakDialProduction;user id=sa; password=PakDial@123; Trusted_Connection=false;");
                //optionsBuilder.UseSqlServer("Server=3.6.157.161;Initial Catalog=PakDialProduction;user id=sa; password=PakDial@123; Trusted_Connection=false;");

                //Sara Connection String 
                // optionsBuilder.UseSqlServer("Server=DESKTOP-BBD64VB\\SQLEXPRESS;Initial Catalog=PakDialSolutions;user id=sa; password=123; Trusted_Connection=false;");

                //Zain Connection String 
                optionsBuilder.UseSqlServer("Server=DESKTOP-1UBUFOQ\\SQLEXPRESS;Initial Catalog=PakDialSolutions;user id=sa; password=12345; Trusted_Connection=false");
                //Fatima
              //optionsBuilder.UseSqlServer("Server=DESKTOP-19RRJQN\\SQLEXPRESS;Initial Catalog=PakDialSolutions;user id=sa; password=123; Trusted_Connection=false;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveZone>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CityAreaId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CityId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StateId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ZoneId).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.UserTypeId).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK_AspNetUsers_UserType");
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AssignedEmpAreas>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CityId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ZoneId).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<AssignedEmpCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BusinessTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CategoryTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CityLat)
                    .HasColumnName("City_Lat")
                    .HasMaxLength(50);

                entity.Property(e => e.CityLog)
                    .HasColumnName("City_Log")
                    .HasMaxLength(50);

                entity.Property(e => e.CityPopular).HasColumnName("City_Popular");

                entity.Property(e => e.CityStatus).HasColumnName("City_Status");

                entity.Property(e => e.CountryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_StateProvince");
            });

            modelBuilder.Entity<CityArea>(entity =>
            {
                entity.ToTable("City_Area");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AreaLat)
                    .HasColumnName("Area_Lat")
                    .HasMaxLength(50);

                entity.Property(e => e.AreaLong)
                    .HasColumnName("Area_Long")
                    .HasMaxLength(50);

                entity.Property(e => e.AreaPopular).HasColumnName("Area_Popular");

                entity.Property(e => e.AreaStatus).HasColumnName("Area_Status");

                entity.Property(e => e.CityId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CountryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GoogleLoc)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityArea)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_City_Area_City");
            });

            modelBuilder.Entity<CompanyListingProfile>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AnnualTurnOver)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BriefAbout).IsUnicode(false);

                entity.Property(e => e.Certification).IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LocationOverview).IsUnicode(false);

                entity.Property(e => e.NumberofEmployees)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductAndServices).IsUnicode(false);

                entity.Property(e => e.ProfessionalAssociation).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.CompanyListingProfile)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyListingProfile_CompanyListings1");
            });

            modelBuilder.Entity<CompanyListingRating>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(256);

                entity.Property(e => e.ImageRatingDir)
                    .HasColumnName("ImageRating_Dir")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageRatingUrl)
                    .HasColumnName("ImageRating_Url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RatingDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.CompanyListingRating)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyListingRating_CompanyListings");
            });

            modelBuilder.Entity<CompanyListings>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BannerImage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BannerImageUrl)
                    .HasColumnName("BannerImage_Url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Email).HasMaxLength(128);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ListingTypeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MetaTitle).HasMaxLength(255);

                entity.Property(e => e.RequestCounter).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Website).HasMaxLength(255);

                entity.HasOne(d => d.ListingType)
                    .WithMany(p => p.CompanyListings)
                    .HasForeignKey(d => d.ListingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyListings_ListingTypes");
            });

            modelBuilder.Entity<CompanyListingTimming>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DaysName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TimeFrom)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TimeTo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.CompanyListingTimming)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyListingTimming_CompanyListings");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

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
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.ReportingTo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

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

                entity.HasOne(d => d.CityArea)
                    .WithMany(p => p.EmployeeAddress)
                    .HasForeignKey(d => d.CityAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Address_City_Area");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeAddress)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Address_Employee");
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

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeContact)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Contact_Employee");
            });

            modelBuilder.Entity<HomeSecMainMenuCat>(entity =>
            {
                entity.ToTable("HomeSec_MainMenuCat");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.HomeSecCatId)
                    .HasColumnName("HomeSecCat_Id")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MainMenuCatId)
                    .HasColumnName("MainMenuCat_Id")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.HomeSecCat)
                    .WithMany(p => p.HomeSecMainMenuCat)
                    .HasForeignKey(d => d.HomeSecCatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeSec_MainMenuCat_HomeSectionCategory");

                entity.HasOne(d => d.MainMenuCat)
                    .WithMany(p => p.HomeSecMainMenuCat)
                    .HasForeignKey(d => d.MainMenuCatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeSec_MainMenuCat_MainMenuCategory");
            });

            modelBuilder.Entity<HomeSectionCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ListingAddress>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Area)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingAddress).IsUnicode(false);

                entity.Property(e => e.CityAreaId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CityId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CountryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LandMark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LatLogAddress)
                    .HasColumnName("Lat_Log_Address")
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StateId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StreetAddress).IsUnicode(false);

                entity.Property(e => e.UpdateBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.ListingAddress)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingAddress_City");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingAddress)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingAddress_CompanyListings");
            });

            modelBuilder.Entity<ListingCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MainCategoryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubCategoryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingCategory)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingCategory_CompanyListings");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.ListingCategory)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingCategory_SubMenuCategory");
            });

            modelBuilder.Entity<ListingGallery>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FileUrl)
                    .HasColumnName("File_Url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UploadDir)
                    .HasColumnName("Upload_Dir")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingGallery)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingGallery_CompanyListings");
            });

            modelBuilder.Entity<ListingLandlineNo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LandlineNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingLandlineNo)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingLandlineNo_CompanyListings");
            });

            modelBuilder.Entity<ListingMobileNo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingMobileNo)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingMobileNo_CompanyListings");
            });

            modelBuilder.Entity<ListingPackages>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ListingPaymentsMode>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ModeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingPaymentsMode)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingPaymentsMode_CompanyListings");

                entity.HasOne(d => d.Mode)
                    .WithMany(p => p.ListingPaymentsMode)
                    .HasForeignKey(d => d.ModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingPaymentsMode_PaymentModes");
            });

            modelBuilder.Entity<ListingPremium>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AccountNo)
                    .HasColumnName("Account_No")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ListingFrom).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ListingTo).HasColumnType("datetime");

                entity.Property(e => e.ModeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PackageId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.Discount).HasMaxLength(255)
                    .IsUnicode(false); 
                entity.Property(e => e.StatusId).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingPremium)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingPremium_CompanyListings");

                entity.HasOne(d => d.Mode)
                    .WithMany(p => p.ListingPremium)
                    .HasForeignKey(d => d.ModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingPremium_PaymentModes");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.ListingPremium)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingPremium_ListingPackages");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ListingPremium)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingPremium_PremiumStatus");
            });

            modelBuilder.Entity<ListingQueryCycle>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AreaId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CityId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubCatId).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ListingQueryTrack>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AuditName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            });

            modelBuilder.Entity<ListingsBusinessTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BusinessId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.ListingsBusinessTypes)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingsBusinessTypes_BusinessTypes");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingsBusinessTypes)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingsBusinessTypes_CompanyListings");
            });

            modelBuilder.Entity<ListingServices>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ServiceTypeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingServices)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingServices_CompanyListings");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.ListingServices)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingServices_TypeOfServices");
            });

            modelBuilder.Entity<ListingSocialMedia>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MediaId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SitePath)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingSocialMedia)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingSocialMedia_CompanyListings");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.ListingSocialMedia)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListingSocialMedia_SocialMediaModes");
            });

            modelBuilder.Entity<ListingTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MainMenuCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CatBannerImage).HasMaxLength(255);

                entity.Property(e => e.CatBannerImageUrl)
                    .HasColumnName("CatBannerImage_Url")
                    .HasMaxLength(255);

                entity.Property(e => e.CatFeatureImage).HasMaxLength(255);

                entity.Property(e => e.CatFeatureImageUrl)
                    .HasColumnName("CatFeatureImage_Url")
                    .HasMaxLength(255);

                entity.Property(e => e.CatViewCounts).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CategoryTypeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MetaDescription).HasMaxLength(255);

                entity.Property(e => e.MetaKeyword).HasMaxLength(255);

                entity.Property(e => e.MetaTitle).HasMaxLength(255);

                entity.Property(e => e.MobileDir)
                    .HasColumnName("Mobile_Dir")
                    .HasMaxLength(255);

                entity.Property(e => e.MobileUrl)
                    .HasColumnName("Mobile_Url")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.WebDir)
                    .HasColumnName("Web_Dir")
                    .HasMaxLength(255);

                entity.Property(e => e.WebUrl)
                    .HasColumnName("Web_Url")
                    .HasMaxLength(255);

                entity.HasOne(d => d.CategoryType)
                    .WithMany(p => p.MainMenuCategory)
                    .HasForeignKey(d => d.CategoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MainMenuCategory_CategoryTypes");
            });

            modelBuilder.Entity<Modules>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OutSourceAdvertisment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClickCounts).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageBtn)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.MobImagePath).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PackagePrices>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingPackageId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ListingPackage)
                    .WithMany(p => p.PackagePrices)
                    .HasForeignKey(d => d.ListingPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackagePrices_ListingPackages");
            });

            modelBuilder.Entity<PackageTransfer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.Property(e => e.AssignedFrom)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.AssignedTo)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.PremiumId).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Premium)
                    .WithMany(p => p.PackageTransfer)
                    .HasForeignKey(d => d.PremiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageTransfer_ListingPremium");
            });

            modelBuilder.Entity<PaymentModes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageDir)
                    .HasColumnName("Image_Dir")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("Image_Url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PremiumManageStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DesignationId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PremiumId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.StatusId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Premium)
                    .WithMany(p => p.PremiumManageStatus)
                    .HasForeignKey(d => d.PremiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PremiumManageStatus_ListingPremium");
            });

            modelBuilder.Entity<PremiumStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleBasedPermission>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RouteControlId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleBasedPermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleBasedPermission_AspNetRoles");

                entity.HasOne(d => d.RouteControl)
                    .WithMany(p => p.RoleBasedPermission)
                    .HasForeignKey(d => d.RouteControlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleBasedPermission_RouteControls");
            });

            modelBuilder.Entity<RouteControls>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Action)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Areas)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Controller)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.RouteControls)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RouteControls_Modules");
            });

            modelBuilder.Entity<SearchBehaviour>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AreaSearch)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LocationSearch)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SearchResults)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SocialMediaModes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ImageDir)
                    .HasColumnName("Image_Dir")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("Image_Url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CountryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StateProvince)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateProvince_Country");
            });

            modelBuilder.Entity<SubMenuCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryTypeId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MainCategoryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MetaDescription).HasMaxLength(255);

                entity.Property(e => e.MetaTitle).HasMaxLength(255);

                entity.Property(e => e.MobileDir)
                    .HasColumnName("Mobile_Dir")
                    .HasMaxLength(255);

                entity.Property(e => e.MobileUrl)
                    .HasColumnName("Mobile_Url")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ParentSubCategoryId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubBannerImage).HasMaxLength(255);

                entity.Property(e => e.SubBannerImageUrl)
                    .HasColumnName("SubBannerImage_Url")
                    .HasMaxLength(255);

                entity.Property(e => e.SubFeatureImage).HasMaxLength(255);

                entity.Property(e => e.SubFeatureImageUrl)
                    .HasColumnName("SubFeatureImage_Url")
                    .HasMaxLength(255);

                entity.Property(e => e.SubViewCount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.WebDir)
                    .HasColumnName("Web_Dir")
                    .HasMaxLength(255);

                entity.Property(e => e.WebUrl)
                    .HasColumnName("Web_Url")
                    .HasMaxLength(255);

                entity.HasOne(d => d.MainCategory)
                    .WithMany(p => p.SubMenuCategory)
                    .HasForeignKey(d => d.MainCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubMenuCategory_MainMenuCategory");
            });

            modelBuilder.Entity<TypeOfServices>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserBasedPermission>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RouteControlId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.RouteControl)
                    .WithMany(p => p.UserBasedPermission)
                    .HasForeignKey(d => d.RouteControlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserBasedPermission_RouteControls");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBasedPermission)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserBasedPermission_AspNetUsers");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VerificationTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ImageDir)
                    .HasColumnName("Image_Dir")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("Image_Url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VerifiedListing>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.VerificationId).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.VerifiedListing)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VerifiedListing_CompanyListings");

                entity.HasOne(d => d.Verification)
                    .WithMany(p => p.VerifiedListing)
                    .HasForeignKey(d => d.VerificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VerifiedListing_VerificationTypes");
            });

            modelBuilder.Entity<Zones>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });
        }
    }
}
