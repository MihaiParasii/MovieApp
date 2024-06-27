using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseNetflix.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_MovieDetail_MovieId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Comments",
                newName: "MovieDetailMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_MovieId",
                table: "Comments",
                newName: "IX_Comments_MovieDetailMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_MovieDetail_MovieDetailMovieId",
                table: "Comments",
                column: "MovieDetailMovieId",
                principalTable: "MovieDetail",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_MovieDetail_MovieDetailMovieId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "MovieDetailMovieId",
                table: "Comments",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_MovieDetailMovieId",
                table: "Comments",
                newName: "IX_Comments_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_MovieDetail_MovieId",
                table: "Comments",
                column: "MovieId",
                principalTable: "MovieDetail",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
