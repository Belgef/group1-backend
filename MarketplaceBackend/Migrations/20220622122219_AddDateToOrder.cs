using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceBackend.Migrations
{
    public partial class AddDateToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OnDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "yekpe6tCi6QLoC9vDDNKFMOrcZb+FU6LPJZKito7e+A=", "XQbkRu3D7Xs2iRNl+0BSgw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "3Vsu/XEDRzrEhW85BBlY3hpYTTpnpMtYJsDBjFOjfDM=", "Jar/fb4MeAvIoFkuaqprpQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnDate",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "NLLk10/U1wgg0z5X58PbEX2Q/dVJ0BTsq9gzO8cy8Po=", "8RoK99ljWrh/HxDx06URdg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "XC9Nwe6SK04jytltltwzCTFX0uYzvkLDgdeSHWTFM8M=", "jhxSc67JM2rRc3OvfhBuTQ==" });
        }
    }
}
