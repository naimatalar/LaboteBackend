using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labote.Core.Migrations
{
    public partial class fdfdgdffdh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxAgency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMainPage = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTopics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentCustomerBankAccountInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankMerchant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentCustomerBankAccountInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentCustomerBankAccountInfos_CurrentCustomers_CurrentCustomerId",
                        column: x => x.CurrentCustomerId,
                        principalTable: "CurrentCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentCustomerContactInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MailAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAddress3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentCustomerContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentCustomerContactInfos_CurrentCustomers_CurrentCustomerId",
                        column: x => x.CurrentCustomerId,
                        principalTable: "CurrentCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    NotDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_UserTopics_UserTopicId",
                        column: x => x.UserTopicId,
                        principalTable: "UserTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    NotDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserTopics_UserTopicId",
                        column: x => x.UserTopicId,
                        principalTable: "UserTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_UserTopics_UserTopicId",
                        column: x => x.UserTopicId,
                        principalTable: "UserTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laboratories_UserTopics_UserTopicId",
                        column: x => x.UserTopicId,
                        principalTable: "UserTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SampleExaminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleExaminations_UserTopics_UserTopicId",
                        column: x => x.UserTopicId,
                        principalTable: "UserTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuModules_AspNetRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuModules_MenuModules_MenuModelId",
                        column: x => x.MenuModelId,
                        principalTable: "MenuModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceResultValueSampleUnitReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementUnitLongName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasureUnitType = table.Column<int>(type: "int", nullable: false),
                    MeasureUnitSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceResultValueSampleUnitReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceResultValueSampleUnitReferences_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceResultValueTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementUnitLongName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasureUnitType = table.Column<int>(type: "int", nullable: false),
                    MeasureUnitSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceResultValueTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceResultValueTypes_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chemical",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chemical_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LaboratoryDevices_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LaboteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryUsers_AspNetUsers_LaboteUserId",
                        column: x => x.LaboteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LaboratoryUsers_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SampleAccepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveyToLaboratoeyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaboteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleAcceptStatus = table.Column<int>(type: "int", nullable: false),
                    SampleReturnType = table.Column<int>(type: "int", nullable: false),
                    SampleAcceptPackaging = table.Column<int>(type: "int", nullable: false),
                    SampleAcceptBringingType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmToGetLaboratoryUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleAccepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleAccepts_AspNetUsers_ConfirmToGetLaboratoryUserId",
                        column: x => x.ConfirmToGetLaboratoryUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SampleAccepts_AspNetUsers_LaboteUserId",
                        column: x => x.LaboteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SampleAccepts_CurrentCustomers_CurrentCustomerId",
                        column: x => x.CurrentCustomerId,
                        principalTable: "CurrentCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleAccepts_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SampleExaminationDevice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleExaminationDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleExaminationDevice_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleExaminationDevice_SampleExaminations_SampleExaminationId",
                        column: x => x.SampleExaminationId,
                        principalTable: "SampleExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SampleExaminationPriceCurrencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleExaminationPriceCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleExaminationPriceCurrencies_SampleExaminations_SampleExaminationId",
                        column: x => x.SampleExaminationId,
                        principalTable: "SampleExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SampleExaminationResultValueTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementUnitLongName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasureUnitType = table.Column<int>(type: "int", nullable: false),
                    MeasureUnitSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleExaminationResultValueTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleExaminationResultValueTypes_SampleExaminations_SampleExaminationId",
                        column: x => x.SampleExaminationId,
                        principalTable: "SampleExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalisysCreateRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LaboteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleAcceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalysisStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisysCreateRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalisysCreateRecords_AspNetUsers_LaboteUserId",
                        column: x => x.LaboteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalisysCreateRecords_SampleAccepts_SampleAcceptId",
                        column: x => x.SampleAcceptId,
                        principalTable: "SampleAccepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SampleExaminationSampleAccepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleAcceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleExaminationSampleAccepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleExaminationSampleAccepts_SampleAccepts_SampleAcceptId",
                        column: x => x.SampleAcceptId,
                        principalTable: "SampleAccepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleExaminationSampleAccepts_SampleExaminations_SampleExaminationId",
                        column: x => x.SampleExaminationId,
                        principalTable: "SampleExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalisysRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalisysCreateRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisysRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalisysRecords_AnalisysCreateRecords_AnalisysCreateRecordId",
                        column: x => x.AnalisysCreateRecordId,
                        principalTable: "AnalisysCreateRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalisysRecordDeviceValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalisysRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceResultValueTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisysRecordDeviceValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalisysRecordDeviceValues_AnalisysRecords_AnalisysRecordId",
                        column: x => x.AnalisysRecordId,
                        principalTable: "AnalisysRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalisysRecordDeviceValues_DeviceResultValueTypes_DeviceResultValueTypeId",
                        column: x => x.DeviceResultValueTypeId,
                        principalTable: "DeviceResultValueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnalisysRecordDeviceValues_SampleExaminations_SampleExaminationId",
                        column: x => x.SampleExaminationId,
                        principalTable: "SampleExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnalisysRecordSampleExaminationResultValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalisysRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleExaminationResultValueTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisysRecordSampleExaminationResultValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalisysRecordSampleExaminationResultValues_AnalisysRecords_AnalisysRecordId",
                        column: x => x.AnalisysRecordId,
                        principalTable: "AnalisysRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalisysRecordSampleExaminationResultValues_SampleExaminationResultValueTypes_SampleExaminationResultValueTypeId",
                        column: x => x.SampleExaminationResultValueTypeId,
                        principalTable: "SampleExaminationResultValueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysCreateRecords_LaboteUserId",
                table: "AnalisysCreateRecords",
                column: "LaboteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysCreateRecords_SampleAcceptId",
                table: "AnalisysCreateRecords",
                column: "SampleAcceptId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysRecordDeviceValues_AnalisysRecordId",
                table: "AnalisysRecordDeviceValues",
                column: "AnalisysRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysRecordDeviceValues_DeviceResultValueTypeId",
                table: "AnalisysRecordDeviceValues",
                column: "DeviceResultValueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysRecordDeviceValues_SampleExaminationId",
                table: "AnalisysRecordDeviceValues",
                column: "SampleExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysRecords_AnalisysCreateRecordId",
                table: "AnalisysRecords",
                column: "AnalisysCreateRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysRecordSampleExaminationResultValues_AnalisysRecordId",
                table: "AnalisysRecordSampleExaminationResultValues",
                column: "AnalisysRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisysRecordSampleExaminationResultValues_SampleExaminationResultValueTypeId",
                table: "AnalisysRecordSampleExaminationResultValues",
                column: "SampleExaminationResultValueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserTopicId",
                table: "AspNetRoles",
                column: "UserTopicId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTopicId",
                table: "AspNetUsers",
                column: "UserTopicId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Chemical_LaboratoryId",
                table: "Chemical",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCustomerBankAccountInfos_CurrentCustomerId",
                table: "CurrentCustomerBankAccountInfos",
                column: "CurrentCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCustomerContactInfos_CurrentCustomerId",
                table: "CurrentCustomerContactInfos",
                column: "CurrentCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceResultValueSampleUnitReferences_DeviceId",
                table: "DeviceResultValueSampleUnitReferences",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceResultValueTypes_DeviceId",
                table: "DeviceResultValueTypes",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserTopicId",
                table: "Devices",
                column: "UserTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_UserTopicId",
                table: "Laboratories",
                column: "UserTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDevices_DeviceId",
                table: "LaboratoryDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDevices_LaboratoryId",
                table: "LaboratoryDevices",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryUsers_LaboratoryId",
                table: "LaboratoryUsers",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryUsers_LaboteUserId",
                table: "LaboratoryUsers",
                column: "LaboteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleAccepts_ConfirmToGetLaboratoryUserId",
                table: "SampleAccepts",
                column: "ConfirmToGetLaboratoryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleAccepts_CurrentCustomerId",
                table: "SampleAccepts",
                column: "CurrentCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleAccepts_LaboratoryId",
                table: "SampleAccepts",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleAccepts_LaboteUserId",
                table: "SampleAccepts",
                column: "LaboteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationDevice_DeviceId",
                table: "SampleExaminationDevice",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationDevice_SampleExaminationId",
                table: "SampleExaminationDevice",
                column: "SampleExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationPriceCurrencies_SampleExaminationId",
                table: "SampleExaminationPriceCurrencies",
                column: "SampleExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationResultValueTypes_SampleExaminationId",
                table: "SampleExaminationResultValueTypes",
                column: "SampleExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminations_UserTopicId",
                table: "SampleExaminations",
                column: "UserTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationSampleAccepts_SampleAcceptId",
                table: "SampleExaminationSampleAccepts",
                column: "SampleAcceptId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleExaminationSampleAccepts_SampleExaminationId",
                table: "SampleExaminationSampleAccepts",
                column: "SampleExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuModules_MenuModelId",
                table: "UserMenuModules",
                column: "MenuModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuModules_UserRoleId",
                table: "UserMenuModules",
                column: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalisysRecordDeviceValues");

            migrationBuilder.DropTable(
                name: "AnalisysRecordSampleExaminationResultValues");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Chemical");

            migrationBuilder.DropTable(
                name: "CurrentCustomerBankAccountInfos");

            migrationBuilder.DropTable(
                name: "CurrentCustomerContactInfos");

            migrationBuilder.DropTable(
                name: "DeviceResultValueSampleUnitReferences");

            migrationBuilder.DropTable(
                name: "LaboratoryDevices");

            migrationBuilder.DropTable(
                name: "LaboratoryUsers");

            migrationBuilder.DropTable(
                name: "SampleExaminationDevice");

            migrationBuilder.DropTable(
                name: "SampleExaminationPriceCurrencies");

            migrationBuilder.DropTable(
                name: "SampleExaminationSampleAccepts");

            migrationBuilder.DropTable(
                name: "UserMenuModules");

            migrationBuilder.DropTable(
                name: "DeviceResultValueTypes");

            migrationBuilder.DropTable(
                name: "AnalisysRecords");

            migrationBuilder.DropTable(
                name: "SampleExaminationResultValueTypes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MenuModules");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "AnalisysCreateRecords");

            migrationBuilder.DropTable(
                name: "SampleExaminations");

            migrationBuilder.DropTable(
                name: "SampleAccepts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CurrentCustomers");

            migrationBuilder.DropTable(
                name: "Laboratories");

            migrationBuilder.DropTable(
                name: "UserTopics");
        }
    }
}
