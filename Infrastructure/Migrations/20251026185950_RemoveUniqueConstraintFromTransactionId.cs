using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueConstraintFromTransactionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sells_TransactionId",
                table: "Sells");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_TransactionId",
                table: "Sells",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sells_TransactionId",
                table: "Sells");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_TransactionId",
                table: "Sells",
                column: "TransactionId",
                unique: true);
        }
    }
}
