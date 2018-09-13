using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VicTrains.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ScheduleSeq",
                incrementBy: 1,
                startValue: 6);

            migrationBuilder.CreateTable(
                name: "TrainLines",
                columns: table => new
                {
                    TrainLineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Line = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainLines", x => x.TrainLineID);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(nullable: false),
                    TrainLineID = table.Column<int>(nullable: false),
                    DepartureStation = table.Column<string>(maxLength: 40, nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ArrivalStation = table.Column<string>(maxLength: 40, nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_Schedules_TrainLines",
                        column: x => x.TrainLineID,
                        principalTable: "TrainLines",
                        principalColumn: "TrainLineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TrainLines",
                columns: new[] { "TrainLineID", "Description", "Line" },
                values: new object[,]
                {
                    { 1, "Wendouree to Melbourne via Ballarat", "Ballarat Line" },
                    { 2, "Bendigo to Melbourne", "Bendigo Line" },
                    { 3, "Melbourne to Geelong", "Geelong Line" },
                    { 4, "Traralgon to Melbourne", "Traralgon Line" },
                    { 5, "Melbourne to Seymour", "Seymour Line" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleID", "ArrivalDateTime", "ArrivalStation", "DepartureDateTime", "DepartureStation", "TrainLineID" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 9, 3, 6, 35, 0, 0, DateTimeKind.Unspecified), "SOUTHERN CROSS STATION", new DateTime(2018, 9, 3, 5, 15, 0, 0, DateTimeKind.Unspecified), "WENDOUREE", 1 },
                    { 2, new DateTime(2018, 9, 5, 8, 6, 0, 0, DateTimeKind.Unspecified), "SOUTHERN CROSS STATION", new DateTime(2018, 9, 5, 5, 58, 0, 0, DateTimeKind.Unspecified), "EPSOM STATION", 2 },
                    { 3, new DateTime(2018, 9, 7, 7, 20, 0, 0, DateTimeKind.Unspecified), "WAURN PONDS STATION", new DateTime(2018, 9, 7, 6, 0, 0, 0, DateTimeKind.Unspecified), "SOUTHERN CROSS STATION", 3 },
                    { 4, new DateTime(2018, 9, 10, 6, 58, 0, 0, DateTimeKind.Unspecified), "SOUTHERN CROSS STATION", new DateTime(2018, 9, 10, 4, 35, 0, 0, DateTimeKind.Unspecified), "TRARALGON STATION", 4 },
                    { 5, new DateTime(2018, 9, 12, 7, 44, 0, 0, DateTimeKind.Unspecified), "SEYMOUR", new DateTime(2018, 9, 12, 6, 12, 0, 0, DateTimeKind.Unspecified), "SOUTHERN CROSS STATION", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TrainLineID",
                table: "Schedules",
                column: "TrainLineID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "TrainLines");

            migrationBuilder.DropSequence(
                name: "ScheduleSeq");
        }
    }
}
