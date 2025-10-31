using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAfterModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sells_Sells_SellEntityId",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_Sells_SellEntityId",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "SellEntityId",
                table: "Sells");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SellEntityId",
                table: "Sells",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sells_SellEntityId",
                table: "Sells",
                column: "SellEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sells_Sells_SellEntityId",
                table: "Sells",
                column: "SellEntityId",
                principalTable: "Sells",
                principalColumn: "Id");
        }
    }
}
