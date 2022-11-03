using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoScore.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Tourmanent");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tourmanent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tourmanent");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Tourmanent",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
