using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace picpay_desafio_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAccountBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AccountBalance",
                table: "Users",
                type: "numeric",
                nullable: false,
                defaultValue: 5000.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Users");
        }
    }
}
