using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoHome.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IntegrationDeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Uri = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Key = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSecret = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => new { x.Id, x.Key });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WeatherReadings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EpochMilliseconds = table.Column<long>(type: "bigint", nullable: true),
                    IndoorTemperatureFahrenheit = table.Column<double>(type: "double", nullable: true),
                    IndoorHumidity = table.Column<int>(type: "int", nullable: true),
                    RelativeBarometricPressure = table.Column<double>(type: "double", nullable: true),
                    AbsoluteBarometricPressure = table.Column<double>(type: "double", nullable: true),
                    OutdoorTemperatureFahrenheit = table.Column<double>(type: "double", nullable: true),
                    BatteryLowIndicator = table.Column<int>(type: "int", nullable: true),
                    OutdoorHumidity = table.Column<int>(type: "int", nullable: true),
                    WindDirection = table.Column<int>(type: "int", nullable: true),
                    WindSpeedMph = table.Column<double>(type: "double", nullable: true),
                    WindGustMph = table.Column<double>(type: "double", nullable: true),
                    MaxDailyGust = table.Column<double>(type: "double", nullable: true),
                    HourlyRainfall = table.Column<double>(type: "double", nullable: true),
                    EventRainfall = table.Column<double>(type: "double", nullable: true),
                    DailyRainfall = table.Column<double>(type: "double", nullable: true),
                    WeeklyRainfall = table.Column<double>(type: "double", nullable: true),
                    MonthlyRainfall = table.Column<double>(type: "double", nullable: true),
                    YearlyRainfall = table.Column<double>(type: "double", nullable: true),
                    TotalRainfall = table.Column<double>(type: "double", nullable: true),
                    SolarRadiation = table.Column<double>(type: "double", nullable: true),
                    UltravioletRadiationIndex = table.Column<int>(type: "int", nullable: true),
                    OutdoorFeelsLikeTemperatureFahrenheit = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit = table.Column<double>(type: "double", nullable: true),
                    IndoorFeelsLikeTemperatureFahrenheit = table.Column<double>(type: "double", nullable: true),
                    IndoorDewPointTemperatureFahrenheit = table.Column<double>(type: "double", nullable: true),
                    LastRain = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Loc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UtcDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    WindGustDir = table.Column<int>(type: "int", nullable: true),
                    WindSpeedMph2MinuteAverage = table.Column<double>(type: "double", nullable: true),
                    WindDirection2MinuteAverage = table.Column<int>(type: "int", nullable: true),
                    WindSpeedMph10MinuteAverage = table.Column<double>(type: "double", nullable: true),
                    WindDirection10MinuteAverage = table.Column<int>(type: "int", nullable: true),
                    PM25AirQualityBatteryLowIndicator = table.Column<int>(type: "int", nullable: true),
                    Previous24HourRainfall = table.Column<double>(type: "double", nullable: true),
                    CO2PartsPerMillion = table.Column<double>(type: "double", nullable: true),
                    CO2SensorBatteryLowIndicator = table.Column<int>(type: "int", nullable: false),
                    PM25OutdoorAirQuality = table.Column<double>(type: "double", nullable: true),
                    PM25OutdoorAirQuality24HourAverage = table.Column<double>(type: "double", nullable: true),
                    PM25IndoorAirQuality = table.Column<double>(type: "double", nullable: true),
                    PM25IndoorAirQuality24HourAverage = table.Column<double>(type: "double", nullable: true),
                    HumiditySensor1 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor2 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor3 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor4 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor5 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor6 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor7 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor8 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor9 = table.Column<int>(type: "int", nullable: true),
                    HumiditySensor10 = table.Column<int>(type: "int", nullable: true),
                    TemperatureSensor1 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor2 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor3 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor4 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor5 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor6 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor7 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor8 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor9 = table.Column<double>(type: "double", nullable: true),
                    TemperatureSensor10 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor1 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor2 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor3 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor4 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor5 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor6 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor7 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor8 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor9 = table.Column<double>(type: "double", nullable: true),
                    SoilTemperatureSensor10 = table.Column<double>(type: "double", nullable: true),
                    SoilHumiditySensor1 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor2 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor3 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor4 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor5 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor6 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor7 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor8 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor9 = table.Column<int>(type: "int", nullable: true),
                    SoilHumiditySensor10 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator1 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator2 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator3 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator4 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator5 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator6 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator7 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator8 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator9 = table.Column<int>(type: "int", nullable: true),
                    BatteryLowIndicator10 = table.Column<int>(type: "int", nullable: true),
                    Relay1 = table.Column<int>(type: "int", nullable: true),
                    Relay2 = table.Column<int>(type: "int", nullable: true),
                    Relay3 = table.Column<int>(type: "int", nullable: true),
                    Relay4 = table.Column<int>(type: "int", nullable: true),
                    Relay5 = table.Column<int>(type: "int", nullable: true),
                    Relay6 = table.Column<int>(type: "int", nullable: true),
                    Relay7 = table.Column<int>(type: "int", nullable: true),
                    Relay8 = table.Column<int>(type: "int", nullable: true),
                    Relay9 = table.Column<int>(type: "int", nullable: true),
                    Relay10 = table.Column<int>(type: "int", nullable: true),
                    IANATimeZone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FeelsLikeTemperatureFahrenheit1 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit2 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit3 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit4 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit5 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit6 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit7 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit8 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit9 = table.Column<double>(type: "double", nullable: true),
                    FeelsLikeTemperatureFahrenheit10 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit1 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit2 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit3 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit4 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit5 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit6 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit7 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit8 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit9 = table.Column<double>(type: "double", nullable: true),
                    DewPointFahrenheit10 = table.Column<double>(type: "double", nullable: true),
                    LightningStrikesPerDay = table.Column<int>(type: "int", nullable: true),
                    LightningStrikesPerHours = table.Column<int>(type: "int", nullable: true),
                    LastLightningStrikeTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LightningStrikeDistance = table.Column<double>(type: "double", nullable: true),
                    MacAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReadings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Interval = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triggers_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TriggerEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TriggerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TimeStamp = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Event = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerEvents_Triggers_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Triggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerEvents_TriggerId",
                table: "TriggerEvents",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_DeviceId",
                table: "Triggers",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TriggerEvents");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "WeatherReadings");

            migrationBuilder.DropTable(
                name: "Triggers");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
