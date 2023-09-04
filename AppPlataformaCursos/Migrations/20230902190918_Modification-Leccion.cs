using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppPlataformaCursos.Migrations
{
    /// <inheritdoc />
    public partial class ModificationLeccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recomendable",
                table: "Lecciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Recomendable",
                table: "Lecciones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
