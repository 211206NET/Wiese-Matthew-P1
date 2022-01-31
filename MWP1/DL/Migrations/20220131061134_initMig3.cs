using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DL.Migrations
{
    public partial class initMig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "P1x",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P1y",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P2x",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P2y",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P3x",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P3y",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P4x",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P4y",
                table: "Games",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "P1x",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P1y",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P2x",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P2y",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P3x",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P3y",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P4x",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P4y",
                table: "Games");
        }
    }
}
