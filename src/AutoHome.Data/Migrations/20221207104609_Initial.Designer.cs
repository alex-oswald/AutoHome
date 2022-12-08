﻿// <auto-generated />
using System;
using AutoHome.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoHome.Data.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    [Migration("20221207104609_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AutoHome.Core.Entities.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IntegrationDeviceId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("AutoHome.Core.Entities.Trigger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Interval")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Triggers");
                });

            modelBuilder.Entity("AutoHome.Core.Entities.TriggerEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Event")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TriggerId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("TriggerEvents");
                });

            modelBuilder.Entity("AutoHome.Core.Entities.Variable", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Key")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsSecret")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id", "Key");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("AutoHome.Core.Entities.WeatherReading", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double?>("AbsoluteBarometricPressure")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "baromabsin");

                    b.Property<int?>("BatteryLowIndicator")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "battout");

                    b.Property<int?>("BatteryLowIndicator1")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt1");

                    b.Property<int?>("BatteryLowIndicator10")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt10");

                    b.Property<int?>("BatteryLowIndicator2")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt2");

                    b.Property<int?>("BatteryLowIndicator3")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt3");

                    b.Property<int?>("BatteryLowIndicator4")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt4");

                    b.Property<int?>("BatteryLowIndicator5")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt5");

                    b.Property<int?>("BatteryLowIndicator6")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt6");

                    b.Property<int?>("BatteryLowIndicator7")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt7");

                    b.Property<int?>("BatteryLowIndicator8")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt8");

                    b.Property<int?>("BatteryLowIndicator9")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt9");

                    b.Property<double?>("CO2PartsPerMillion")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "co2");

                    b.Property<int>("CO2SensorBatteryLowIndicator")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt_co2");

                    b.Property<double?>("DailyRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dailyrainin");

                    b.Property<double?>("DewPointFahrenheit")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint");

                    b.Property<double?>("DewPointFahrenheit1")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint1");

                    b.Property<double?>("DewPointFahrenheit10")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint10");

                    b.Property<double?>("DewPointFahrenheit2")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint2");

                    b.Property<double?>("DewPointFahrenheit3")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint3");

                    b.Property<double?>("DewPointFahrenheit4")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint4");

                    b.Property<double?>("DewPointFahrenheit5")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint5");

                    b.Property<double?>("DewPointFahrenheit6")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint6");

                    b.Property<double?>("DewPointFahrenheit7")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint7");

                    b.Property<double?>("DewPointFahrenheit8")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint8");

                    b.Property<double?>("DewPointFahrenheit9")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPoint9");

                    b.Property<long?>("EpochMilliseconds")
                        .HasColumnType("bigint")
                        .HasAnnotation("Relational:JsonPropertyName", "dateutc");

                    b.Property<double?>("EventRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "eventrainin");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit1")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike1");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit10")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike10");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit2")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike2");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit3")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike3");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit4")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike4");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit5")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike5");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit6")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike6");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit7")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike7");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit8")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike8");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit9")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike9");

                    b.Property<double?>("HourlyRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "hourlyrainin");

                    b.Property<int?>("HumiditySensor1")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity1");

                    b.Property<int?>("HumiditySensor10")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity10");

                    b.Property<int?>("HumiditySensor2")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity2");

                    b.Property<int?>("HumiditySensor3")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity3");

                    b.Property<int?>("HumiditySensor4")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity4");

                    b.Property<int?>("HumiditySensor5")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity5");

                    b.Property<int?>("HumiditySensor6")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity6");

                    b.Property<int?>("HumiditySensor7")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity7");

                    b.Property<int?>("HumiditySensor8")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity8");

                    b.Property<int?>("HumiditySensor9")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity9");

                    b.Property<string>("IANATimeZone")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "tz");

                    b.Property<double?>("IndoorDewPointTemperatureFahrenheit")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "dewPointin");

                    b.Property<double?>("IndoorFeelsLikeTemperatureFahrenheit")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLikein");

                    b.Property<int?>("IndoorHumidity")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidityin");

                    b.Property<double?>("IndoorTemperatureFahrenheit")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "tempinf");

                    b.Property<DateTimeOffset?>("LastLightningStrikeTime")
                        .HasColumnType("datetime(6)")
                        .HasAnnotation("Relational:JsonPropertyName", "lightning_time");

                    b.Property<DateTimeOffset>("LastRain")
                        .HasColumnType("datetime(6)")
                        .HasAnnotation("Relational:JsonPropertyName", "lastRain");

                    b.Property<double?>("LightningStrikeDistance")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "lightning_distance");

                    b.Property<int?>("LightningStrikesPerDay")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "lightning_day");

                    b.Property<int?>("LightningStrikesPerHours")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "lightning_hour");

                    b.Property<string>("Loc")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "loc");

                    b.Property<string>("MacAddress")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "macAddress");

                    b.Property<double?>("MaxDailyGust")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "maxdailygust");

                    b.Property<double?>("MonthlyRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "monthlyrainin");

                    b.Property<double?>("OutdoorFeelsLikeTemperatureFahrenheit")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "feelsLike");

                    b.Property<int?>("OutdoorHumidity")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity");

                    b.Property<double?>("OutdoorTemperatureFahrenheit")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "tempf");

                    b.Property<int?>("PM25AirQualityBatteryLowIndicator")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "batt_25");

                    b.Property<double?>("PM25IndoorAirQuality")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "pm25_in");

                    b.Property<double?>("PM25IndoorAirQuality24HourAverage")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "pm25_in_24h");

                    b.Property<double?>("PM25OutdoorAirQuality")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "pm25");

                    b.Property<double?>("PM25OutdoorAirQuality24HourAverage")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "pm25_24h");

                    b.Property<double?>("Previous24HourRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "24hourrainin");

                    b.Property<double?>("RelativeBarometricPressure")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "baromrelin");

                    b.Property<int?>("Relay1")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay1");

                    b.Property<int?>("Relay10")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay10");

                    b.Property<int?>("Relay2")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay2");

                    b.Property<int?>("Relay3")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay3");

                    b.Property<int?>("Relay4")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay4");

                    b.Property<int?>("Relay5")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay5");

                    b.Property<int?>("Relay6")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay6");

                    b.Property<int?>("Relay7")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay7");

                    b.Property<int?>("Relay8")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay8");

                    b.Property<int?>("Relay9")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "relay9");

                    b.Property<int?>("SoilHumiditySensor1")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum1");

                    b.Property<int?>("SoilHumiditySensor10")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum10");

                    b.Property<int?>("SoilHumiditySensor2")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum2");

                    b.Property<int?>("SoilHumiditySensor3")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum3");

                    b.Property<int?>("SoilHumiditySensor4")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum4");

                    b.Property<int?>("SoilHumiditySensor5")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum5");

                    b.Property<int?>("SoilHumiditySensor6")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum6");

                    b.Property<int?>("SoilHumiditySensor7")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum7");

                    b.Property<int?>("SoilHumiditySensor8")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum8");

                    b.Property<int?>("SoilHumiditySensor9")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "soilhum9");

                    b.Property<double?>("SoilTemperatureSensor1")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp1f");

                    b.Property<double?>("SoilTemperatureSensor10")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp10f");

                    b.Property<double?>("SoilTemperatureSensor2")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp2f");

                    b.Property<double?>("SoilTemperatureSensor3")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp3f");

                    b.Property<double?>("SoilTemperatureSensor4")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp4f");

                    b.Property<double?>("SoilTemperatureSensor5")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp5f");

                    b.Property<double?>("SoilTemperatureSensor6")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp6f");

                    b.Property<double?>("SoilTemperatureSensor7")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp7f");

                    b.Property<double?>("SoilTemperatureSensor8")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp8f");

                    b.Property<double?>("SoilTemperatureSensor9")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "soiltemp9f");

                    b.Property<double?>("SolarRadiation")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "solarradiation");

                    b.Property<double?>("TemperatureSensor1")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp1f");

                    b.Property<double?>("TemperatureSensor10")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp10f");

                    b.Property<double?>("TemperatureSensor2")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp2f");

                    b.Property<double?>("TemperatureSensor3")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp3f");

                    b.Property<double?>("TemperatureSensor4")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp4f");

                    b.Property<double?>("TemperatureSensor5")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp5f");

                    b.Property<double?>("TemperatureSensor6")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp6f");

                    b.Property<double?>("TemperatureSensor7")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp7f");

                    b.Property<double?>("TemperatureSensor8")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp8f");

                    b.Property<double?>("TemperatureSensor9")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "temp9f");

                    b.Property<double?>("TotalRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "totalrainin");

                    b.Property<int?>("UltravioletRadiationIndex")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "uv");

                    b.Property<DateTimeOffset?>("UtcDate")
                        .HasColumnType("datetime(6)")
                        .HasAnnotation("Relational:JsonPropertyName", "date");

                    b.Property<double?>("WeeklyRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "weeklyrainin");

                    b.Property<int?>("WindDirection")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "winddir");

                    b.Property<int?>("WindDirection10MinuteAverage")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "winddir_avg10m");

                    b.Property<int?>("WindDirection2MinuteAverage")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "winddir_avg2m");

                    b.Property<int?>("WindGustDir")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "windgustdir");

                    b.Property<double?>("WindGustMph")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "windgustmph");

                    b.Property<double?>("WindSpeedMph")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "windspeedmph");

                    b.Property<double?>("WindSpeedMph10MinuteAverage")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "windspdmph_avg10m");

                    b.Property<double?>("WindSpeedMph2MinuteAverage")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "windspdmph_avg2m");

                    b.Property<double?>("YearlyRainfall")
                        .HasColumnType("double")
                        .HasAnnotation("Relational:JsonPropertyName", "yearlyrainin");

                    b.HasKey("Id");

                    b.ToTable("WeatherReadings");
                });

            modelBuilder.Entity("AutoHome.Core.Entities.Trigger", b =>
                {
                    b.HasOne("AutoHome.Core.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("AutoHome.Core.Entities.TriggerEvent", b =>
                {
                    b.HasOne("AutoHome.Core.Entities.Trigger", "Trigger")
                        .WithMany()
                        .HasForeignKey("TriggerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });
#pragma warning restore 612, 618
        }
    }
}