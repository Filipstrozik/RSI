using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSIapi.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
