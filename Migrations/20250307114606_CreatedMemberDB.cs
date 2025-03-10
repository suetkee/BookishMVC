using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class CreatedMemberDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckoutDate",
                table: "Copy",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Copy",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Copy",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Copy",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Copy_MemberId",
                table: "Copy",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copy_Member_MemberId",
                table: "Copy",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copy_Member_MemberId",
                table: "Copy");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Copy_MemberId",
                table: "Copy");

            migrationBuilder.DropColumn(
                name: "CheckoutDate",
                table: "Copy");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Copy");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Copy");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Copy");
        }
    }
}
