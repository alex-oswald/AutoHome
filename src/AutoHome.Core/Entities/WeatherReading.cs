using System.Text.Json.Serialization;

namespace AutoHome.Core.Entities;

public class WeatherReading : IEntity
{
    public Guid Id { get; set; }

    [JsonPropertyName("dateutc")]
    public long? EpochMilliseconds { get; init; }

    [JsonPropertyName("tempinf")]
    public double? IndoorTemperatureFahrenheit { get; init; }

    [JsonPropertyName("humidityin")]
    public int? IndoorHumidity { get; init; }

    [JsonPropertyName("baromrelin")]
    public double? RelativeBarometricPressure { get; init; }

    [JsonPropertyName("baromabsin")]
    public double? AbsoluteBarometricPressure { get; init; }

    [JsonPropertyName("tempf")]
    public double? OutdoorTemperatureFahrenheit { get; init; }

    [JsonPropertyName("battout")]
    public int? BatteryLowIndicator { get; init; }

    [JsonPropertyName("humidity")]
    public int? OutdoorHumidity { get; init; }

    [JsonPropertyName("winddir")]
    public int? WindDirection { get; init; }

    [JsonPropertyName("windspeedmph")]
    public double? WindSpeedMph { get; init; }

    [JsonPropertyName("windgustmph")]
    public double? WindGustMph { get; init; }

    [JsonPropertyName("maxdailygust")]
    public double? MaxDailyGust { get; init; }

    [JsonPropertyName("hourlyrainin")]
    public double? HourlyRainfall { get; init; }

    [JsonPropertyName("eventrainin")]
    public double? EventRainfall { get; init; }

    [JsonPropertyName("dailyrainin")]
    public double? DailyRainfall { get; init; }

    [JsonPropertyName("weeklyrainin")]
    public double? WeeklyRainfall { get; init; }

    [JsonPropertyName("monthlyrainin")]
    public double? MonthlyRainfall { get; init; }

    [JsonPropertyName("yearlyrainin")]
    public double? YearlyRainfall { get; init; }

    [JsonPropertyName("totalrainin")]
    public double? TotalRainfall { get; init; }

    [JsonPropertyName("solarradiation")]
    public double? SolarRadiation { get; init; }

    [JsonPropertyName("uv")]
    public int? UltravioletRadiationIndex { get; init; }

    [JsonPropertyName("feelsLike")]
    public double? OutdoorFeelsLikeTemperatureFahrenheit { get; init; }

    [JsonPropertyName("dewPoint")]
    public double? DewPointFahrenheit { get; init; }

    [JsonPropertyName("feelsLikein")]
    public double? IndoorFeelsLikeTemperatureFahrenheit { get; init; }

    [JsonPropertyName("dewPointin")]
    public double? IndoorDewPointTemperatureFahrenheit { get; init; }

    [JsonPropertyName("lastRain")]
    public DateTimeOffset LastRain { get; init; }

    [JsonPropertyName("loc")]
    public string? Loc { get; init; }

    [JsonPropertyName("date")]
    public DateTimeOffset? UtcDate { get; init; }

    [JsonPropertyName("windgustdir")]
    public int? WindGustDir { get; init; }

    [JsonPropertyName("windspdmph_avg2m")]
    public double? WindSpeedMph2MinuteAverage { get; init; }

    [JsonPropertyName("winddir_avg2m")]
    public int? WindDirection2MinuteAverage { get; init; }

    [JsonPropertyName("windspdmph_avg10m")]
    public double? WindSpeedMph10MinuteAverage { get; init; }

    [JsonPropertyName("winddir_avg10m")]
    public int? WindDirection10MinuteAverage { get; init; }

    [JsonPropertyName("batt_25")]
    public int? PM25AirQualityBatteryLowIndicator { get; init; }

    [JsonPropertyName("24hourrainin")]
    public double? Previous24HourRainfall { get; init; }

    [JsonPropertyName("co2")]
    public double? CO2PartsPerMillion { get; init; }

    [JsonPropertyName("batt_co2")]
    public int CO2SensorBatteryLowIndicator { get; init; }

    [JsonPropertyName("pm25")]
    public double? PM25OutdoorAirQuality { get; init; }

    [JsonPropertyName("pm25_24h")]
    public double? PM25OutdoorAirQuality24HourAverage { get; init; }

    [JsonPropertyName("pm25_in")]
    public double? PM25IndoorAirQuality { get; init; }

    [JsonPropertyName("pm25_in_24h")]
    public double? PM25IndoorAirQuality24HourAverage { get; init; }

