using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash" },
                values: new object[] { "378d6859-719a-48c9-827d-f5c2a767d6b6", new DateTime(2023, 11, 10, 11, 38, 26, 412, DateTimeKind.Local).AddTicks(8666), "AQAAAAIAAYagAAAAEIbhbwnyY+NdfV/cxsDu8ZLtrkfGqX61T6bZCkyoslGhjGiwgS1Jg7ZTFgf+/9yoFg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JShShIR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MidleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportSerie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash" },
                values: new object[] { "4754bc0a-449a-4392-968b-8d416685909d", new DateTime(2023, 11, 10, 11, 36, 52, 487, DateTimeKind.Local).AddTicks(3325), "AQAAAAIAAYagAAAAEBMq2ljew8Gmy8e9bxfHeQPsOeYBaBIs43B8c6I6n7/LgO3qZ2w+2eiTZMXbiR6Bpw==" });
        }
    }
}
