using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movies.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageRating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRatings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2360343a-93d0-485f-8182-552434ae7179"), "Action" },
                    { new Guid("65fa3cc9-3f4d-42fb-8b1c-c1dcd789d6ec"), "Sci-Fi" },
                    { new Guid("d8f9dafa-f46e-4346-9fba-958434ad1823"), "Comedy" },
                    { new Guid("e1020877-c97b-411a-8186-c554220bf5d2"), "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "AverageRating", "Description", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { new Guid("2f053594-a04d-4b97-aa64-2b59745d4e96"), 4.8f, "A computer hacker learns from mysterious rebels about the true nature of his reality...", 1999, "The Matrix" },
                    { new Guid("5f0b0e0e-78d7-40c4-b439-da4256cec59d"), 4.5f, "A thief who steals corporate secrets through the use of dream-sharing technology...", 2010, "Inception" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { new Guid("2360343a-93d0-485f-8182-552434ae7179"), new Guid("2f053594-a04d-4b97-aa64-2b59745d4e96") },
                    { new Guid("65fa3cc9-3f4d-42fb-8b1c-c1dcd789d6ec"), new Guid("2f053594-a04d-4b97-aa64-2b59745d4e96") },
                    { new Guid("2360343a-93d0-485f-8182-552434ae7179"), new Guid("5f0b0e0e-78d7-40c4-b439-da4256cec59d") },
                    { new Guid("65fa3cc9-3f4d-42fb-8b1c-c1dcd789d6ec"), new Guid("5f0b0e0e-78d7-40c4-b439-da4256cec59d") }
                });

            migrationBuilder.InsertData(
                table: "MovieRatings",
                columns: new[] { "Id", "MovieId", "Rating" },
                values: new object[,]
                {
                    { 1, new Guid("5f0b0e0e-78d7-40c4-b439-da4256cec59d"), 5 },
                    { 2, new Guid("5f0b0e0e-78d7-40c4-b439-da4256cec59d"), 4 },
                    { 3, new Guid("2f053594-a04d-4b97-aa64-2b59745d4e96"), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRatings_MovieId",
                table: "MovieRatings",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "MovieRatings");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
