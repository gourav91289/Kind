using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OmniPot.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    AppConnectionId = table.Column<Guid>(nullable: false),
                    ConnectionId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    Tenant = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.AppConnectionId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    AppMessageId = table.Column<Guid>(nullable: false),
                    ConnectionId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    MessageType = table.Column<int>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    Tenant = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.AppMessageId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 5, nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    ItemTypeId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    ParentItemTypeId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.ItemTypeId);
                    table.ForeignKey(
                        name: "FK_ItemType_ParentItemType",
                        column: x => x.ParentItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantTagOrders",
                columns: table => new
                {
                    PlantTagOrderId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    OrderDateUtc = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    TagQuantity = table.Column<int>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTagOrders", x => x.PlantTagOrderId);
                });

            migrationBuilder.CreateTable(
                name: "RfidTag",
                columns: table => new
                {
                    RfidTagId = table.Column<Guid>(nullable: false),
                    DestroyedUtc = table.Column<DateTime>(nullable: true),
                    ReplacedById = table.Column<Guid>(nullable: true),
                    ReplacedUtc = table.Column<DateTime>(nullable: true),
                    TagData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfidTag", x => x.RfidTagId);
                });

            migrationBuilder.CreateTable(
                name: "StateLicenses",
                columns: table => new
                {
                    StateLicenseId = table.Column<Guid>(nullable: false),
                    Expiry = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    LicenseNumber = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateLicenses", x => x.StateLicenseId);
                });

            migrationBuilder.CreateTable(
                name: "TaxItems",
                columns: table => new
                {
                    TaxItemId = table.Column<Guid>(nullable: false),
                    AgencyName = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(7,5)", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxItems", x => x.TaxItemId);
                });

            migrationBuilder.CreateTable(
                name: "TestDefinitions",
                columns: table => new
                {
                    TestDefinitionId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    IsPassFail = table.Column<bool>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RequiresDecimal = table.Column<bool>(nullable: false),
                    RequiresNote = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDefinitions", x => x.TestDefinitionId);
                });

            migrationBuilder.CreateTable(
                name: "UnitsOfMeasure",
                columns: table => new
                {
                    UnitOfMeasureId = table.Column<Guid>(nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 10, nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsOfMeasure", x => x.UnitOfMeasureId);
                });

            migrationBuilder.CreateTable(
                name: "StatesOrProvinces",
                columns: table => new
                {
                    StateOrProvinceId = table.Column<Guid>(nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 5, nullable: true),
                    CountryId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatesOrProvinces", x => x.StateOrProvinceId);
                    table.ForeignKey(
                        name: "FK_StatesOrProvinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantTagOrderItems",
                columns: table => new
                {
                    PlantTagOrderItemId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PlantTagOrderId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Sku = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    TagType = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTagOrderItems", x => x.PlantTagOrderItemId);
                    table.ForeignKey(
                        name: "FK_PlantTagOrderItems_PlantTagOrders_PlantTagOrderId",
                        column: x => x.PlantTagOrderId,
                        principalTable: "PlantTagOrders",
                        principalColumn: "PlantTagOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(nullable: false),
                    Addressee = table.Column<string>(maxLength: 50, nullable: false),
                    CityName = table.Column<string>(maxLength: 64, nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeliveryLine1 = table.Column<string>(maxLength: 50, nullable: false),
                    DeliveryLine2 = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    StateOrProvinceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_StatesOrProvinces_StateOrProvinceId",
                        column: x => x.StateOrProvinceId,
                        principalTable: "StatesOrProvinces",
                        principalColumn: "StateOrProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CssOverrides = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RouteName = table.Column<string>(maxLength: 25, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(maxLength: 100, nullable: true),
                    UseLots = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                    table.UniqueConstraint("AK_Tenant_DisplayName", x => x.DisplayName);
                    table.UniqueConstraint("AK_Tenant_RouteName", x => x.RouteName);
                    table.ForeignKey(
                        name: "FK_Tenants_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(nullable: false),
                    Color = table.Column<string>(maxLength: 25, nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    GpsId = table.Column<string>(maxLength: 255, nullable: true),
                    Make = table.Column<string>(maxLength: 30, nullable: true),
                    MiscData = table.Column<string>(maxLength: 255, nullable: true),
                    Model = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PlateNumber = table.Column<string>(maxLength: 20, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Vin = table.Column<string>(maxLength: 25, nullable: true),
                    Year = table.Column<string>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(nullable: false),
                    BillingAddressId = table.Column<Guid>(nullable: true),
                    BusinessDbaName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactCellPhone = table.Column<string>(nullable: true),
                    ContactEmailAddress = table.Column<string>(nullable: true),
                    ContactFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    ContactLastName = table.Column<string>(maxLength: 50, nullable: true),
                    ContactMiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeliveryPin = table.Column<string>(maxLength: 8, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 255, nullable: true),
                    Fein = table.Column<string>(maxLength: 20, nullable: true),
                    InsuranceDocumentNumber = table.Column<string>(maxLength: 20, nullable: true),
                    InsuranceProvider = table.Column<string>(maxLength: 20, nullable: true),
                    IsTaxExempt = table.Column<bool>(nullable: false),
                    LicenseExpiry = table.Column<DateTime>(nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PrivateNotes = table.Column<string>(maxLength: 1000, nullable: true),
                    PublicNotes = table.Column<string>(maxLength: 1000, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SalesTaxId = table.Column<string>(maxLength: 20, nullable: true),
                    ShippingAddressId = table.Column<Guid>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    VendorType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendors_Addresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Addresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    CompensationType = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfHire = table.Column<DateTime>(nullable: false),
                    DateOfTermination = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    IsDriver = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    LicenseExpiry = table.Column<DateTime>(nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    OtherPhone = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SupervisorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    VehicleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRecord",
                columns: table => new
                {
                    ServiceRecordId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CurrentMilage = table.Column<int>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    NextServiceDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(maxLength: 500, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ServiceDate = table.Column<DateTime>(nullable: false),
                    ServiceType = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: false),
                    VendorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecord", x => x.ServiceRecordId);
                    table.ForeignKey(
                        name: "FK_ServiceRecord_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    ClassificationType = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    IsSalable = table.Column<bool>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    ParentLocationId = table.Column<Guid>(nullable: true),
                    RouteName = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.UniqueConstraint("AK_Location_TenantId_DisplayName", x => new { x.TenantId, x.DisplayName });
                    table.UniqueConstraint("AK_Location_TenantId_RouteName", x => new { x.TenantId, x.RouteName });
                    table.ForeignKey(
                        name: "FK_Locations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_SubLocation",
                        column: x => x.ParentLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    BusinessPhone = table.Column<string>(nullable: true),
                    CellPhone = table.Column<string>(nullable: true),
                    ClientType = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DefaultSaleLocationId = table.Column<Guid>(nullable: false),
                    DeliveryAddressId = table.Column<Guid>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    IsTaxExempt = table.Column<bool>(nullable: false),
                    IsTempStatus = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    MailingAddressId = table.Column<Guid>(nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    NickName = table.Column<string>(maxLength: 50, nullable: true),
                    PrivateNotes = table.Column<string>(maxLength: 1000, nullable: true),
                    PrivateNotesExtended = table.Column<string>(nullable: true),
                    PublicNotes = table.Column<string>(maxLength: 1000, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Locations_DefaultSaleLocationId",
                        column: x => x.DefaultSaleLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Addresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Addresses_MailingAddressId",
                        column: x => x.MailingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxGroups",
                columns: table => new
                {
                    TaxGroupId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayColor = table.Column<string>(maxLength: 20, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    IsAppliedBeforeDiscount = table.Column<bool>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxGroups", x => x.TaxGroupId);
                    table.ForeignKey(
                        name: "FK_TaxGroups_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestRules",
                columns: table => new
                {
                    TestRuleId = table.Column<Guid>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    TestDefinitionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRules", x => x.TestRuleId);
                    table.ForeignKey(
                        name: "FK_TestRules_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestRules_TestDefinitions_TestDefinitionId",
                        column: x => x.TestDefinitionId,
                        principalTable: "TestDefinitions",
                        principalColumn: "TestDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    VendorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contact_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    InventoryItemId = table.Column<Guid>(nullable: false),
                    ClassificationType = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    ItemCategoryId = table.Column<Guid>(nullable: true),
                    ItemExpiryDate = table.Column<DateTime>(nullable: true),
                    ItemGroupId = table.Column<Guid>(nullable: true),
                    ItemTypeId = table.Column<Guid>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(maxLength: 144, nullable: true),
                    PurchaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SalesNotes = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    StaticPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxGroupId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TraceableQuantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnitOfMeasureId = table.Column<Guid>(nullable: true),
                    UpcSku = table.Column<string>(maxLength: 20, nullable: true),
                    VendorId = table.Column<Guid>(nullable: true),
                    WholesaleTaxGroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.InventoryItemId);
                    table.ForeignKey(
                        name: "FK_InventoryItems_ItemTypes_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_ItemTypes_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalTable: "TaxGroups",
                        principalColumn: "TaxGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_UnitsOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "UnitOfMeasureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_TaxGroups_WholesaleTaxGroupId",
                        column: x => x.WholesaleTaxGroupId,
                        principalTable: "TaxGroups",
                        principalColumn: "TaxGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxGroupItems",
                columns: table => new
                {
                    TaxItemId = table.Column<Guid>(nullable: false),
                    TaxGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxGroupItems", x => new { x.TaxItemId, x.TaxGroupId });
                    table.ForeignKey(
                        name: "FK_TaxGroupItems_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalTable: "TaxGroups",
                        principalColumn: "TaxGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxGroupItems_TaxItems_TaxItemId",
                        column: x => x.TaxItemId,
                        principalTable: "TaxItems",
                        principalColumn: "TaxItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    BatchId = table.Column<Guid>(nullable: false),
                    BarCode = table.Column<string>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ExpiryDateUtc = table.Column<DateTime>(nullable: false),
                    InventoryItemId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    ManufactureDateUtc = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchId);
                    table.UniqueConstraint("AK_Batch_LocationId_BarCode", x => new { x.LocationId, x.BarCode });
                    table.ForeignKey(
                        name: "FK_Batches_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Batches_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UploadDocuments",
                columns: table => new
                {
                    UploadDocumentId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    ContentType = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 255, nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    ExpirationUtc = table.Column<DateTime>(nullable: false),
                    FileData = table.Column<byte[]>(nullable: true),
                    InventoryItemId = table.Column<Guid>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    RemoveUponExpiry = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ServiceRecordId = table.Column<Guid>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: true),
                    VendorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadDocuments", x => x.UploadDocumentId);
                    table.ForeignKey(
                        name: "FK_UploadDocuments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadDocuments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadDocuments_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadDocuments_ServiceRecord_ServiceRecordId",
                        column: x => x.ServiceRecordId,
                        principalTable: "ServiceRecord",
                        principalColumn: "ServiceRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadDocuments_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadDocuments_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    LotId = table.Column<Guid>(nullable: false),
                    BarCode = table.Column<string>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CurrentLocationId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    UnitOfMeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.LotId);
                    table.UniqueConstraint("AK_Lot_CurrentLocationId_BarCode", x => new { x.CurrentLocationId, x.BarCode });
                    table.ForeignKey(
                        name: "FK_Lots_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lots_Locations_CurrentLocationId",
                        column: x => x.CurrentLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lots_UnitsOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "UnitOfMeasureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    TestResultId = table.Column<Guid>(nullable: false),
                    HasPassed = table.Column<bool>(nullable: true),
                    LotId = table.Column<Guid>(nullable: true),
                    Result = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ResultNotes = table.Column<string>(maxLength: 100, nullable: true),
                    StrainName = table.Column<string>(maxLength: 25, nullable: false),
                    TestDate = table.Column<DateTime>(nullable: false),
                    TestDefinitionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.TestResultId);
                    table.ForeignKey(
                        name: "FK_TestResults_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "LotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestResults_TestDefinitions_TestDefinitionId",
                        column: x => x.TestDefinitionId,
                        principalTable: "TestDefinitions",
                        principalColumn: "TestDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    ApplicationStatus = table.Column<int>(nullable: false),
                    ApprovedUtc = table.Column<DateTime>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CurrentOccupation = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DeniedReason = table.Column<string>(nullable: true),
                    DeniedUtc = table.Column<DateTime>(nullable: true),
                    EducationStatus = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    EmploymentStatus = table.Column<int>(nullable: false),
                    Ethnicity = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    GrowAddressAddressId = table.Column<Guid>(nullable: true),
                    GrowAddressId = table.Column<Guid>(nullable: false),
                    HasDohDiscount = table.Column<bool>(nullable: false),
                    IsAlsoPatient = table.Column<bool>(nullable: false),
                    IsCitizen = table.Column<bool>(nullable: false),
                    IsCurrentlyEnrolled = table.Column<bool>(nullable: false),
                    IsGrowAddressCooperative = table.Column<bool>(nullable: false),
                    IsVeteran = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MaritalStatus = table.Column<int>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PatientLicenseId = table.Column<Guid>(nullable: true),
                    PersonType = table.Column<int>(nullable: false),
                    Race = table.Column<int>(nullable: false),
                    RequestedTagCount = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_People_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_Addresses_GrowAddressAddressId",
                        column: x => x.GrowAddressAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    LicenseId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ExpiryMonth = table.Column<int>(nullable: false),
                    ExpiryYear = table.Column<int>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.LicenseId);
                    table.ForeignKey(
                        name: "FK_License_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransaction",
                columns: table => new
                {
                    PaymentTransactionId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AuthCode = table.Column<string>(nullable: true),
                    AvsResponse = table.Column<string>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CvvResponse = table.Column<string>(nullable: true),
                    LastFour = table.Column<string>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: true),
                    PlantTagOrderId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    TransactionUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransaction", x => x.PaymentTransactionId);
                    table.ForeignKey(
                        name: "FK_PaymentTransaction_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    PhoneId = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneType = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoneId);
                    table.ForeignKey(
                        name: "FK_Phone_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantTags",
                columns: table => new
                {
                    PlantTagId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ExpiryUtc = table.Column<DateTime>(nullable: true),
                    IssuedUtc = table.Column<DateTime>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedUtc = table.Column<DateTime>(nullable: false),
                    ParentRfidTagId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTags", x => x.PlantTagId);
                    table.ForeignKey(
                        name: "FK_PlantTags_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantTags_RfidTag_ParentRfidTagId",
                        column: x => x.ParentRfidTagId,
                        principalTable: "RfidTag",
                        principalColumn: "RfidTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantTags_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateOrProvinceId",
                table: "Addresses",
                column: "StateOrProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_InventoryItemId",
                table: "Batches",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_LocationId",
                table: "Batches",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DefaultSaleLocationId",
                table: "Clients",
                column: "DefaultSaleLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DeliveryAddressId",
                table: "Clients",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MailingAddressId",
                table: "Clients",
                column: "MailingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TenantId",
                table: "Clients",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ClientId",
                table: "Contact",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_VendorId",
                table: "Contact",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressId",
                table: "Employees",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TenantId",
                table: "Employees",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_VehicleId",
                table: "Employees",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemCategoryId",
                table: "InventoryItems",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemGroupId",
                table: "InventoryItems",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemTypeId",
                table: "InventoryItems",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_TaxGroupId",
                table: "InventoryItems",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_TenantId",
                table: "InventoryItems",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_UnitOfMeasureId",
                table: "InventoryItems",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_VendorId",
                table: "InventoryItems",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_WholesaleTaxGroupId",
                table: "InventoryItems",
                column: "WholesaleTaxGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_ParentItemTypeId",
                table: "ItemTypes",
                column: "ParentItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_License_PersonId",
                table: "License",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId",
                table: "Locations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_EmployeeId",
                table: "Locations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TenantId",
                table: "Locations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_BatchId",
                table: "Lots",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_CurrentLocationId",
                table: "Lots",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_UnitOfMeasureId",
                table: "Lots",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_PersonId",
                table: "PaymentTransaction",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_People_AddressId",
                table: "People",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_People_GrowAddressAddressId",
                table: "People",
                column: "GrowAddressAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_PatientLicenseId",
                table: "People",
                column: "PatientLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PersonId",
                table: "Phone",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTags_AddressId",
                table: "PlantTags",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTags_ParentRfidTagId",
                table: "PlantTags",
                column: "ParentRfidTagId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTags_PersonId",
                table: "PlantTags",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTagOrderItems_PlantTagOrderId",
                table: "PlantTagOrderItems",
                column: "PlantTagOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecord_VehicleId",
                table: "ServiceRecord",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_StatesOrProvinces_CountryId",
                table: "StatesOrProvinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxGroups_LocationId",
                table: "TaxGroups",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxGroupItems_TaxGroupId",
                table: "TaxGroupItems",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxGroupItems_TaxItemId",
                table: "TaxGroupItems",
                column: "TaxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_AddressId",
                table: "Tenants",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_LotId",
                table: "TestResults",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestDefinitionId",
                table: "TestResults",
                column: "TestDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRules_LocationId",
                table: "TestRules",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRules_TestDefinitionId",
                table: "TestRules",
                column: "TestDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadDocuments_ClientId",
                table: "UploadDocuments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadDocuments_EmployeeId",
                table: "UploadDocuments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadDocuments_InventoryItemId",
                table: "UploadDocuments",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadDocuments_ServiceRecordId",
                table: "UploadDocuments",
                column: "ServiceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadDocuments_VehicleId",
                table: "UploadDocuments",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadDocuments_VendorId",
                table: "UploadDocuments",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TenantId",
                table: "Vehicles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_BillingAddressId",
                table: "Vendors",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ShippingAddressId",
                table: "Vendors",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_TenantId",
                table: "Vendors",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_License_PatientLicenseId",
                table: "People",
                column: "PatientLicenseId",
                principalTable: "License",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_StatesOrProvinces_StateOrProvinceId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Addresses_AddressId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Addresses_GrowAddressAddressId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_License_People_PersonId",
                table: "License");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "PaymentTransaction");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "PlantTags");

            migrationBuilder.DropTable(
                name: "PlantTagOrderItems");

            migrationBuilder.DropTable(
                name: "StateLicenses");

            migrationBuilder.DropTable(
                name: "TaxGroupItems");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "TestRules");

            migrationBuilder.DropTable(
                name: "UploadDocuments");

            migrationBuilder.DropTable(
                name: "RfidTag");

            migrationBuilder.DropTable(
                name: "PlantTagOrders");

            migrationBuilder.DropTable(
                name: "TaxItems");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "TestDefinitions");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ServiceRecord");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "TaxGroups");

            migrationBuilder.DropTable(
                name: "UnitsOfMeasure");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "StatesOrProvinces");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "License");
        }
    }
}
