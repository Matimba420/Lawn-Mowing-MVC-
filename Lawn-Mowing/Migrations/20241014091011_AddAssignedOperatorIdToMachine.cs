using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lawn_Mowing.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedOperatorIdToMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedOperatorId",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedOperatorId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedOperatorId",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_AssignedOperatorId",
                table: "Machines",
                column: "AssignedOperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Operators_AssignedOperatorId",
                table: "Machines",
                column: "AssignedOperatorId",
                principalTable: "Operators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Operators_AssignedOperatorId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_AssignedOperatorId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "AssignedOperatorId",
                table: "Machines");
        }
    }
}
