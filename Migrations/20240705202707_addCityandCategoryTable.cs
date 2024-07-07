using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT53.Migrations
{
    /// <inheritdoc />
    public partial class addCityandCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Properties",
                newName: "Name_Ku");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Properties",
                newName: "Name_En");

            migrationBuilder.AddColumn<int>(
                name: "Area",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description_Ar",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description_En",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description_Ku",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShowInAdPage",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShowInMainPage",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name_Ar",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBathrooms",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName_Ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName_En = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName_Ku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryIconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName_Ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityName_En = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityName_Ku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CityId",
                table: "Properties",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_PropertyId",
                table: "Files",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Properties_PropertyId",
                table: "Files",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Category_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_City_CityId",
                table: "Properties",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Properties_PropertyId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Category_CategoryId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_City_CityId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CityId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Files_PropertyId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Description_Ar",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Description_En",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Description_Ku",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsShowInAdPage",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsShowInMainPage",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Name_Ar",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "NumberOfBathrooms",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Name_Ku",
                table: "Properties",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Name_En",
                table: "Properties",
                newName: "Description");
        }
    }
}
