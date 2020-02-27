using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Accounts.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(
            MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Accounts",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountStatus = table.Column<int>(),
                    Email = table.Column<string>(),
                    HashPassword = table.Column<string>(),
                    ProvinceId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Accounts",
                        x => x.Id);
                });
        }

        protected override void Down(
            MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Accounts");
        }
    }
}
