using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.persistence.Migrations
{
    public partial class Addroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "name" },
                values: new object[] { 2, "Operator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "name" },
                values: new object[] { 3, "Customer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
