using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneracionOrdenDespacho.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    OrdenId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Usuario = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DireccionUsuario = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.OrdenId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DespachosUltimaMilla",
                columns: table => new
                {
                    DespachoUltimaMillaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    OrdenId = table.Column<Guid>(type: "char(36)", nullable: false),
                    NombreBodega = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IdentificadorDelivery = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Items = table.Column<string>(type: "longtext", maxLength: 40000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespachosUltimaMilla", x => x.DespachoUltimaMillaId);
                    table.ForeignKey(
                        name: "FK_DespachosUltimaMilla_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "OrdenId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DespachosUltimaMilla_OrdenId",
                table: "DespachosUltimaMilla",
                column: "OrdenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespachosUltimaMilla");

            migrationBuilder.DropTable(
                name: "Ordenes");
        }
    }
}
