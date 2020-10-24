using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoSalesApp.DLL.Migrations
{
    /// <summary>
    /// The AddedForeignKeyToSaleEventRefToArticle migration.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class AddedForeignKeyToSaleEventRefToArticle : Migration
    {
        /// <summary>
        /// <para>
        /// Builds the operations that will migrate the database 'up'.
        /// </para>
        /// <para>
        /// That is, builds the operations that will take the database from the state left in by the
        /// previous migration so that it is up-to-date with regard to this migration.
        /// </para>
        /// <para>
        /// This method must be overridden in each class the inherits from <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" />.
        /// </para>
        /// </summary>
        /// <param name="migrationBuilder">The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will build the operations.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleEvents_Articles_ArticleSoldArticleNumber",
                table: "SaleEvents");

            migrationBuilder.DropIndex(
                name: "IX_SaleEvents_ArticleSoldArticleNumber",
                table: "SaleEvents");

            migrationBuilder.DropColumn(
                name: "ArticleSoldArticleNumber",
                table: "SaleEvents");

            migrationBuilder.AddColumn<string>(
                name: "ArticleSoldNumber",
                table: "SaleEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleEvents_ArticleSoldNumber",
                table: "SaleEvents",
                column: "ArticleSoldNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleEvents_Articles_ArticleSoldNumber",
                table: "SaleEvents",
                column: "ArticleSoldNumber",
                principalTable: "Articles",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Restrict);
        }

        /// <summary>
        /// <para>
        /// Builds the operations that will migrate the database 'down'.
        /// </para>
        /// <para>
        /// That is, builds the operations that will take the database from the state left in by
        /// this migration so that it returns to the state that it was in before this migration was applied.
        /// </para>
        /// <para>
        /// This method must be overridden in each class the inherits from <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" /> if
        /// both 'up' and 'down' migrations are to be supported. If it is not overridden, then calling it
        /// will throw and it will not be possible to migrate in the 'down' direction.
        /// </para>
        /// </summary>
        /// <param name="migrationBuilder">The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will build the operations.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleEvents_Articles_ArticleSoldNumber",
                table: "SaleEvents");

            migrationBuilder.DropIndex(
                name: "IX_SaleEvents_ArticleSoldNumber",
                table: "SaleEvents");

            migrationBuilder.DropColumn(
                name: "ArticleSoldNumber",
                table: "SaleEvents");

            migrationBuilder.AddColumn<string>(
                name: "ArticleSoldArticleNumber",
                table: "SaleEvents",
                type: "nvarchar(32)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleEvents_ArticleSoldArticleNumber",
                table: "SaleEvents",
                column: "ArticleSoldArticleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleEvents_Articles_ArticleSoldArticleNumber",
                table: "SaleEvents",
                column: "ArticleSoldArticleNumber",
                principalTable: "Articles",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
