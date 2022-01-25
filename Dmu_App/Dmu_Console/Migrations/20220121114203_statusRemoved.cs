using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dmu_Console.Migrations
{
    public partial class statusRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MigrationStatuses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MigrationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    From = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MigrationStatuses", x => x.Id);
                });
        }
    }
}
