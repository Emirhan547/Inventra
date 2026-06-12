using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");
        }
    }
}
