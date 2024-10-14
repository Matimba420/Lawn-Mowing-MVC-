using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lawn_Mowing.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedMachineIdToOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConflictManager",
                table: "ConflictManager");

            migrationBuilder.RenameTable(
                name: "ConflictManager",
                newName: "ConflictManagers");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedMachineName",
                table: "Operators",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OperableMachineIds",
                table: "Operators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConflictManagers",
                table: "ConflictManagers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AssignedMachineName", "OperableMachineIds" },
                values: new object[] { null, "[1]" });

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AssignedMachineName", "OperableMachineIds" },
                values: new object[] { null, "[2]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConflictManagers",
                table: "ConflictManagers");

            migrationBuilder.DropColumn(
                name: "OperableMachineIds",
                table: "Operators");

            migrationBuilder.RenameTable(
                name: "ConflictManagers",
                newName: "ConflictManager");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedMachineName",
                table: "Operators",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConflictManager",
                table: "ConflictManager",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedMachineName",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedMachineName",
                value: 2);
        }
    }
}
