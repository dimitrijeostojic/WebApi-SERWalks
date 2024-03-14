using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SERWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7734507a-1505-46bd-8416-989cbff2fe19"), "Easy" },
                    { new Guid("819bbef6-a149-4390-9e99-589b9dcacd49"), "Medium" },
                    { new Guid("9cc663aa-3ff3-439a-a5f3-0e733272b1b7"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("309b7bc9-339d-4855-aa85-573cef4ceb50"), "POM", "Pomoravlje", "pomoravlje.jpg" },
                    { new Guid("38d42c0c-aeac-425b-a1f7-f942708ef693"), "VOJ", "Vojvodina", "vojvodina.jpg" },
                    { new Guid("757ec18b-c924-408e-9456-5587dcaab8a4"), "SUM", "Sumadija", "sumadija.jpg" },
                    { new Guid("c41ed9d3-5dc7-4a94-84cc-0be563500b61"), "BEO", "Beograd", "beograd.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7734507a-1505-46bd-8416-989cbff2fe19"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("819bbef6-a149-4390-9e99-589b9dcacd49"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9cc663aa-3ff3-439a-a5f3-0e733272b1b7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("309b7bc9-339d-4855-aa85-573cef4ceb50"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("38d42c0c-aeac-425b-a1f7-f942708ef693"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("757ec18b-c924-408e-9456-5587dcaab8a4"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c41ed9d3-5dc7-4a94-84cc-0be563500b61"));
        }
    }
}
