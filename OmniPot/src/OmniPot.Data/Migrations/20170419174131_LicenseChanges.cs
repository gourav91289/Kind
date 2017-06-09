using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OmniPot.Data.Migrations
{
    public partial class LicenseChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CaretakerExpiry",
                table: "StateLicenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "CaretakerIsMedicaid",
                table: "StateLicenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CaretakerIssueDate",
                table: "StateLicenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CaretakerLicenseNumber",
                table: "StateLicenses",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHospice",
                table: "StateLicenses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaretakerExpiry",
                table: "StateLicenses");

            migrationBuilder.DropColumn(
                name: "CaretakerIsMedicaid",
                table: "StateLicenses");

            migrationBuilder.DropColumn(
                name: "CaretakerIssueDate",
                table: "StateLicenses");

            migrationBuilder.DropColumn(
                name: "CaretakerLicenseNumber",
                table: "StateLicenses");

            migrationBuilder.DropColumn(
                name: "IsHospice",
                table: "StateLicenses");
        }
    }
}
