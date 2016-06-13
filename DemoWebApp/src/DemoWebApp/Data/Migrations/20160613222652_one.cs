using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoWebApp.Data.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblMovies",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    ImdbLink = table.Column<string>(nullable: true),
                    PosterLink = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMovies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblMoviesWithGenres",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Genre = table.Column<int>(nullable: false),
                    MovieID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMoviesWithGenres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblMoviesWithGenres_TblMovies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "TblMovies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblMoviesWithGenres_MovieID",
                table: "TblMoviesWithGenres",
                column: "MovieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblMoviesWithGenres");

            migrationBuilder.DropTable(
                name: "TblMovies");
        }
    }
}
