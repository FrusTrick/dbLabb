using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dbLabb.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interests_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IntrestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Interests_IntrestId",
                        column: x => x.IntrestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "LastName", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Doe", "John", "+46704523256" },
                    { 2, "Smith", "Jane", "+46736259883" },
                    { 3, "Johnson", "Alice", "+46789012345" },
                    { 4, "Brown", "Bob", "+46712345678" },
                    { 5, "Davis", "Eve", "+46798765432" },
                    { 6, "Wilson", "Charlie", "+46756473829" }
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Name", "PersonId" },
                values: new object[,]
                {
                    { 1, "Photography", 1 },
                    { 2, "Hiking", 1 },
                    { 3, "Cooking", 2 },
                    { 4, "Reading", 3 },
                    { 5, "Traveling", 4 },
                    { 6, "Gaming", 5 },
                    { 7, "Gardening", 6 },
                    { 8, "Cycling", 2 },
                    { 9, "Music", 3 },
                    { 10, "Yoga", 4 }
                });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "Id", "IntrestId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://www.photography.com" },
                    { 2, 2, "https://www.hikingadventures.com" },
                    { 3, 3, "https://www.cookingrecipes.com" },
                    { 4, 4, "https://www.bookclub.com" },
                    { 5, 5, "https://www.travelblog.com" },
                    { 6, 6, "https://www.gamingworld.com" },
                    { 7, 7, "https://www.gardeningtips.com" },
                    { 8, 8, "https://www.cyclingroutes.com" },
                    { 9, 9, "https://www.musiclovers.com" },
                    { 10, 10, "https://www.yogapractice.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interests_PersonId",
                table: "Interests",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_IntrestId",
                table: "Links",
                column: "IntrestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