    [JsonPropertyName("humidity1")]
    public int? HumiditySensor1 { get; init; }

    [JsonPropertyName("humidity2")]
    public int? HumiditySensor2 { get; init; }

    [JsonPropertyName("humidity3")]
    public int? HumiditySensor3 { get; init; }

    [JsonPropertyName("humidity4")]
    public int? HumiditySensor4 { get; init; }

    [JsonPropertyName("humidity5")]
    public int? HumiditySensor5 { get; init; }

    [JsonPropertyName("humidity6")]
    public int? HumiditySensor6 { get; init; }

    [JsonPropertyName("humidity7")]
    public int? HumiditySensor7 { get; init; }

    [JsonPropertyName("humidity8")]
    public int? HumiditySensor8 { get; init; }

    [JsonPropertyName("humidity9")]
    public int? HumiditySensor9 { get; init; }

    [JsonPropertyName("humidity10")]
    public int? HumiditySensor10 { get; init; }

    [JsonPropertyName("temp1f")]
    public double? TemperatureSensor1 { get; init; }

    [JsonPropertyName("temp2f")]
    public double? TemperatureSensor2 { get; init; }

    [JsonPropertyName("temp3f")]
    public double? TemperatureSensor3 { get; init; }

    [JsonPropertyName("temp4f")]
    public double? TemperatureSensor4 { get; init; }

    [JsonPropertyName("temp5f")]
    public double? TemperatureSensor5 { get; init; }

    [JsonPropertyName("temp6f")]
    public double? TemperatureSensor6 { get; init; }

    [JsonPropertyName("temp7f")]
    public double? TemperatureSensor7 { get; init; }

    [JsonPropertyName("temp8f")]
    public double? TemperatureSensor8 { get; init; }

    [JsonPropertyName("temp9f")]
    public double? TemperatureSensor9 { get; init; }

    [JsonPropertyName("temp10f")]
    public double? TemperatureSensor10 { get; init; }

    [JsonPropertyName("soiltemp1f")]
    public double? SoilTemperatureSensor1 { get; init; }

    [JsonPropertyName("soiltemp2f")]
    public double? SoilTemperatureSensor2 { get; init; }

    [JsonPropertyName("soiltemp3f")]
    public double? SoilTemperatureSensor3 { get; init; }

    [JsonPropertyName("soiltemp4f")]
    public double? SoilTemperatureSensor4 { get; init; }

    [JsonPropertyName("soiltemp5f")]
    public double? SoilTemperatureSensor5 { get; init; }

    [JsonPropertyName("soiltemp6f")]
    public double? SoilTemperatureSensor6 { get; init; }

    [JsonPropertyName("soiltemp7f")]
    public double? SoilTemperatureSensor7 { get; init; }

    [JsonPropertyName("soiltemp8f")]
    public double? SoilTemperatureSensor8 { get; init; }

    [JsonPropertyName("soiltemp9f")]
    public double? SoilTemperatureSensor9 { get; init; }

    [JsonPropertyName("soiltemp10f")]
    public double? SoilTemperatureSensor10 { get; init; }

    [JsonPropertyName("soilhum1")]
    public int? SoilHumiditySensor1 { get; init; }

    [JsonPropertyName("soilhum2")]
    public int? SoilHumiditySensor2 { get; init; }

    [JsonPropertyName("soilhum3")]
    public int? SoilHumiditySensor3 { get; init; }

    [JsonPropertyName("soilhum4")]
    public int? SoilHumiditySensor4 { get; init; }

    [JsonPropertyName("soilhum5")]
    public int? SoilHumiditySensor5 { get; init; }

    [JsonPropertyName("soilhum6")]
    public int? SoilHumiditySensor6 { get; init; }

    [JsonPropertyName("soilhum7")]
    public int? SoilHumiditySensor7 { get; init; }

    [JsonPropertyName("soilhum8")]
    public int? SoilHumiditySensor8 { get; init; }

    [JsonPropertyName("soilhum9")]
    public int? SoilHumiditySensor9 { get; init; }

    [JsonPropertyName("soilhum10")]
    public int? SoilHumiditySensor10 { get; init; }

    [JsonPropertyName("batt1")]
    public int? BatteryLowIndicator1 { get; init; }

    [JsonPropertyName("batt2")]
    public int? BatteryLowIndicator2 { get; init; }

    [JsonPropertyName("batt3")]
    public int? BatteryLowIndicator3 { get; init; }

