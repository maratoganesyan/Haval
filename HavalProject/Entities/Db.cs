using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HavalProject.Entities
{
    public partial class Db : DbContext
    {
        public virtual DbSet<AdditationalServices> AdditationalServices { get; set; }
        public virtual DbSet<Body> Body { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarPhoto> CarPhoto { get; set; }
        public virtual DbSet<CarStatus> CarStatus { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<DriveType> DriveType { get; set; }
        public virtual DbSet<Engine> Engine { get; set; }
        public virtual DbSet<EngineType> EngineType { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<GoodsInSupply> GoodsInSupply { get; set; }
        public virtual DbSet<Models> Models { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderStatuses> OrderStatuses { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RudderSide> RudderSide { get; set; }
        public virtual DbSet<ServicesInOrders> ServicesInOrders { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<SupplyStatuses> SupplyStatuses { get; set; }
        public virtual DbSet<Transmission> Transmission { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Year> Year { get; set; }

        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        public Db()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-S7OLEVV\\SQLEXPRESS;" +
                    "Initial Catalog=Haval;Integrated Security=True;" +
                    "Trust Server Certificate=True;Command Timeout=300;" +
                    "MultipleActiveResultSets=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditationalServices>(entity =>
            {
                entity.HasKey(e => e.IdService);

                entity.Property(e => e.ServiceDescription).IsRequired();

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ServicePrice).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Body>(entity =>
            {
                entity.HasKey(e => e.IdBody);

                entity.Property(e => e.BodyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.IdCar);

                entity.Property(e => e.ClientPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EngineCapacity).HasColumnType("decimal(3, 1)");

                entity.HasOne(d => d.IdBodyNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdBody)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Body");

                entity.HasOne(d => d.IdCarStatusNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdCarStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_CarStatus");

                entity.HasOne(d => d.IdColorNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdColor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Color");

                entity.HasOne(d => d.IdConfigurationNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdConfiguration)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Configuration");

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Country");

                entity.HasOne(d => d.IdDriveTypeNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdDriveType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_DriveType");

                entity.HasOne(d => d.IdEngineNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdEngine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Engine");

                entity.HasOne(d => d.IdEngineTypeNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdEngineType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_EngineType");

                entity.HasOne(d => d.IdModelNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_ModelName");

                entity.HasOne(d => d.IdRudderSideNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdRudderSide)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_RudderSide");

                entity.HasOne(d => d.IdTransmissionNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdTransmission)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Transmission");

                entity.HasOne(d => d.IdYearNavigation)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.IdYear)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Year");
            });

            modelBuilder.Entity<CarPhoto>(entity =>
            {
                entity.HasKey(e => new { e.IdCar, e.PhotoNumber });

                entity.Property(e => e.Photo).IsRequired();

                entity.HasOne(d => d.IdCarNavigation)
                    .WithMany(p => p.CarPhoto)
                    .HasForeignKey(d => d.IdCar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarPhoto_Car");
            });

            modelBuilder.Entity<CarStatus>(entity =>
            {
                entity.HasKey(e => e.IdCarStatus);

                entity.Property(e => e.CarStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.IdCity);

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.IdColor);

                entity.Property(e => e.ColorDescription).IsRequired();

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.HEX)
                    .IsRequired()
                    .HasMaxLength(7);
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.HasKey(e => e.IdConfiguration);

                entity.Property(e => e.ConfigurationName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DriveType>(entity =>
            {
                entity.HasKey(e => e.IdDriveType);

                entity.Property(e => e.DriveTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Engine>(entity =>
            {
                entity.HasKey(e => e.IdEngineName);

                entity.Property(e => e.EngineName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EngineType>(entity =>
            {
                entity.HasKey(e => e.IdEngineType);

                entity.Property(e => e.EngineTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.IdGender);

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(7);
            });

            modelBuilder.Entity<GoodsInSupply>(entity =>
            {
                entity.HasKey(e => new { e.IdSupply, e.IdCar });

                entity.Property(e => e.SupplyPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdCarNavigation)
                    .WithMany(p => p.GoodsInSupply)
                    .HasForeignKey(d => d.IdCar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoodsInSupply_Car");

                entity.HasOne(d => d.IdSupplyNavigation)
                    .WithMany(p => p.GoodsInSupply)
                    .HasForeignKey(d => d.IdSupply)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoodsInSupply_Supplies");
            });

            modelBuilder.Entity<Models>(entity =>
            {
                entity.HasKey(e => e.IdModel)
                    .HasName("PK_ModelName");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.Property(e => e.DateTimeOfOrder).HasColumnType("smalldatetime");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdCarNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.IdCar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Car");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.OrderIdClientNavigation)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Users");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.OrderIdManagerNavigation)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Users1");

                entity.HasOne(d => d.IdOrderStatusNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.IdOrderStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderStatuses");
            });

            modelBuilder.Entity<OrderStatuses>(entity =>
            {
                entity.HasKey(e => e.IdOrderStatus);

                entity.Property(e => e.OrderStatusName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<RudderSide>(entity =>
            {
                entity.HasKey(e => e.IdRudderSide);

                entity.Property(e => e.RuddeSideName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ServicesInOrders>(entity =>
            {
                entity.HasKey(e => new { e.IdOrder, e.IdService });

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.ServicesInOrders)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicesInOrders_Order");

                entity.HasOne(d => d.IdServiceNavigation)
                    .WithMany(p => p.ServicesInOrders)
                    .HasForeignKey(d => d.IdService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicesInOrders_AdditationalServices");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.IdSupplier);

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(18);

                entity.HasOne(d => d.IdCityNavigation)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Suppliers_Cities");
            });

            modelBuilder.Entity<Supplies>(entity =>
            {
                entity.HasKey(e => e.IdSupply);

                entity.Property(e => e.DateTimeOfSupply).HasColumnType("smalldatetime");

                entity.Property(e => e.TotalPriceOfSupply).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdSupplierNavigation)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.IdSupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplies_Suppliers");

                entity.HasOne(d => d.IdSupplyStatusNavigation)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.IdSupplyStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplies_SupplyStatuses");
            });

            modelBuilder.Entity<SupplyStatuses>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Transmission>(entity =>
            {
                entity.HasKey(e => e.IdTransmission);

                entity.Property(e => e.TransmissionName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(18);

                entity.Property(e => e.Photo).IsRequired();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdGenderNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdGender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Gender");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<Year>(entity =>
            {
                entity.HasKey(e => e.IdYear);

                entity.Property(e => e.IdYear).ValueGeneratedNever();

                entity.Property(e => e.YearValue)
                    .IsRequired()
                    .HasMaxLength(4);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
