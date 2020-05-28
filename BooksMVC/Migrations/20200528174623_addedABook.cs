using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksMVC.Migrations
{
    public partial class addedABook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "InInventory", "NumberOfPages", "Title" },
                values: new object[] { 4, "Stephan King", true, 253, "Cujo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
