using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoHome.Data.Migrations
{
    public partial class AddWeatherReadingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSecret",
                table: "Variables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "WeatherReadings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EpochMilliseconds = table.Column<long>(type: "INTEGER", nullable: true),
                    IndoorTemperatureFahrenheit = table.Column<double>(type: "REAL", nullable: true),
                    IndoorHumidity = table.Column<int>(type: "INTEGER", nullable: true),
                    RelativeBarometricPressure = table.Column<double>(type: "REAL", nullable: true),
                    AbsoluteBarometricPressure = table.Column<double>(type: "REAL", nullable: true),
                    OutdoorTemperatureFahrenheit = table.Column<double>(type: "REAL", nullable: true),
                    BatteryLowIndicator = table.Column<int>(type: "INTEGER", nullable: true),
                    OutdoorHumidity = table.Column<int>(type: "INTEGER", nullable: true),
                    WindDirection = table.Column<int>(type: "INTEGER", nullable: true),
                    WindSpeedMph = table.Column<double>(type: "REAL", nullable: true),
                    WindGustMph = table.Column<double>(type: "REAL", nullable: true),
                    MaxDailyGust = table.Column<double>(type: "REAL", nullable: true),
                    HourlyRainfall = table.Column<double>(type: "REAL", nullable: true),
                    EventRainfall = table.Column<double>(type: "REAL", nullable: true),
                    DailyRainfall = table.Column<double>(type: "REAL", nullable: true),
                    WeeklyRainfall = table.Column<double>(type: "REAL", nullable: true),
                    MonthlyRainfall = table.Column<double>(type: "REAL", nullable: true),
                    YearlyRainfall = table.Column<double>(type: "REAL", nullable: true),
                    TotalRainfall = table.Column<double>(type: "REAL", nullable: true),
                    SolarRadiation = table.Column<double>(type: "REAL", nullable: true),
                    UltravioletRadiationIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    OutdoorFeelsLikeTemperatureFahrenheit = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit = table.Column<double>(type: "REAL", nullable: true),
                    IndoorFeelsLikeTemperatureFahrenheit = table.Column<double>(type: "REAL", nullable: true),
                    IndoorDewPointTemperatureFahrenheit = table.Column<double>(type: "REAL", nullable: true),
                    LastRain = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Loc = table.Column<string>(type: "TEXT", nullable: true),
                    UtcDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    WindGustDir = table.Column<int>(type: "INTEGER", nullable: true),
                    WindSpeedMph2MinuteAverage = table.Column<double>(type: "REAL", nullable: true),
                    WindDirection2MinuteAverage = table.Column<int>(type: "INTEGER", nullable: true),
                    WindSpeedMph10MinuteAverage = table.Column<double>(type: "REAL", nullable: true),
                    WindDirection10MinuteAverage = table.Column<int>(type: "INTEGER", nullable: true),
                    PM25AirQualityBatteryLowIndicator = table.Column<int>(type: "INTEGER", nullable: true),
                    Previous24HourRainfall = table.Column<double>(type: "REAL", nullable: true),
                    CO2PartsPerMillion = table.Column<double>(type: "REAL", nullable: true),
                    CO2SensorBatteryLowIndicator = table.Column<int>(type: "INTEGER", nullable: false),
                    PM25OutdoorAirQuality = table.Column<double>(type: "REAL", nullable: true),
                    PM25OutdoorAirQuality24HourAverage = table.Column<double>(type: "REAL", nullable: true),
                    PM25IndoorAirQuality = table.Column<double>(type: "REAL", nullable: true),
                    PM25IndoorAirQuality24HourAverage = table.Column<double>(type: "REAL", nullable: true),
                    HumiditySensor1 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor2 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor3 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor4 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor5 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor6 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor7 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor8 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor9 = table.Column<int>(type: "INTEGER", nullable: true),
                    HumiditySensor10 = table.Column<int>(type: "INTEGER", nullable: true),
                    TemperatureSensor1 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor2 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor3 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor4 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor5 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor6 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor7 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor8 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor9 = table.Column<double>(type: "REAL", nullable: true),
                    TemperatureSensor10 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor1 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor2 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor3 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor4 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor5 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor6 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor7 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor8 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor9 = table.Column<double>(type: "REAL", nullable: true),
                    SoilTemperatureSensor10 = table.Column<double>(type: "REAL", nullable: true),
                    SoilHumiditySensor1 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor2 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor3 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor4 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor5 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor6 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor7 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor8 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor9 = table.Column<int>(type: "INTEGER", nullable: true),
                    SoilHumiditySensor10 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator1 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator2 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator3 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator4 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator5 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator6 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator7 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator8 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator9 = table.Column<int>(type: "INTEGER", nullable: true),
                    BatteryLowIndicator10 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay2 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay3 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay4 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay5 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay6 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay7 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay8 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay9 = table.Column<int>(type: "INTEGER", nullable: true),
                    Relay10 = table.Column<int>(type: "INTEGER", nullable: true),
                    IANATimeZone = table.Column<string>(type: "TEXT", nullable: true),
                    FeelsLikeTemperatureFahrenheit1 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit2 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit3 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit4 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit5 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit6 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit7 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit8 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit9 = table.Column<double>(type: "REAL", nullable: true),
                    FeelsLikeTemperatureFahrenheit10 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit1 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit2 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit3 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit4 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit5 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit6 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit7 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit8 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit9 = table.Column<double>(type: "REAL", nullable: true),
                    DewPointFahrenheit10 = table.Column<double>(type: "REAL", nullable: true),
                    LightningStrikesPerDay = table.Column<int>(type: "INTEGER", nullable: true),
                    LightningStrikesPerHours = table.Column<int>(type: "INTEGER", nullable: true),
                    LastLightningStrikeTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LightningStrikeDistance = table.Column<double>(type: "REAL", nullable: true),
                    MacAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReadings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherReadings");

            migrationBuilder.DropColumn(
                name: "IsSecret",
                table: "Variables");
        }
    }
}
