using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotesModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotesTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotesDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesModel", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesModel");
        }
    }
}
