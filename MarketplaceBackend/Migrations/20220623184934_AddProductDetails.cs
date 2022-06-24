using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceBackend.Migrations
{
    public partial class AddProductDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExtraInformation",
                table: "Products",
                newName: "DetailsTextSecondary");

            migrationBuilder.AddColumn<string>(
                name: "DetailsPictureURLPrimary",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "DetailsPictureURLSecondary",
                table: "Products",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailsTextPrimary",
                table: "Products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailsPictureURLPrimary",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DetailsPictureURLSecondary",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DetailsTextPrimary",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DetailsTextSecondary",
                table: "Products",
                newName: "ExtraInformation");
        }
    }
}
