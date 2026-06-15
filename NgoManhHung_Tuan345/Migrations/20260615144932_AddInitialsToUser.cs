using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NgoManhHung_Tuan345.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Initials",
                table: "AspNetUsers",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Initials",
                table: "AspNetUsers");
        }
    }
}
