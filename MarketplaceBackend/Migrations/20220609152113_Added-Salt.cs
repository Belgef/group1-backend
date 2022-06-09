using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceBackend.Migrations
{
    public partial class AddedSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Salt");

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Users",
                type: "text",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Users",
                newName: "Password");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "12345");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "55555");
        }
    }
}
