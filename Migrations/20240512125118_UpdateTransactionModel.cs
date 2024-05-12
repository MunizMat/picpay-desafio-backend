using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace picpay_desafio_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_PayerId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Users",
                newName: "TaxIdentifier");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Cpf",
                table: "Users",
                newName: "IX_Users_TaxIdentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Transfers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("6c44268a-6fc5-4084-baef-26bb1b773105"));

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_WalletId",
                table: "Transfers",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_PayerId",
                table: "Transfers",
                column: "PayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Wallets_WalletId",
                table: "Transfers",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_PayerId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Wallets_WalletId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_WalletId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "TaxIdentifier",
                table: "Users",
                newName: "Cpf");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TaxIdentifier",
                table: "Users",
                newName: "IX_Users_Cpf");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_PayerId",
                table: "Transfers",
                column: "PayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
