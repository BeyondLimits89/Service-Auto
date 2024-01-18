using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service_Auto.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Customer_OwnerID",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Vehicle",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_OwnerID",
                table: "Vehicle",
                newName: "IX_Vehicle_CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Customer_CustomerID",
                table: "Vehicle",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Customer_CustomerID",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Vehicle",
                newName: "OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_CustomerID",
                table: "Vehicle",
                newName: "IX_Vehicle_OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Customer_OwnerID",
                table: "Vehicle",
                column: "OwnerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
