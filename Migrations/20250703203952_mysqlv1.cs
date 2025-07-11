﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uttt.Micro.Libro.Migrations
{
    /// <inheritdoc />
    public partial class mysqlv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LibreriaMateriales",
                columns: table => new
                {
                    LibreriaMaterialId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Titulo = table.Column<string>(type: "longtext", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AutorLibro = table.Column<Guid>(type: "char(36)", nullable: true),
                    NewData = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibreriaMateriales", x => x.LibreriaMaterialId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibreriaMateriales");
        }
    }
}
