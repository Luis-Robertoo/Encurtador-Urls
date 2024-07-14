using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encurtei.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migracao_Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL_ORIGINAL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    URL_ENCURTADA = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QR_CODE = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Link");
        }
    }
}
