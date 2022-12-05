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
    [Migration("20221102082624_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
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
                        .HasColumnType("double");

                    b.Property<int?>("BatteryLowIndicator")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator1")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator10")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator2")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator3")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator4")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator5")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator6")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator7")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator8")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryLowIndicator9")
                        .HasColumnType("int");

                    b.Property<double?>("CO2PartsPerMillion")
                        .HasColumnType("double");

                    b.Property<int>("CO2SensorBatteryLowIndicator")
                        .HasColumnType("int");

                    b.Property<double?>("DailyRainfall")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit1")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit10")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit2")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit3")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit4")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit5")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit6")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit7")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit8")
                        .HasColumnType("double");

                    b.Property<double?>("DewPointFahrenheit9")
                        .HasColumnType("double");

                    b.Property<long?>("EpochMilliseconds")
                        .HasColumnType("bigint");

                    b.Property<double?>("EventRainfall")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit1")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit10")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit2")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit3")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit4")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit5")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit6")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit7")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit8")
                        .HasColumnType("double");

                    b.Property<double?>("FeelsLikeTemperatureFahrenheit9")
                        .HasColumnType("double");

                    b.Property<double?>("HourlyRainfall")
                        .HasColumnType("double");

                    b.Property<int?>("HumiditySensor1")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor10")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor2")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor3")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor4")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor5")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor6")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor7")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor8")
                        .HasColumnType("int");

                    b.Property<int?>("HumiditySensor9")
                        .HasColumnType("int");

                    b.Property<string>("IANATimeZone")
                        .HasColumnType("longtext");

                    b.Property<double?>("IndoorDewPointTemperatureFahrenheit")
                        .HasColumnType("double");

                    b.Property<double?>("IndoorFeelsLikeTemperatureFahrenheit")
                        .HasColumnType("double");

                    b.Property<int?>("IndoorHumidity")
                        .HasColumnType("int");

                    b.Property<double?>("IndoorTemperatureFahrenheit")
                        .HasColumnType("double");

                    b.Property<DateTimeOffset?>("LastLightningStrikeTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("LastRain")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("LightningStrikeDistance")
                        .HasColumnType("double");

                    b.Property<int?>("LightningStrikesPerDay")
                        .HasColumnType("int");

                    b.Property<int?>("LightningStrikesPerHours")
                        .HasColumnType("int");

                    b.Property<string>("Loc")
                        .HasColumnType("longtext");

                    b.Property<string>("MacAddress")
                        .HasColumnType("longtext");

                    b.Property<double?>("MaxDailyGust")
                        .HasColumnType("double");

                    b.Property<double?>("MonthlyRainfall")
                        .HasColumnType("double");

                    b.Property<double?>("OutdoorFeelsLikeTemperatureFahrenheit")
                        .HasColumnType("double");

                    b.Property<int?>("OutdoorHumidity")
                        .HasColumnType("int");

                    b.Property<double?>("OutdoorTemperatureFahrenheit")
                        .HasColumnType("double");

                    b.Property<int?>("PM25AirQualityBatteryLowIndicator")
                        .HasColumnType("int");

                    b.Property<double?>("PM25IndoorAirQuality")
                        .HasColumnType("double");

                    b.Property<double?>("PM25IndoorAirQuality24HourAverage")
                        .HasColumnType("double");

                    b.Property<double?>("PM25OutdoorAirQuality")
                        .HasColumnType("double");

                    b.Property<double?>("PM25OutdoorAirQuality24HourAverage")
                        .HasColumnType("double");

                    b.Property<double?>("Previous24HourRainfall")
                        .HasColumnType("double");

                    b.Property<double?>("RelativeBarometricPressure")
                        .HasColumnType("double");

                    b.Property<int?>("Relay1")
                        .HasColumnType("int");

                    b.Property<int?>("Relay10")
                        .HasColumnType("int");

                    b.Property<int?>("Relay2")
                        .HasColumnType("int");

                    b.Property<int?>("Relay3")
                        .HasColumnType("int");

                    b.Property<int?>("Relay4")
                        .HasColumnType("int");

                    b.Property<int?>("Relay5")
                        .HasColumnType("int");

                    b.Property<int?>("Relay6")
                        .HasColumnType("int");

                    b.Property<int?>("Relay7")
                        .HasColumnType("int");

                    b.Property<int?>("Relay8")
                        .HasColumnType("int");

                    b.Property<int?>("Relay9")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor1")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor10")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor2")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor3")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor4")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor5")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor6")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor7")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor8")
                        .HasColumnType("int");

                    b.Property<int?>("SoilHumiditySensor9")
                        .HasColumnType("int");

                    b.Property<double?>("SoilTemperatureSensor1")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor10")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor2")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor3")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor4")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor5")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor6")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor7")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor8")
                        .HasColumnType("double");

                    b.Property<double?>("SoilTemperatureSensor9")
                        .HasColumnType("double");

                    b.Property<double?>("SolarRadiation")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor1")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor10")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor2")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor3")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor4")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor5")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor6")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor7")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor8")
                        .HasColumnType("double");

                    b.Property<double?>("TemperatureSensor9")
                        .HasColumnType("double");

                    b.Property<double?>("TotalRainfall")
                        .HasColumnType("double");

                    b.Property<int?>("UltravioletRadiationIndex")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UtcDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("WeeklyRainfall")
                        .HasColumnType("double");

                    b.Property<int?>("WindDirection")
                        .HasColumnType("int");

                    b.Property<int?>("WindDirection10MinuteAverage")
                        .HasColumnType("int");

                    b.Property<int?>("WindDirection2MinuteAverage")
                        .HasColumnType("int");

                    b.Property<int?>("WindGustDir")
                        .HasColumnType("int");

                    b.Property<double?>("WindGustMph")
                        .HasColumnType("double");

                    b.Property<double?>("WindSpeedMph")
                        .HasColumnType("double");

                    b.Property<double?>("WindSpeedMph10MinuteAverage")
                        .HasColumnType("double");

                    b.Property<double?>("WindSpeedMph2MinuteAverage")
                        .HasColumnType("double");

                    b.Property<double?>("YearlyRainfall")
                        .HasColumnType("double");

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