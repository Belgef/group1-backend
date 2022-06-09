using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceBackend.Migrations
{
    public partial class FixedSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "S3p7N6EL3VawdwGIhb2tLr00HGkn0L3iOxaKvFBI81c=", "S5Yzqn1TTlYyabQFwzEvJg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "4zQvN/g/QOddrhVuSQyqQkb/3Zj4jw8yv+DrvR6ykzs=", "voooOPOmVIfsZ4NQ80QdgA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "FAKCDSyrybkEAYunAOEkBW1Nl0hSexIkhMWsWwmPxsY=", "8eBnI2I2ZU1CAq45akw8rA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "xQj0FS435gdLcoRB8OIHQDF5j7cQAvVTHtVYnkdvNaQ=", "M7WbCYuYZcwbbdkUM7oeAg==" });
        }
    }
}
