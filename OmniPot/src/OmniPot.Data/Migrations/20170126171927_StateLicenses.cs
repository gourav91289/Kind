using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OmniPot.Data.Migrations
{
    public partial class StateLicenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisabledVeteran",
                table: "StateLicenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMedicaid",
                table: "StateLicenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSsi",
                table: "StateLicenses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabledVeteran",
                table: "StateLicenses");

            migrationBuilder.DropColumn(
                name: "IsMedicaid",
                table: "StateLicenses");

            migrationBuilder.DropColumn(
                name: "IsSsi",
                table: "StateLicenses");
        }
    }
}
