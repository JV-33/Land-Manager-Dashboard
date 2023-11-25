using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    PersonalCodeOrRegistrationNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "LandProperties",
                columns: table => new
                {
                    LandPropertyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CadastreNumber = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandProperties", x => x.LandPropertyId);
                    table.ForeignKey(
                        name: "FK_LandProperties_Persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LandParcels",
                columns: table => new
                {
                    LandParcelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalAreaInHectares = table.Column<double>(type: "REAL", nullable: false),
                    SurveyDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LandPropertyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandParcels", x => x.LandParcelId);
                    table.ForeignKey(
                        name: "FK_LandParcels_LandProperties_LandPropertyId",
                        column: x => x.LandPropertyId,
                        principalTable: "LandProperties",
                        principalColumn: "LandPropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LandUses",
                columns: table => new
                {
                    LandUseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    AreaInHectares = table.Column<double>(type: "REAL", nullable: false),
                    LandParcelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandUses", x => x.LandUseId);
                    table.ForeignKey(
                        name: "FK_LandUses_LandParcels_LandParcelId",
                        column: x => x.LandParcelId,
                        principalTable: "LandParcels",
                        principalColumn: "LandParcelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LandParcels_LandPropertyId",
                table: "LandParcels",
                column: "LandPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_LandProperties_OwnerId",
                table: "LandProperties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LandUses_LandParcelId",
                table: "LandUses",
                column: "LandParcelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandUses");

            migrationBuilder.DropTable(
                name: "LandParcels");

            migrationBuilder.DropTable(
                name: "LandProperties");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
