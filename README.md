# AutoCurtains

An IoT solution for automatically opening and closing curtains.

The Raspberry Pi runs a Blazor server app that controls the stepper motor. Visit the web page on your local network to open or close your curtains.

## Components

- Raspberry Pi Zero W
- Stepper Motor
- L7805 Voltage Regulator
- A4988 Motor Driver
- 100uF Electrolytic Capacitor
- 5.5mm/2.1mm Female Barrel Jack Connector

## Wiring Diagram

![bb](./schematics/AutoCurtains_bb.svg)


## Blazor app screenshot

![phone_screenshot](./misc/phone_screenshot.png)

## Secrets

Create `appsettings.Production.json` and copy contents of `secrets.json`.

## Other

A4988 [Datasheet](https://www.allegromicro.com/-/media/files/datasheets/a4988-datasheet.pdf)

https://docs.microsoft.com/en-us/dotnet/iot/deployment

https://www.makerguides.com/a4988-stepper-motor-driver-arduino-tutorial/