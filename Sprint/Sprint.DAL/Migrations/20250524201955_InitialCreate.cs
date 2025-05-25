using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sprint.DAL.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourtNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTrainer = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtReservations_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourtReservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerPhotos_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourtReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerReservations_CourtReservations_CourtReservationId",
                        column: x => x.CourtReservationId,
                        principalTable: "CourtReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerReservations_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainerReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerReviews_TrainerReservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "TrainerReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "CourtNumber", "HourlyRate", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("3cf12330-a729-46b7-a157-26f885ef0485"), "D", 500m, false },
                    { new Guid("94dc0241-8559-43a9-b3b7-f1afacaf5db8"), "B", 800m, false },
                    { new Guid("9861fc0e-c019-46d1-a6cc-7493f5c472a9"), "C", 800m, false },
                    { new Guid("b20733f6-5e38-4b35-9b25-2acf9aa9d69f"), "A", 1000m, false }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "IsDeleted", "IsTrainer", "LastName", "PasswordHash", "PhotoPath", "Role", "SecurityStamp" },
                values: new object[,]
                {
                    { new Guid("2142d07c-15ef-4162-8e6a-b1d14f34c7c7"), new DateTime(1955, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "monic@nonexistentmail.com", "Monica", false, false, "Bellucci", "AQAAAAEAACcQAAAAEFYDAxi2JUB0XAOJMRelra7pPklyW3I6XQaw9cOe33/ic9+BavRdOF9tp9qS6bJGLQ==", "https://img3.stockfresh.com/files/w/wavebreak_media/m/12/8944331_stock-photo-badminton-player-holding-racket-and-shuttlecock.jpg", 0, "4abcdef" },
                    { new Guid("29b36392-6efa-4000-a4d1-0c45951e34dd"), new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "493352@mail.muni.cz", "Jitka", false, true, "Viceníková", "AQAAAAEAACcQAAAAEFYDAxi2JUB0XAOJMRelra7pPklyW3I6XQaw9cOe33/ic9+BavRdOF9tp9qS6bJGLQ==", "https://img3.stockfresh.com/files/w/wavebreak_media/m/12/8944331_stock-photo-badminton-player-holding-racket-and-shuttlecock.jpg", 1, "6abcdef" },
                    { new Guid("3bb62eca-1148-48c4-aea1-a1afd822e754"), new DateTime(1995, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "pppeter@nonexistentmail.com", "Peter", false, false, "Griffin", "AQAAAAEAACcQAAAAEFYDAxi2JUB0XAOJMRelra7pPklyW3I6XQaw9cOe33/ic9+BavRdOF9tp9qS6bJGLQ==", "https://img3.stockfresh.com/files/w/wavebreak_media/m/12/8944331_stock-photo-badminton-player-holding-racket-and-shuttlecock.jpg", 0, "2abcdef" },
                    { new Guid("92b7e4cf-4a7e-445d-87b1-e1b22c3ae743"), new DateTime(1993, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rntr@nonexistentmail.com", "Roman", false, false, "NieTenRoman", "AQAAAAEAACcQAAAAEFYDAxi2JUB0XAOJMRelra7pPklyW3I6XQaw9cOe33/ic9+BavRdOF9tp9qS6bJGLQ==", "https://img3.stockfresh.com/files/w/wavebreak_media/m/12/8944331_stock-photo-badminton-player-holding-racket-and-shuttlecock.jpg", 0, "3abcdef" },
                    { new Guid("9c13cb1f-cf48-4b42-acc4-9e2df5c714f8"), new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@sprint.cz", "Admin", false, true, "Admin", "AQAAAAEAACcQAAAAEFYDAxi2JUB0XAOJMRelra7pPklyW3I6XQaw9cOe33/ic9+BavRdOF9tp9qS6bJGLQ==", "https://img3.stockfresh.com/files/w/wavebreak_media/m/12/8944331_stock-photo-badminton-player-holding-racket-and-shuttlecock.jpg", 2, "6abcdef" },
                    { new Guid("f9d5aaee-5282-4b8d-81c8-e4fbd4a7dea5"), new DateTime(2001, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "514329@mail.muni.cz", "Adam", false, false, "Mydla", "AQAAAAEAACcQAAAAEFYDAxi2JUB0XAOJMRelra7pPklyW3I6XQaw9cOe33/ic9+BavRdOF9tp9qS6bJGLQ==", "https://img3.stockfresh.com/files/w/wavebreak_media/m/12/8944331_stock-photo-badminton-player-holding-racket-and-shuttlecock.jpg", 0, "1abcdef" },
                    { new Guid("fd7603ed-c854-47a9-a1b1-d29c6331b9b8"), new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "rhanculak@mail.muni.cz", "Radovan", false, true, "Hančuľák", "AQAAAAEAACcQAAAAEFYDAxi2JUB0XAOJMRelra7pPklyW3I6XQaw9cOe33/ic9+BavRdOF9tp9qS6bJGLQ==", "https://img3.stockfresh.com/files/w/wavebreak_media/m/12/8944331_stock-photo-badminton-player-holding-racket-and-shuttlecock.jpg", 1, "5abcdef" }
                });

            migrationBuilder.InsertData(
                table: "CourtReservations",
                columns: new[] { "Id", "CourtId", "Created", "From", "IsDeleted", "To", "UserId" },
                values: new object[,]
                {
                    { new Guid("032e0700-793a-4f1a-940c-6e363a7f8595"), new Guid("9861fc0e-c019-46d1-a6cc-7493f5c472a9"), new DateTime(2022, 9, 21, 6, 51, 34, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 9, 30, 13, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f9d5aaee-5282-4b8d-81c8-e4fbd4a7dea5") },
                    { new Guid("3cc5ea0a-234f-44f5-bc40-b9e30d99a26e"), new Guid("94dc0241-8559-43a9-b3b7-f1afacaf5db8"), new DateTime(2022, 9, 15, 18, 4, 4, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 16, 13, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 9, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new Guid("92b7e4cf-4a7e-445d-87b1-e1b22c3ae743") },
                    { new Guid("626a4852-3fd6-4b73-87a5-cde2b2fc330b"), new Guid("94dc0241-8559-43a9-b3b7-f1afacaf5db8"), new DateTime(2022, 9, 16, 9, 53, 34, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2022, 9, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("92b7e4cf-4a7e-445d-87b1-e1b22c3ae743") },
                    { new Guid("7172178f-9529-409a-8ea1-1f9cd9a6c7db"), new Guid("b20733f6-5e38-4b35-9b25-2acf9aa9d69f"), new DateTime(2022, 9, 28, 19, 19, 19, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 10, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3bb62eca-1148-48c4-aea1-a1afd822e754") },
                    { new Guid("aba9140c-2fbe-4716-823e-a80076dbea05"), new Guid("b20733f6-5e38-4b35-9b25-2acf9aa9d69f"), new DateTime(2022, 9, 20, 9, 53, 34, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 29, 11, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 9, 29, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3bb62eca-1148-48c4-aea1-a1afd822e754") },
                    { new Guid("eaa31107-6912-4f8e-9194-1c0c065d1a13"), new Guid("b20733f6-5e38-4b35-9b25-2acf9aa9d69f"), new DateTime(2022, 9, 20, 9, 53, 34, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 29, 10, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 9, 29, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f9d5aaee-5282-4b8d-81c8-e4fbd4a7dea5") },
                    { new Guid("f037f5cc-5268-4798-9d37-e8e11c2c0f4b"), new Guid("3cf12330-a729-46b7-a157-26f885ef0485"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 31, 10, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 12, 31, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2142d07c-15ef-4162-8e6a-b1d14f34c7c7") },
                    { new Guid("ffad232c-e49c-4c17-8951-8ef6e798a137"), new Guid("9861fc0e-c019-46d1-a6cc-7493f5c472a9"), new DateTime(2022, 9, 5, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 17, 8, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2022, 11, 17, 16, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f9d5aaee-5282-4b8d-81c8-e4fbd4a7dea5") }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Description", "HourlyRate", "IsDeleted", "UserId" },
                values: new object[,]
                {
                    { new Guid("4f4a3974-482c-4322-82ed-097696f99044"), "I know how to play, that's all", 2000m, false, new Guid("fd7603ed-c854-47a9-a1b1-d29c6331b9b8") },
                    { new Guid("8b405a02-5743-42ca-8a9a-3f2169c8304c"), "I know how to play even better, that's all", 3485m, false, new Guid("29b36392-6efa-4000-a4d1-0c45951e34dd") }
                });

            migrationBuilder.InsertData(
                table: "TrainerPhotos",
                columns: new[] { "Id", "Hide", "IsDeleted", "Path", "TrainerId" },
                values: new object[,]
                {
                    { new Guid("0beaf8c1-5e2a-48db-a3f1-d4724eb04ec9"), false, false, "https://static8.depositphotos.com/1460388/938/i/600/depositphotos_9388060-stock-photo-next-rally.jpg", new Guid("4f4a3974-482c-4322-82ed-097696f99044") },
                    { new Guid("6632f38a-098c-4188-bf6c-0997d33f7d09"), true, false, "https://static8.depositphotos.com/1460388/938/i/600/depositphotos_9388060-stock-photo-next-rally.jpg", new Guid("8b405a02-5743-42ca-8a9a-3f2169c8304c") },
                    { new Guid("78895bb4-efee-473e-9018-658a95eaaec8"), false, false, "https://static8.depositphotos.com/1460388/938/i/600/depositphotos_9388060-stock-photo-next-rally.jpg", new Guid("4f4a3974-482c-4322-82ed-097696f99044") },
                    { new Guid("cd757cf4-c84b-4bab-ba03-a2e4ab9c87f5"), true, false, "https://static8.depositphotos.com/1460388/938/i/600/depositphotos_9388060-stock-photo-next-rally.jpg", new Guid("8b405a02-5743-42ca-8a9a-3f2169c8304c") }
                });

            migrationBuilder.InsertData(
                table: "TrainerReservations",
                columns: new[] { "Id", "CourtReservationId", "IsDeleted", "TrainerId" },
                values: new object[,]
                {
                    { new Guid("32c69f2b-e44f-4e1a-8758-0549c1f7de92"), new Guid("3cc5ea0a-234f-44f5-bc40-b9e30d99a26e"), false, new Guid("4f4a3974-482c-4322-82ed-097696f99044") },
                    { new Guid("33f29cef-6644-4cb8-b607-16d266575767"), new Guid("f037f5cc-5268-4798-9d37-e8e11c2c0f4b"), false, new Guid("8b405a02-5743-42ca-8a9a-3f2169c8304c") },
                    { new Guid("774cb8b1-ce75-45f5-b256-d81b9dcdcc0a"), new Guid("626a4852-3fd6-4b73-87a5-cde2b2fc330b"), true, new Guid("4f4a3974-482c-4322-82ed-097696f99044") }
                });

            migrationBuilder.InsertData(
                table: "TrainerReviews",
                columns: new[] { "Id", "Hide", "IsDeleted", "Rating", "ReservationId", "Text" },
                values: new object[] { new Guid("4f609a6c-d3be-459c-95e2-f8df4debfa04"), false, false, 5, new Guid("33f29cef-6644-4cb8-b607-16d266575767"), "Awesome!" });

            migrationBuilder.CreateIndex(
                name: "IX_CourtReservations_CourtId",
                table: "CourtReservations",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtReservations_UserId",
                table: "CourtReservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPhotos_TrainerId",
                table: "TrainerPhotos",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReservations_CourtReservationId",
                table: "TrainerReservations",
                column: "CourtReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReservations_TrainerId",
                table: "TrainerReservations",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReviews_ReservationId",
                table: "TrainerReviews",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerPhotos");

            migrationBuilder.DropTable(
                name: "TrainerReviews");

            migrationBuilder.DropTable(
                name: "TrainerReservations");

            migrationBuilder.DropTable(
                name: "CourtReservations");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
