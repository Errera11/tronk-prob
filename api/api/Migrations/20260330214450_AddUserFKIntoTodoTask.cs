using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFKIntoTodoTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todo_task_user_CreatedById",
                table: "todo_task");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "todo_task",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_todo_task_CreatedById",
                table: "todo_task",
                newName: "IX_todo_task_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_todo_task_user_UserId",
                table: "todo_task",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todo_task_user_UserId",
                table: "todo_task");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "todo_task",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_todo_task_UserId",
                table: "todo_task",
                newName: "IX_todo_task_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_todo_task_user_CreatedById",
                table: "todo_task",
                column: "CreatedById",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
