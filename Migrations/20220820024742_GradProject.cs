using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grade_Project_.Migrations
{
    public partial class GradProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model_Icon",
                table: "Car_Models");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model_Icon",
                table: "Car_Models",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
