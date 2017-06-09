using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OmniPot.Data.Identity;
using OmniPot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using OmniPot.Common;
using OmniPot.Data.Models;

namespace OmniPot.Data
{
    public class KindDbContext : DbContext
    {
        private readonly Guid CurrentUserId;
        private readonly string connectionString; 

        private KindDbContext() { }

        public KindDbContext(string connectionString)
        {
            this.connectionString = connectionString; 
            var user = new SeedUserContext();
            CurrentUserId = user.UserId;             
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!string.IsNullOrEmpty(connectionString))
                builder.UseSqlServer(connectionString);
        }

        public KindDbContext(DbContextOptions<KindDbContext> options, IUserContext userContext) : base(options)
        {
            CurrentUserId = userContext.UserId;
        }

        public KindDbContext(DbContextOptions<KindDbContext> options) : this(options, new SeedUserContext())
        {
        }

        public DbSet<AppConnection> Connections { get; set; }

        public DbSet<AppMessage> Messages { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<StateOrProvince> StatesOrProvinces { get;set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<InventoryItem> InventoryItems { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

        public DbSet<UploadDocument> UploadDocuments { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<TaxGroup> TaxGroups { get; set; }

        public DbSet<TaxGroupItem> TaxGroupItems { get; set; }

        public DbSet<TaxItem> TaxItems { get; set; }

        public DbSet<TestDefinition> TestDefinitions { get; set; }

        public DbSet<TestRule> TestRules { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<Batch> Batches { get; set; }

        public DbSet<Lot> Lots { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<PlantTag> PlantTags { get; set; }

        public DbSet<PlantTagOrder> PlantTagOrders { get; set; }

        public DbSet<PlantTagOrderItem> PlantTagOrderItems { get; set; }
        //This is a lookup only, it's an extract that comes from RI for license lookups
        public DbSet<StateLicense> StateLicenses { get; set; }

        public IQueryable<PlantTag> AvailablePlantTags
        {
            get
            {
                return PlantTags.Include(t => t.ParentRfidTag).Where(t => t.State == TrackableEntityState.IsActive && t.IssuedUtc == null && t.ParentRfidTag.DestroyedUtc == null);
            }
        }
        
        public override int SaveChanges()
        {
            UpdateTimestamps();
            
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateTimestamps(); 

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateTimestamps()
        {
            //Here is where we ensure that the end user never mucks with these 
            var trackables = ChangeTracker.Entries<ITrackableEntity>();
            
            //inserted entities.. 
            foreach (var item in trackables.Where(t => t.State == EntityState.Added))
            {
                item.Entity.State = TrackableEntityState.IsActive;
                item.Entity.CreatedUtc = DateTime.UtcNow;
                item.Entity.ModifiedUtc = DateTime.UtcNow;
                item.Entity.CreatedByUserId = CurrentUserId;
                item.Entity.ModifiedByUserId = CurrentUserId; 
            }

            //updated entities 
            foreach (var item in trackables.Where(t => t.State == EntityState.Modified))
            {
                item.Entity.ModifiedUtc = DateTime.UtcNow;
                item.Entity.ModifiedByUserId = CurrentUserId;
                 
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            //Tenant display names and route names need to be unique across the app
            builder.Entity<Tenant>()
                .HasAlternateKey(t => t.DisplayName)                
                .HasName("AK_Tenant_DisplayName");

            builder.Entity<Tenant>()
                .HasAlternateKey(t => t.RouteName)
                .HasName("AK_Tenant_RouteName");

            //Location display names and route names need to be unique to the tenant
            builder.Entity<Location>()
                .HasAlternateKey(t => new { t.TenantId, t.DisplayName })                
                .HasName("AK_Location_TenantId_DisplayName");

            builder.Entity<Location>()
                .HasAlternateKey(t => new { t.TenantId, t.RouteName })
                .HasName("AK_Location_TenantId_RouteName");

            builder.Entity<Batch>().HasAlternateKey(t => new { t.LocationId, t.BarCode })
                .HasName("AK_Batch_LocationId_BarCode");

            builder.Entity<Lot>().HasAlternateKey(t => new { t.CurrentLocationId, t.BarCode })
                .HasName("AK_Lot_CurrentLocationId_BarCode");

            builder.Entity<Location>(entity =>
            {
                entity
                .HasMany(e => e.Children)
                .WithOne(e => e.ParentLocation)
                .HasForeignKey(e => e.ParentLocationId)
                .HasConstraintName("FK_Location_SubLocation");
            });

            //builder.Entity<ItemType>()
            //    .HasAlternateKey(t => new { t.DisplayName, t.ParentItemTypeId })
            //    .HasName("AK_ItemType_DisplayName_ParentItem");

            builder.Entity<ItemType>(entity =>
            {
                entity
                .HasMany(e => e.Children)
                .WithOne(e => e.ParentType)
                .HasForeignKey(e => e.ParentItemTypeId)
                .HasConstraintName("FK_ItemType_ParentItemType")
                .IsRequired(false);
            });

            builder.Entity<TaxItem>().Property(e => e.Rate).ForSqlServerHasColumnType("decimal(7,5)");
            builder.Entity<TaxGroupItem>().HasKey(t => new { t.TaxItemId, t.TaxGroupId});
            builder.Entity<TaxGroupItem>().HasOne(e => e.TaxGroup).WithMany(e => e.TaxGroupItems).HasForeignKey(e => e.TaxGroupId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            builder.Entity<TaxGroupItem>().HasOne(e => e.TaxItem).WithMany(e => e.TaxGroupItems).HasForeignKey(e => e.TaxItemId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<InventoryItem>().Property(e => e.TraceableQuantity).ForSqlServerHasColumnType("decimal(18,4)");
            builder.Entity<InventoryItem>().Property(e => e.PurchaseCost).ForSqlServerHasColumnType("decimal(18,2)");
            builder.Entity<InventoryItem>().Property(e => e.StaticPrice).ForSqlServerHasColumnType("decimal(18,2)");
            builder.Entity<InventoryItem>().HasOne(e => e.Tenant).WithMany(e => e.InventoryItems).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<TestResult>().Property(e => e.Result).ForSqlServerHasColumnType("decimal(18,4)");
            builder.Entity<Lot>().Property(e => e.Weight).ForSqlServerHasColumnType("decimal(18,5)");
            //TODO: Currently i'm just adding these to apease EF. We need to look at these relationships and determine best way to nail this.
            builder.Entity<Batch>().HasOne(e => e.Location).WithMany(e => e.Batches).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            builder.Entity<Batch>().Ignore(e => e.Quantity);
            builder.Entity<Lot>().HasOne(e => e.CurrentLocation).WithMany(e => e.Lots).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            builder.Entity<Lot>().Ignore(e => e.Weight);
            builder.Entity<Vendor>().HasOne<Tenant>().WithMany(e => e.Vendors).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            builder.Entity<Client>().HasOne<Tenant>().WithMany(e => e.Clients).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            
            //builder.Entity<TaxItem>().HasOne<Tenant>().WithMany(e => e.TaxItems).OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Restrict);           
            builder.Entity<PlantTag>().HasKey(t => t.PlantTagId);
            builder.Entity<RfidTag>().HasKey(t => t.RfidTagId);

            builder.Entity<Person>(entity =>
            {
                entity.HasOne(e => e.GrowAddress).WithOne().IsRequired(false).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict); 
            });

            base.OnModelCreating(builder);
        }
    }
}
