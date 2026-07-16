using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReservationSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenreTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HallTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokenTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokenTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatTable_HallTable_HallId",
                        column: x => x.HallId,
                        principalTable: "HallTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenreTable",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenreTable", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenreTable_GenreTable_GenreId",
                        column: x => x.GenreId,
                        principalTable: "GenreTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenreTable_MovieTable_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowtimeTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowtimeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowtimeTable_HallTable_HallId",
                        column: x => x.HallId,
                        principalTable: "HallTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowtimeTable_MovieTable_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    ReservationStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTable", x => x.Id);
                    table.CheckConstraint("CK_ReservationTable_ReservationStatus", "ReservationStatus IN (0, 1, 2)");
                    table.ForeignKey(
                        name: "FK_ReservationTable_ShowtimeTable_ShowtimeId",
                        column: x => x.ShowtimeId,
                        principalTable: "ShowtimeTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservedSeatTable",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservedSeatTable", x => new { x.SeatId, x.ShowtimeId });
                    table.ForeignKey(
                        name: "FK_ReservedSeatTable_ReservationTable_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "ReservationTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservedSeatTable_SeatTable_SeatId",
                        column: x => x.SeatId,
                        principalTable: "SeatTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenreTable_GenreId",
                table: "MovieGenreTable",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTable_Description",
                table: "MovieTable",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTable_Title",
                table: "MovieTable",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTable_ShowtimeId",
                table: "ReservationTable",
                column: "ShowtimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTable_UserId",
                table: "ReservationTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedSeatTable_ReservationId",
                table: "ReservedSeatTable",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedSeatTable_ShowtimeId_SeatId",
                table: "ReservedSeatTable",
                columns: new[] { "ShowtimeId", "SeatId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatTable_HallId",
                table: "SeatTable",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeTable_HallId",
                table: "ShowtimeTable",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeTable_MovieId",
                table: "ShowtimeTable",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_Email",
                table: "UserTable",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenreTable");

            migrationBuilder.DropTable(
                name: "RefreshTokenTable");

            migrationBuilder.DropTable(
                name: "ReservedSeatTable");

            migrationBuilder.DropTable(
                name: "GenreTable");

            migrationBuilder.DropTable(
                name: "ReservationTable");

            migrationBuilder.DropTable(
                name: "SeatTable");

            migrationBuilder.DropTable(
                name: "ShowtimeTable");

            migrationBuilder.DropTable(
                name: "UserTable");

            migrationBuilder.DropTable(
                name: "HallTable");

            migrationBuilder.DropTable(
                name: "MovieTable");
        }
    }
}
