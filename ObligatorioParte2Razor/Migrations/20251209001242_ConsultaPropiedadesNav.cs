using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioParte2Razor.Migrations
{
    /// <inheritdoc />
    public partial class ConsultaPropiedadesNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicoID",
                table: "Consultas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteID",
                table: "Consultas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_MedicoID",
                table: "Consultas",
                column: "MedicoID");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteID",
                table: "Consultas",
                column: "PacienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Medicos_MedicoID",
                table: "Consultas",
                column: "MedicoID",
                principalTable: "Medicos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Pacientes_PacienteID",
                table: "Consultas",
                column: "PacienteID",
                principalTable: "Pacientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Medicos_MedicoID",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Pacientes_PacienteID",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_MedicoID",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_PacienteID",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "MedicoID",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "PacienteID",
                table: "Consultas");
        }
    }
}
