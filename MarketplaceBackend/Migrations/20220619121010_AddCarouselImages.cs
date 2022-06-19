using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketplaceBackend.Migrations
{
    public partial class AddCarouselImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarouselImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageURL = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselImages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "BU6cnMagVC2GrT9ZAyt0ONN6N1d4GZmevNdXmYlUXik=", "AoobxnhkDq69fB8tU7Aw+g==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "VP0n1vFmwU3N9L6QeESfvtp4/MgtvKQzJb1WOVjKk7s=", "v2RcVPFas/foYoMvFpTufQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarouselImages");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "4tFYR9RpQ1E6AGF65AhzWgXeeGF2uc2plPn9/nevF/Q=", "ZHcYqdhBDaVu6FSzdnNtRg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "5q+KOAT6Oi0/SU54Oe0hQoQXXiVnJBF9O8cFO1vAMoM=", "J3WOjWwywRLq9nPNaESRqQ==" });
        }
    }
}
