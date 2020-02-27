using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Addresses.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(
            MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Countries",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Countries",
                        x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Province",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(),
                    CountryId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Province",
                        x => x.Id);

                    table.ForeignKey(
                        "FK_Province_Countries_CountryId",
                        x => x.CountryId,
                        "Countries",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Province_CountryId",
                "Province",
                "CountryId");
        }

        protected override void Down(
            MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Province");

            migrationBuilder.DropTable(
                "Countries");
        }
    }
}
