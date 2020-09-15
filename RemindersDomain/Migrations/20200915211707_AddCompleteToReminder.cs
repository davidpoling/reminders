using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemindersDomain.Migrations
{
    public partial class AddCompleteToReminder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Reminder",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Complete",
                table: "Reminder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complete",
                table: "Reminder");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Reminder",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