    [JsonPropertyName("batt4")]
    public int? BatteryLowIndicator4 { get; init; }

    [JsonPropertyName("batt5")]
    public int? BatteryLowIndicator5 { get; init; }

    [JsonPropertyName("batt6")]
    public int? BatteryLowIndicator6 { get; init; }

    [JsonPropertyName("batt7")]
    public int? BatteryLowIndicator7 { get; init; }

    [JsonPropertyName("batt8")]
    public int? BatteryLowIndicator8 { get; init; }

    [JsonPropertyName("batt9")]
    public int? BatteryLowIndicator9 { get; init; }

    [JsonPropertyName("batt10")]
    public int? BatteryLowIndicator10 { get; init; }

    [JsonPropertyName("relay1")]
    public int? Relay1 { get; init; }

    [JsonPropertyName("relay2")]
    public int? Relay2 { get; init; }

    [JsonPropertyName("relay3")]
    public int? Relay3 { get; init; }

    [JsonPropertyName("relay4")]
    public int? Relay4 { get; init; }

    [JsonPropertyName("relay5")]
    public int? Relay5 { get; init; }

    [JsonPropertyName("relay6")]
    public int? Relay6 { get; init; }

    [JsonPropertyName("relay7")]
    public int? Relay7 { get; init; }

    [JsonPropertyName("relay8")]
    public int? Relay8 { get; init; }

    [JsonPropertyName("relay9")]
    public int? Relay9 { get; init; }

    [JsonPropertyName("relay10")]
    public int? Relay10 { get; init; }

    [JsonPropertyName("tz")]
    public string? IANATimeZone { get; init; }

    [JsonPropertyName("feelsLike1")]
    public double? FeelsLikeTemperatureFahrenheit1 { get; init; }

    [JsonPropertyName("feelsLike2")]
    public double? FeelsLikeTemperatureFahrenheit2 { get; init; }

    [JsonPropertyName("feelsLike3")]
    public double? FeelsLikeTemperatureFahrenheit3 { get; init; }

    [JsonPropertyName("feelsLike4")]
    public double? FeelsLikeTemperatureFahrenheit4 { get; init; }

    [JsonPropertyName("feelsLike5")]
    public double? FeelsLikeTemperatureFahrenheit5 { get; init; }

    [JsonPropertyName("feelsLike6")]
    public double? FeelsLikeTemperatureFahrenheit6 { get; init; }

    [JsonPropertyName("feelsLike7")]
    public double? FeelsLikeTemperatureFahrenheit7 { get; init; }

    [JsonPropertyName("feelsLike8")]
    public double? FeelsLikeTemperatureFahrenheit8 { get; init; }

    [JsonPropertyName("feelsLike9")]
    public double? FeelsLikeTemperatureFahrenheit9 { get; init; }

    [JsonPropertyName("feelsLike10")]
    public double? FeelsLikeTemperatureFahrenheit10 { get; init; }

    [JsonPropertyName("dewPoint1")]
    public double? DewPointFahrenheit1 { get; init; }

    [JsonPropertyName("dewPoint2")]
    public double? DewPointFahrenheit2 { get; init; }

    [JsonPropertyName("dewPoint3")]
    public double? DewPointFahrenheit3 { get; init; }

    [JsonPropertyName("dewPoint4")]
    public double? DewPointFahrenheit4 { get; init; }

    [JsonPropertyName("dewPoint5")]
    public double? DewPointFahrenheit5 { get; init; }

    [JsonPropertyName("dewPoint6")]
    public double? DewPointFahrenheit6 { get; init; }

    [JsonPropertyName("dewPoint7")]
    public double? DewPointFahrenheit7 { get; init; }

    [JsonPropertyName("dewPoint8")]
    public double? DewPointFahrenheit8 { get; init; }

    [JsonPropertyName("dewPoint9")]
    public double? DewPointFahrenheit9 { get; init; }

    [JsonPropertyName("dewPoint10")]
    public double? DewPointFahrenheit10 { get; init; }

    [JsonPropertyName("lightning_day")]
    public int? LightningStrikesPerDay { get; init; }

    [JsonPropertyName("lightning_hour")]
    public int? LightningStrikesPerHours { get; init; }

    [JsonPropertyName("lightning_time")]
    public DateTimeOffset? LastLightningStrikeTime { get; init; }

    [JsonPropertyName("lightning_distance")]
    public double? LightningStrikeDistance { get; init; }

    [JsonPropertyName("macAddress")]
    public string? MacAddress { get; init; }
}
