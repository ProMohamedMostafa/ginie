using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication4.Migrations
{
    public partial class firt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessAccount",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberOfEmployees = table.Column<int>(type: "int", nullable: false),
                    BusinessDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubscriptionYearsBackage = table.Column<double>(type: "float", nullable: false),
                    bPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAccount", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Business__9BCE05DC6CD7D758",
                table: "BusinessAccount",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Business__A9D105349AD036B1",
                table: "BusinessAccount",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessAccount");
        }
    }
}
