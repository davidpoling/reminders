using Microsoft.EntityFrameworkCore.Migrations;

namespace RemindersDomain.Migrations
{
    public partial class ChangeCheckedToComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checked",
                table: "ShoppingList");

            migrationBuilder.AddColumn<bool>(
                name: "Complete",
                table: "ShoppingList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Complete",
                table: "Reminder",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bit(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complete",
                table: "ShoppingList");

            migrationBuilder.AddColumn<ulong>(
                name: "Checked",
                table: "ShoppingList",
                type: "bit(1)",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Complete",
                table: "Reminder",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
