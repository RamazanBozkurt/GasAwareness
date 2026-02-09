using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasAwareness.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSurveysTableVideoIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Videos_VideoId",
                table: "Surveys");

            migrationBuilder.AlterColumn<Guid>(
                name: "VideoId",
                table: "Surveys",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "SurveyOptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Videos_VideoId",
                table: "Surveys",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Videos_VideoId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "SurveyOptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "VideoId",
                table: "Surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Videos_VideoId",
                table: "Surveys",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
