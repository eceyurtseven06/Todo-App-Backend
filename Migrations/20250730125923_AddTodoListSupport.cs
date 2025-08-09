using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoListSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos");

            migrationBuilder.RenameTable(
                name: "ToDos",
                newName: "ToDo");

            migrationBuilder.AddColumn<int>(
                name: "TodoListId",
                table: "ToDo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDo",
                table: "ToDo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_TodoListId",
                table: "ToDo",
                column: "TodoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_TodoLists_TodoListId",
                table: "ToDo",
                column: "TodoListId",
                principalTable: "TodoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_TodoLists_TodoListId",
                table: "ToDo");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDo",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_TodoListId",
                table: "ToDo");

            migrationBuilder.DropColumn(
                name: "TodoListId",
                table: "ToDo");

            migrationBuilder.RenameTable(
                name: "ToDo",
                newName: "ToDos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos",
                column: "Id");
        }
    }
}
