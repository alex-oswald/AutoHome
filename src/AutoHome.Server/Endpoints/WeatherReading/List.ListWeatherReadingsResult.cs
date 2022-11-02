namespace AutoHome.Server.Endpoints.WeatherReading;

public class ListWeatherReadingsResult
{
    public Guid Id { get; set; }


    public long? EpochMilliseconds { get; init; }


    public double? IndoorTemperatureFahrenheit { get; init; }


    public int? IndoorHumidity { get; init; }


    public double? RelativeBarometricPressure { get; init; }


    public double? AbsoluteBarometricPressure { get; init; }


    public double? OutdoorTemperatureFahrenheit { get; init; }


    public int? BatteryLowIndicator { get; init; }


    public int? OutdoorHumidity { get; init; }


    public int? WindDirection { get; init; }


    public double? WindSpeedMph { get; init; }


    public double? WindGustMph { get; init; }


    public double? MaxDailyGust { get; init; }


    public double? HourlyRainfall { get; init; }


    public double? EventRainfall { get; init; }


    public double? DailyRainfall { get; init; }


    public double? WeeklyRainfall { get; init; }


    public double? MonthlyRainfall { get; init; }


    public double? YearlyRainfall { get; init; }


    public double? TotalRainfall { get; init; }


    public double? SolarRadiation { get; init; }


    public int? UltravioletRadiationIndex { get; init; }


    public double? OutdoorFeelsLikeTemperatureFahrenheit { get; init; }


    public double? DewPointFahrenheit { get; init; }


    public double? IndoorFeelsLikeTemperatureFahrenheit { get; init; }


    public double? IndoorDewPointTemperatureFahrenheit { get; init; }


    public DateTimeOffset LastRain { get; init; }


    //public string? Loc { get; init; }


    public DateTimeOffset? UtcDate { get; init; }


    //public int? WindGustDir { get; init; }


    //public double? WindSpeedMph2MinuteAverage { get; init; }


    //public int? WindDirection2MinuteAverage { get; init; }


    //public double? WindSpeedMph10MinuteAverage { get; init; }


    //public int? WindDirection10MinuteAverage { get; init; }


    //public int? PM25AirQualityBatteryLowIndicator { get; init; }


    //public double? Previous24HourRainfall { get; init; }


    //public double? CO2PartsPerMillion { get; init; }


    //public int CO2SensorBatteryLowIndicator { get; init; }


    //public double? PM25OutdoorAirQuality { get; init; }


    //public double? PM25OutdoorAirQuality24HourAverage { get; init; }


    //public double? PM25IndoorAirQuality { get; init; }


    //public double? PM25IndoorAirQuality24HourAverage { get; init; }


    //public int? HumiditySensor1 { get; init; }


    //public int? HumiditySensor2 { get; init; }


    //public int? HumiditySensor3 { get; init; }


    //public int? HumiditySensor4 { get; init; }


    //public int? HumiditySensor5 { get; init; }


    //public int? HumiditySensor6 { get; init; }


    //public int? HumiditySensor7 { get; init; }


    //public int? HumiditySensor8 { get; init; }


    //public int? HumiditySensor9 { get; init; }


    //public int? HumiditySensor10 { get; init; }


    //public double? TemperatureSensor1 { get; init; }


    //public double? TemperatureSensor2 { get; init; }


    //public double? TemperatureSensor3 { get; init; }


    //public double? TemperatureSensor4 { get; init; }


    //public double? TemperatureSensor5 { get; init; }


    //public double? TemperatureSensor6 { get; init; }


    //public double? TemperatureSensor7 { get; init; }


    //public double? TemperatureSensor8 { get; init; }


    //public double? TemperatureSensor9 { get; init; }


    //public double? TemperatureSensor10 { get; init; }


    //public double? SoilTemperatureSensor1 { get; init; }


    //public double? SoilTemperatureSensor2 { get; init; }


    //public double? SoilTemperatureSensor3 { get; init; }


    //public double? SoilTemperatureSensor4 { get; init; }


    //public double? SoilTemperatureSensor5 { get; init; }


    //public double? SoilTemperatureSensor6 { get; init; }


    //public double? SoilTemperatureSensor7 { get; init; }


    //public double? SoilTemperatureSensor8 { get; init; }


    //public double? SoilTemperatureSensor9 { get; init; }


    //public double? SoilTemperatureSensor10 { get; init; }


    //public int? SoilHumiditySensor1 { get; init; }


    //public int? SoilHumiditySensor2 { get; init; }


    //public int? SoilHumiditySensor3 { get; init; }


    //public int? SoilHumiditySensor4 { get; init; }


    //public int? SoilHumiditySensor5 { get; init; }


    //public int? SoilHumiditySensor6 { get; init; }


    //public int? SoilHumiditySensor7 { get; init; }


    //public int? SoilHumiditySensor8 { get; init; }


    //public int? SoilHumiditySensor9 { get; init; }


    //public int? SoilHumiditySensor10 { get; init; }


    //public int? BatteryLowIndicator1 { get; init; }


    //public int? BatteryLowIndicator2 { get; init; }


    //public int? BatteryLowIndicator3 { get; init; }


    //public int? BatteryLowIndicator4 { get; init; }


    //public int? BatteryLowIndicator5 { get; init; }


    //public int? BatteryLowIndicator6 { get; init; }


    //public int? BatteryLowIndicator7 { get; init; }


    //public int? BatteryLowIndicator8 { get; init; }


    //public int? BatteryLowIndicator9 { get; init; }


    //public int? BatteryLowIndicator10 { get; init; }


    //public int? Relay1 { get; init; }


    //public int? Relay2 { get; init; }


    //public int? Relay3 { get; init; }


    //public int? Relay4 { get; init; }


    //public int? Relay5 { get; init; }


    //public int? Relay6 { get; init; }


    //public int? Relay7 { get; init; }


    //public int? Relay8 { get; init; }


    //public int? Relay9 { get; init; }


    //public int? Relay10 { get; init; }


    //public string? IANATimeZone { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit1 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit2 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit3 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit4 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit5 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit6 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit7 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit8 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit9 { get; init; }


    //public double? FeelsLikeTemperatureFahrenheit10 { get; init; }


    //public double? DewPointFahrenheit1 { get; init; }


    //public double? DewPointFahrenheit2 { get; init; }


    //public double? DewPointFahrenheit3 { get; init; }


    //public double? DewPointFahrenheit4 { get; init; }


    //public double? DewPointFahrenheit5 { get; init; }


    //public double? DewPointFahrenheit6 { get; init; }


    //public double? DewPointFahrenheit7 { get; init; }


    //public double? DewPointFahrenheit8 { get; init; }


    //public double? DewPointFahrenheit9 { get; init; }


    //public double? DewPointFahrenheit10 { get; init; }


    //public int? LightningStrikesPerDay { get; init; }


    //public int? LightningStrikesPerHours { get; init; }


    //public DateTimeOffset? LastLightningStrikeTime { get; init; }


    //public double? LightningStrikeDistance { get; init; }


    //public string? MacAddress { get; init; }
}
