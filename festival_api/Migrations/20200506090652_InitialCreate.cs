using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace festival_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ArtistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistName = table.Column<string>(nullable: true),
                    ArtistEmail = table.Column<string>(nullable: true),
                    ArtistPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(nullable: true),
                    VenueStreet = table.Column<string>(nullable: true),
                    VenueCity = table.Column<string>(nullable: true),
                    VenueState = table.Column<string>(nullable: true),
                    VenueZip = table.Column<string>(nullable: true),
                    VenueCountry = table.Column<string>(nullable: true),
                    VenueSeats = table.Column<int>(nullable: false),
                    ServesAlcohol = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gigs",
                columns: table => new
                {
                    GigId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GigHeadline = table.Column<string>(nullable: true),
                    GigLengthInMinutes = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: true),
                    ArtistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gigs", x => x.GigId);
                    table.ForeignKey(
                        name: "FK_Gigs_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gigs_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "ArtistEmail", "ArtistName", "ArtistPhone" },
                values: new object[] { 1, "c64@gmail.com", "C64's", "111-222-3333" });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "ArtistEmail", "ArtistName", "ArtistPhone" },
                values: new object[] { 2, "plasma@gmail.com", "Plasma Screen", "444-555-6666" });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "ServesAlcohol", "VenueCity", "VenueCountry", "VenueName", "VenueSeats", "VenueState", "VenueStreet", "VenueZip" },
                values: new object[] { 1, true, "Du Bois", "USA", "Rock Hall", 145, "PA", "123 Main Street", "18702" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EventDate", "EventName", "VenueId" },
                values: new object[] { 1, new DateTime(2021, 6, 23, 19, 30, 0, 0, DateTimeKind.Unspecified), "Big Splash", 1 });

            migrationBuilder.InsertData(
                table: "Gigs",
                columns: new[] { "GigId", "ArtistId", "EventId", "GigHeadline", "GigLengthInMinutes" },
                values: new object[] { 1, 1, 1, "Rumble", 60 });

            migrationBuilder.InsertData(
                table: "Gigs",
                columns: new[] { "GigId", "ArtistId", "EventId", "GigHeadline", "GigLengthInMinutes" },
                values: new object[] { 2, 2, 1, "Boston Tea Party", 70 });

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_ArtistId",
                table: "Gigs",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_EventId",
                table: "Gigs",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gigs");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
