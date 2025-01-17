using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_base_implement.Migrations
{
    /// <inheritdoc />
    public partial class _migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "document_type",
                table: "templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_templates_UserId",
                table: "templates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_templates_users_UserId",
                table: "templates",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_templates_users_UserId",
                table: "templates");

            migrationBuilder.DropIndex(
                name: "IX_templates_UserId",
                table: "templates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "templates");

            migrationBuilder.DropColumn(
                name: "document_type",
                table: "templates");
        }
    }
}
