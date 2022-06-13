using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceBackend.Migrations
{
    public partial class AddUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "THo1KzbAJXe7YX2kyZE9f5lh/7R2G4a0vyjWkUlTR8E=", "Z4uNeaE3OuxIXryfu+JyQA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Hash", "Salt" },
                values: new object[] { "KjDxY0M+Li3TSdyNOQ/VuN4LvF4P67r5I/Xo3DPvc3Y=", "fOiJu6ZWLaQjvuTRc5G0jw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

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
    }
}
