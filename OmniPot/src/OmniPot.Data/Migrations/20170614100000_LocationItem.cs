using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OmniPot.Data.Migrations
{
    public partial class LocationItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationItems",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(nullable: false),
                    ItemTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Locations_LocationItems_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_LocationItems_ItemId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                    
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "LocationItems");
        }
    }
}
