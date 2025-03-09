using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DynamicSun.Weather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "WeatherData",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeatherDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Temperature = table.Column<double>(type: "numeric(7,2)", nullable: false),
                    RelativeHumidity = table.Column<double>(type: "numeric(5,2)", nullable: false),
                    DewPoint = table.Column<double>(type: "numeric(7,2)", nullable: false),
                    AtmosphericPressure = table.Column<double>(type: "numeric(7,2)", nullable: false),
                    WindDirection = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    WindSpeed = table.Column<double>(type: "numeric(7,2)", nullable: true),
                    Cloudiness = table.Column<double>(type: "numeric(5,2)", nullable: true),
                    CloudBaseHeight = table.Column<double>(type: "numeric(7,2)", nullable: false),
                    Visibility = table.Column<string>(type: "text", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "text", nullable: true),
                    ObjectCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ObjectEditDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData",
                schema: "public");
        }
    }
}
