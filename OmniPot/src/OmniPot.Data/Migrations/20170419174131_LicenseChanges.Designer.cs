using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OmniPot.Data;

namespace OmniPot.Data.Migrations
{
    [DbContext(typeof(KindDbContext))]
    [Migration("20170419174131_LicenseChanges")]
    partial class LicenseChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OmniPot.Data.Models.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Addressee")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DeliveryLine1")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("DeliveryLine2")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<Guid?>("StateOrProvinceId")
                        .IsRequired();

                    b.HasKey("AddressId");

                    b.HasIndex("StateOrProvinceId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("OmniPot.Data.Models.AppConnection", b =>
                {
                    b.Property<Guid>("AppConnectionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConnectionId");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("Location");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<string>("Tenant");

                    b.Property<Guid>("UserId");

                    b.Property<string>("Username");

                    b.HasKey("AppConnectionId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("OmniPot.Data.Models.AppMessage", b =>
                {
                    b.Property<Guid>("AppMessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ConnectionId");

                    b.Property<string>("Content");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("Location");

                    b.Property<int>("MessageType");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<string>("Tenant");

                    b.Property<Guid?>("UserId");

                    b.Property<string>("Username");

                    b.HasKey("AppMessageId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Batch", b =>
                {
                    b.Property<Guid>("BatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BarCode")
                        .IsRequired();

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<DateTime>("ExpiryDateUtc");

                    b.Property<Guid>("InventoryItemId");

                    b.Property<Guid>("LocationId");

                    b.Property<DateTime>("ManufactureDateUtc");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("BatchId");

                    b.HasAlternateKey("LocationId", "BarCode")
                        .HasName("AK_Batch_LocationId_BarCode");

                    b.HasIndex("InventoryItemId");

                    b.HasIndex("LocationId");

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessPhone");

                    b.Property<string>("CellPhone");

                    b.Property<int>("ClientType");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<Guid>("DefaultSaleLocationId");

                    b.Property<Guid?>("DeliveryAddressId");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("Gender");

                    b.Property<bool>("IsTaxExempt");

                    b.Property<bool>("IsTempStatus");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid?>("MailingAddressId");

                    b.Property<string>("MiddleName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("NickName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("PrivateNotes")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("PrivateNotesExtended");

                    b.Property<string>("PublicNotes")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<Guid>("TenantId");

                    b.HasKey("ClientId");

                    b.HasIndex("DefaultSaleLocationId");

                    b.HasIndex("DeliveryAddressId");

                    b.HasIndex("MailingAddressId");

                    b.HasIndex("TenantId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Contact", b =>
                {
                    b.Property<Guid>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClientId");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("EmailAddress");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("PhoneNumber");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid?>("VendorId");

                    b.HasKey("ContactId");

                    b.HasIndex("ClientId");

                    b.HasIndex("VendorId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Country", b =>
                {
                    b.Property<Guid>("CountryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasAnnotation("MaxLength", 5);

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<int>("CompensationType");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<DateTime>("DateOfHire");

                    b.Property<DateTime>("DateOfTermination");

                    b.Property<string>("FirstName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("IsDriver");

                    b.Property<string>("LastName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("LicenseExpiry");

                    b.Property<string>("LicenseNumber")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("MiddleName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("MobilePhone");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("OtherPhone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<int>("Status");

                    b.Property<Guid?>("SupervisorId");

                    b.Property<Guid>("TenantId");

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("UserId");

                    b.Property<Guid?>("VehicleId");

                    b.HasKey("EmployeeId");

                    b.HasIndex("AddressId");

                    b.HasIndex("SupervisorId");

                    b.HasIndex("TenantId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("OmniPot.Data.Models.InventoryItem", b =>
                {
                    b.Property<Guid>("InventoryItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClassificationType");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid?>("ItemCategoryId");

                    b.Property<DateTime?>("ItemExpiryDate");

                    b.Property<Guid?>("ItemGroupId");

                    b.Property<Guid?>("ItemTypeId");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("Notes")
                        .HasAnnotation("MaxLength", 144);

                    b.Property<decimal>("PurchaseCost")
                        .HasAnnotation("SqlServer:ColumnType", "decimal(18,2)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SalesNotes");

                    b.Property<int>("State");

                    b.Property<decimal>("StaticPrice")
                        .HasAnnotation("SqlServer:ColumnType", "decimal(18,2)");

                    b.Property<Guid>("TaxGroupId");

                    b.Property<Guid>("TenantId");

                    b.Property<decimal>("TraceableQuantity")
                        .HasAnnotation("SqlServer:ColumnType", "decimal(18,4)");

                    b.Property<Guid?>("UnitOfMeasureId");

                    b.Property<string>("UpcSku")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<Guid?>("VendorId");

                    b.Property<Guid?>("WholesaleTaxGroupId");

                    b.HasKey("InventoryItemId");

                    b.HasIndex("ItemCategoryId");

                    b.HasIndex("ItemGroupId");

                    b.HasIndex("ItemTypeId");

                    b.HasIndex("TaxGroupId");

                    b.HasIndex("TenantId");

                    b.HasIndex("UnitOfMeasureId");

                    b.HasIndex("VendorId");

                    b.HasIndex("WholesaleTaxGroupId");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("OmniPot.Data.Models.ItemType", b =>
                {
                    b.Property<Guid>("ItemTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<Guid?>("ParentItemTypeId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("ItemTypeId");

                    b.HasIndex("ParentItemTypeId");

                    b.ToTable("ItemTypes");
                });

            modelBuilder.Entity("OmniPot.Data.Models.License", b =>
                {
                    b.Property<Guid>("LicenseId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<int>("ExpiryMonth");

                    b.Property<int>("ExpiryYear");

                    b.Property<string>("LicenseNumber");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<Guid?>("PersonId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("LicenseId");

                    b.HasIndex("PersonId");

                    b.ToTable("License");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<int>("ClassificationType");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid?>("EmployeeId");

                    b.Property<bool>("IsSalable");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<Guid?>("ParentLocationId");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<Guid>("TenantId");

                    b.HasKey("LocationId");

                    b.HasAlternateKey("TenantId", "DisplayName")
                        .HasName("AK_Location_TenantId_DisplayName");


                    b.HasAlternateKey("TenantId", "RouteName")
                        .HasName("AK_Location_TenantId_RouteName");

                    b.HasIndex("AddressId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ParentLocationId");

                    b.HasIndex("TenantId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Lot", b =>
                {
                    b.Property<Guid>("LotId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BarCode")
                        .IsRequired();

                    b.Property<Guid>("BatchId");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<Guid>("CurrentLocationId");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("Notes")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<int>("Quantity");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<Guid>("UnitOfMeasureId");

                    b.HasKey("LotId");

                    b.HasAlternateKey("CurrentLocationId", "BarCode")
                        .HasName("AK_Lot_CurrentLocationId_BarCode");

                    b.HasIndex("BatchId");

                    b.HasIndex("CurrentLocationId");

                    b.HasIndex("UnitOfMeasureId");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("OmniPot.Data.Models.PaymentTransaction", b =>
                {
                    b.Property<Guid>("PaymentTransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("AuthCode");

                    b.Property<string>("AvsResponse");

                    b.Property<string>("CardType");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("CvvResponse");

                    b.Property<string>("LastFour");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("OrderId");

                    b.Property<Guid?>("PersonId");

                    b.Property<Guid>("PlantTagOrderId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<string>("Token");

                    b.Property<DateTime>("TransactionUtc");

                    b.HasKey("PaymentTransactionId");

                    b.HasIndex("PersonId");

                    b.ToTable("PaymentTransaction");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<int>("ApplicationStatus");

                    b.Property<DateTime?>("ApprovedUtc");

                    b.Property<string>("CompanyName");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("CurrentOccupation");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("DeniedReason");

                    b.Property<DateTime?>("DeniedUtc");

                    b.Property<int>("EducationStatus");

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<int>("EmploymentStatus");

                    b.Property<int>("Ethnicity");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.Property<Guid?>("GrowAddressAddressId");

                    b.Property<Guid>("GrowAddressId");

                    b.Property<bool>("HasDohDiscount");

                    b.Property<bool>("IsAlsoPatient");

                    b.Property<bool>("IsCitizen");

                    b.Property<bool>("IsCurrentlyEnrolled");

                    b.Property<bool>("IsGrowAddressCooperative");

                    b.Property<bool>("IsVeteran");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int>("MaritalStatus");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<Guid?>("PatientLicenseId");

                    b.Property<int>("PersonType");

                    b.Property<int>("Race");

                    b.Property<int>("RequestedTagCount");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("PersonId");

                    b.HasIndex("AddressId");

                    b.HasIndex("GrowAddressAddressId")
                        .IsUnique();

                    b.HasIndex("PatientLicenseId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Phone", b =>
                {
                    b.Property<Guid>("PhoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<Guid?>("PersonId");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("PhoneType");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("PhoneId");

                    b.HasIndex("PersonId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("OmniPot.Data.Models.PlantTag", b =>
                {
                    b.Property<Guid>("PlantTagId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<DateTime?>("ExpiryUtc");

                    b.Property<DateTime?>("IssuedUtc");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<Guid>("ParentRfidTagId");

                    b.Property<Guid?>("PersonId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("PlantTagId");

                    b.HasIndex("AddressId");

                    b.HasIndex("ParentRfidTagId");

                    b.HasIndex("PersonId");

                    b.ToTable("PlantTags");
                });

            modelBuilder.Entity("OmniPot.Data.Models.PlantTagOrder", b =>
                {
                    b.Property<Guid>("PlantTagOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<DateTime>("OrderDateUtc");

                    b.Property<Guid>("PersonId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<int>("TagQuantity");

                    b.Property<decimal>("Tax");

                    b.Property<string>("Token");

                    b.HasKey("PlantTagOrderId");

                    b.ToTable("PlantTagOrders");
                });

            modelBuilder.Entity("OmniPot.Data.Models.PlantTagOrderItem", b =>
                {
                    b.Property<Guid>("PlantTagOrderItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<Guid>("PlantTagOrderId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Sku");

                    b.Property<int>("State");

                    b.Property<int>("TagType");

                    b.Property<decimal>("TotalAmount");

                    b.HasKey("PlantTagOrderItemId");

                    b.HasIndex("PlantTagOrderId");

                    b.ToTable("PlantTagOrderItems");
                });

            modelBuilder.Entity("OmniPot.Data.Models.RfidTag", b =>
                {
                    b.Property<Guid>("RfidTagId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DestroyedUtc");

                    b.Property<Guid?>("ReplacedById");

                    b.Property<DateTime?>("ReplacedUtc");

                    b.Property<byte[]>("TagData");

                    b.HasKey("RfidTagId");

                    b.ToTable("RfidTag");
                });

            modelBuilder.Entity("OmniPot.Data.Models.ServiceRecord", b =>
                {
                    b.Property<Guid>("ServiceRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<int>("CurrentMilage");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<DateTime>("NextServiceDate");

                    b.Property<string>("Notes")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("ServiceDate");

                    b.Property<int>("ServiceType");

                    b.Property<int>("State");

                    b.Property<Guid>("VehicleId");

                    b.Property<Guid?>("VendorId");

                    b.HasKey("ServiceRecordId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ServiceRecord");
                });

            modelBuilder.Entity("OmniPot.Data.Models.StateLicense", b =>
                {
                    b.Property<Guid>("StateLicenseId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CaretakerExpiry");

                    b.Property<bool>("CaretakerIsMedicaid");

                    b.Property<DateTime>("CaretakerIssueDate");

                    b.Property<string>("CaretakerLicenseNumber");

                    b.Property<DateTime>("Expiry");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDisabledVeteran");

                    b.Property<bool>("IsHospice");

                    b.Property<bool>("IsMedicaid");

                    b.Property<bool>("IsSsi");

                    b.Property<DateTime>("IssueDate");

                    b.Property<string>("LastName");

                    b.Property<string>("LicenseNumber");

                    b.Property<string>("MiddleName");

                    b.Property<int>("Status");

                    b.HasKey("StateLicenseId");

                    b.ToTable("StateLicenses");
                });

            modelBuilder.Entity("OmniPot.Data.Models.StateOrProvince", b =>
                {
                    b.Property<Guid>("StateOrProvinceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasAnnotation("MaxLength", 5);

                    b.Property<Guid>("CountryId");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("StateOrProvinceId");

                    b.HasIndex("CountryId");

                    b.ToTable("StatesOrProvinces");
                });

            modelBuilder.Entity("OmniPot.Data.Models.TaxGroup", b =>
                {
                    b.Property<Guid>("TaxGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayColor")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("IsAppliedBeforeDiscount");

                    b.Property<Guid>("LocationId");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("TaxGroupId");

                    b.HasIndex("LocationId");

                    b.ToTable("TaxGroups");
                });

            modelBuilder.Entity("OmniPot.Data.Models.TaxGroupItem", b =>
                {
                    b.Property<Guid>("TaxItemId");

                    b.Property<Guid>("TaxGroupId");

                    b.HasKey("TaxItemId", "TaxGroupId");

                    b.HasIndex("TaxGroupId");

                    b.HasIndex("TaxItemId");

                    b.ToTable("TaxGroupItems");
                });

            modelBuilder.Entity("OmniPot.Data.Models.TaxItem", b =>
                {
                    b.Property<Guid>("TaxItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgencyName");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<decimal>("Rate")
                        .HasAnnotation("SqlServer:ColumnType", "decimal(7,5)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<Guid>("TenantId");

                    b.HasKey("TaxItemId");

                    b.ToTable("TaxItems");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Tenant", b =>
                {
                    b.Property<Guid>("TenantId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("CssOverrides");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<string>("Theme")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("UseLots");

                    b.HasKey("TenantId");

                    b.HasAlternateKey("DisplayName")
                        .HasName("AK_Tenant_DisplayName");


                    b.HasAlternateKey("RouteName")
                        .HasName("AK_Tenant_RouteName");

                    b.HasIndex("AddressId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("OmniPot.Data.Models.TestDefinition", b =>
                {
                    b.Property<Guid>("TestDefinitionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("DisplayOrder");

                    b.Property<bool?>("IsPassFail");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<bool>("RequiresDecimal");

                    b.Property<bool>("RequiresNote");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("TestDefinitionId");

                    b.ToTable("TestDefinitions");
                });

            modelBuilder.Entity("OmniPot.Data.Models.TestResult", b =>
                {
                    b.Property<Guid>("TestResultId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("HasPassed");

                    b.Property<Guid?>("LotId");

                    b.Property<decimal>("Result")
                        .HasAnnotation("SqlServer:ColumnType", "decimal(18,4)");

                    b.Property<string>("ResultNotes")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("StrainName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<DateTime>("TestDate");

                    b.Property<Guid>("TestDefinitionId");

                    b.HasKey("TestResultId");

                    b.HasIndex("LotId");

                    b.HasIndex("TestDefinitionId");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("OmniPot.Data.Models.TestRule", b =>
                {
                    b.Property<Guid>("TestRuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRequired");

                    b.Property<Guid>("LocationId");

                    b.Property<Guid>("TestDefinitionId");

                    b.HasKey("TestRuleId");

                    b.HasIndex("LocationId");

                    b.HasIndex("TestDefinitionId");

                    b.ToTable("TestRules");
                });

            modelBuilder.Entity("OmniPot.Data.Models.UnitOfMeasure", b =>
                {
                    b.Property<Guid>("UnitOfMeasureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.HasKey("UnitOfMeasureId");

                    b.ToTable("UnitsOfMeasure");
                });

            modelBuilder.Entity("OmniPot.Data.Models.UploadDocument", b =>
                {
                    b.Property<Guid>("UploadDocumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClientId");

                    b.Property<string>("ContentType")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<Guid?>("EmployeeId");

                    b.Property<DateTime>("ExpirationUtc");

                    b.Property<byte[]>("FileData");

                    b.Property<Guid?>("InventoryItemId");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<bool>("RemoveUponExpiry");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid?>("ServiceRecordId");

                    b.Property<int>("State");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid?>("VehicleId");

                    b.Property<Guid?>("VendorId");

                    b.HasKey("UploadDocumentId");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("InventoryItemId");

                    b.HasIndex("ServiceRecordId");

                    b.HasIndex("VehicleId");

                    b.HasIndex("VendorId");

                    b.ToTable("UploadDocuments");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Vehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("GpsId")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Make")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("MiscData")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Model")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("PlateNumber")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<Guid>("TenantId");

                    b.Property<string>("Vin")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Year")
                        .HasAnnotation("MaxLength", 4);

                    b.HasKey("VehicleId");

                    b.HasIndex("TenantId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Vendor", b =>
                {
                    b.Property<Guid>("VendorId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BillingAddressId");

                    b.Property<string>("BusinessDbaName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("ContactCellPhone");

                    b.Property<string>("ContactEmailAddress");

                    b.Property<string>("ContactFirstName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("ContactLastName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("ContactMiddleName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("ContactPhone");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DeliveryPin")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<string>("DisplayName")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Fein")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("InsuranceDocumentNumber")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("InsuranceProvider")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<bool>("IsTaxExempt");

                    b.Property<DateTime>("LicenseExpiry");

                    b.Property<string>("LicenseNumber")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("PrivateNotes")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("PublicNotes")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SalesTaxId")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<Guid?>("ShippingAddressId");

                    b.Property<int>("State");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("VendorType");

                    b.HasKey("VendorId");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("ShippingAddressId");

                    b.HasIndex("TenantId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Address", b =>
                {
                    b.HasOne("OmniPot.Data.Models.StateOrProvince", "StateOrProvince")
                        .WithMany()
                        .HasForeignKey("StateOrProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.Batch", b =>
                {
                    b.HasOne("OmniPot.Data.Models.InventoryItem", "InventoryItem")
                        .WithMany()
                        .HasForeignKey("InventoryItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.Location", "Location")
                        .WithMany("Batches")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Client", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Location", "DefaultSaleLocation")
                        .WithMany()
                        .HasForeignKey("DefaultSaleLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.Address", "DeliveryAddress")
                        .WithMany()
                        .HasForeignKey("DeliveryAddressId");

                    b.HasOne("OmniPot.Data.Models.Address", "MailingAddress")
                        .WithMany()
                        .HasForeignKey("MailingAddressId");

                    b.HasOne("OmniPot.Data.Models.Tenant")
                        .WithMany("Clients")
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Contact", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Client")
                        .WithMany("Contacts")
                        .HasForeignKey("ClientId");

                    b.HasOne("OmniPot.Data.Models.Vendor")
                        .WithMany("Contacts")
                        .HasForeignKey("VendorId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Employee", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("OmniPot.Data.Models.Employee", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorId");

                    b.HasOne("OmniPot.Data.Models.Tenant")
                        .WithMany("Employees")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.InventoryItem", b =>
                {
                    b.HasOne("OmniPot.Data.Models.ItemType", "ItemCategory")
                        .WithMany()
                        .HasForeignKey("ItemCategoryId");

                    b.HasOne("OmniPot.Data.Models.ItemType", "ItemGroup")
                        .WithMany()
                        .HasForeignKey("ItemGroupId");

                    b.HasOne("OmniPot.Data.Models.ItemType", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeId");

                    b.HasOne("OmniPot.Data.Models.TaxGroup", "TaxGroup")
                        .WithMany()
                        .HasForeignKey("TaxGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.Tenant", "Tenant")
                        .WithMany("InventoryItems")
                        .HasForeignKey("TenantId");

                    b.HasOne("OmniPot.Data.Models.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureId");

                    b.HasOne("OmniPot.Data.Models.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId");

                    b.HasOne("OmniPot.Data.Models.TaxGroup", "WholesaleTaxGroup")
                        .WithMany()
                        .HasForeignKey("WholesaleTaxGroupId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.ItemType", b =>
                {
                    b.HasOne("OmniPot.Data.Models.ItemType", "ParentType")
                        .WithMany("Children")
                        .HasForeignKey("ParentItemTypeId")
                        .HasConstraintName("FK_ItemType_ParentItemType");
                });

            modelBuilder.Entity("OmniPot.Data.Models.License", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Person")
                        .WithMany("Licenses")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Location", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("OmniPot.Data.Models.Employee")
                        .WithMany("AllowedLocations")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("OmniPot.Data.Models.Location", "ParentLocation")
                        .WithMany("Children")
                        .HasForeignKey("ParentLocationId")
                        .HasConstraintName("FK_Location_SubLocation");

                    b.HasOne("OmniPot.Data.Models.Tenant", "Tenant")
                        .WithMany("Locations")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.Lot", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Batch", "Batch")
                        .WithMany("Lots")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.Location", "CurrentLocation")
                        .WithMany("Lots")
                        .HasForeignKey("CurrentLocationId");

                    b.HasOne("OmniPot.Data.Models.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.PaymentTransaction", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Person")
                        .WithMany("Payments")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Person", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.Address", "GrowAddress")
                        .WithOne()
                        .HasForeignKey("OmniPot.Data.Models.Person", "GrowAddressAddressId");

                    b.HasOne("OmniPot.Data.Models.License", "PatientLicense")
                        .WithMany()
                        .HasForeignKey("PatientLicenseId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Phone", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Person")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.PlantTag", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Address", "PlantAddress")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("OmniPot.Data.Models.RfidTag", "ParentRfidTag")
                        .WithMany()
                        .HasForeignKey("ParentRfidTagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.PlantTagOrderItem", b =>
                {
                    b.HasOne("OmniPot.Data.Models.PlantTagOrder")
                        .WithMany("OrderItems")
                        .HasForeignKey("PlantTagOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.ServiceRecord", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Vehicle", "Vehicle")
                        .WithMany("ServiceRecords")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.StateOrProvince", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.TaxGroup", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.TaxGroupItem", b =>
                {
                    b.HasOne("OmniPot.Data.Models.TaxGroup", "TaxGroup")
                        .WithMany("TaxGroupItems")
                        .HasForeignKey("TaxGroupId");

                    b.HasOne("OmniPot.Data.Models.TaxItem", "TaxItem")
                        .WithMany("TaxGroupItems")
                        .HasForeignKey("TaxItemId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Tenant", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.TestResult", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Lot")
                        .WithMany("TestResults")
                        .HasForeignKey("LotId");

                    b.HasOne("OmniPot.Data.Models.TestDefinition", "TestDefinition")
                        .WithMany()
                        .HasForeignKey("TestDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.TestRule", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmniPot.Data.Models.TestDefinition", "TestDefinition")
                        .WithMany()
                        .HasForeignKey("TestDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.UploadDocument", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Client")
                        .WithMany("Documents")
                        .HasForeignKey("ClientId");

                    b.HasOne("OmniPot.Data.Models.Employee")
                        .WithMany("Documents")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("OmniPot.Data.Models.InventoryItem")
                        .WithMany("UploadDocuments")
                        .HasForeignKey("InventoryItemId");

                    b.HasOne("OmniPot.Data.Models.ServiceRecord")
                        .WithMany("Documents")
                        .HasForeignKey("ServiceRecordId");

                    b.HasOne("OmniPot.Data.Models.Vehicle")
                        .WithMany("Documents")
                        .HasForeignKey("VehicleId");

                    b.HasOne("OmniPot.Data.Models.Vendor")
                        .WithMany("Documents")
                        .HasForeignKey("VendorId");
                });

            modelBuilder.Entity("OmniPot.Data.Models.Vehicle", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Tenant")
                        .WithMany("Vehicles")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmniPot.Data.Models.Vendor", b =>
                {
                    b.HasOne("OmniPot.Data.Models.Address", "BillingAddress")
                        .WithMany()
                        .HasForeignKey("BillingAddressId");

                    b.HasOne("OmniPot.Data.Models.Address", "ShippingAddress")
                        .WithMany()
                        .HasForeignKey("ShippingAddressId");

                    b.HasOne("OmniPot.Data.Models.Tenant")
                        .WithMany("Vendors")
                        .HasForeignKey("TenantId");
                });
        }
    }
}
