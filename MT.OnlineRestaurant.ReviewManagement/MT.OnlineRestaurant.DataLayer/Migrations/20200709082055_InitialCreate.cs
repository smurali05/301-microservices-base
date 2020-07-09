using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblRestaurantReview",
                columns: table => new
                {
                    TblReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TblRestaurantId = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    TblUserComments = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    TblRating = table.Column<string>(maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    TblCustomerId = table.Column<int>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRestaurantReview", x => x.TblReviewId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblRestaurantReview");
        }
    }
}
