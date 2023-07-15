using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSIapi.Migrations
{
    /// <inheritdoc />
    public partial class BoardUserIdAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Boards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_CreatedById",
                table: "Boards",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_CreatedById",
                table: "Boards",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_CreatedById",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_CreatedById",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Boards");
        }
    }
}
