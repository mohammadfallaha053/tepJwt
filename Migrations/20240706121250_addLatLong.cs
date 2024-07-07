using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT53.Migrations
{
    /// <inheritdoc />
    public partial class addLatLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Properties",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Long",
                table: "Properties",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "Properties");
        }
    }
}
