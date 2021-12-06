using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "PostService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(type: "varchar", maxLength: 75, nullable: false),
                    Author = table.Column<string>(type: "varchar", maxLength: 60, nullable: false),
                    Summary = table.Column<string>(type: "varchar", maxLength: 300, nullable: false),
                    Published = table.Column<DateTime>(type: "date", nullable: false),
                    Url = table.Column<string>(type: "varchar", maxLength: 286, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Id",
                table: "PostService",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Url",
                table: "PostService",
                column: "Url",
                unique: true)
                .Annotation("Relational:Collation", new[] { "case_insensitive_collation" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostService");
        }
    }
}
