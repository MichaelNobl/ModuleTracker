using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModuleTracker.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SheetDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SheetNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    NumOfExercises = table.Column<int>(type: "INTEGER", nullable: false),
                    ModuleDtoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetDto_Modules_ModuleDtoId",
                        column: x => x.ModuleDtoId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SheetDto_ModuleDtoId",
                table: "SheetDto",
                column: "ModuleDtoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetDto");

            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
