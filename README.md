# AutoHome

Open source home automation! AutoHome is a web application that runs on a Raspberry Pi and controls local devices on your network.


## Development

Create database migrations

```cmd
cd src\AutoHome.Data
dotnet ef migrations add MIGRATION_NAME
```


### Update tools

```cmd
dotnet tool update --global dotnet-ef
```


## Devices


### Automatic Curtains


#### Components

- Raspberry Pi Zero W
- Stepper Motor
- L7805 Voltage Regulator
- A4988 Motor Driver
- 100uF Electrolytic Capacitor
- 5.5mm/2.1mm Female Barrel Jack Connector


#### Links

A4988 [Datasheet](https://www.allegromicro.com/-/media/files/datasheets/a4988-datasheet.pdf)

https://docs.microsoft.com/en-us/dotnet/iot/deployment

https://www.makerguides.com/a4988-stepper-motor-driver-arduino-tutorial/
