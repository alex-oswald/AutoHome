﻿using AutoHome.Client.Components;
using AutoHome.Data.EndpointObjects.Devices;
using AutoHome.PluginCore;
using AutoMapper;

namespace AutoHome.Server;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		CreateMap<AddEditDeviceForm, AddDeviceRequest>();
		CreateMap<ListDevicesResult, AddEditDeviceForm>();
		CreateMap<AddEditDeviceForm, UpdateDeviceRequest>();

		CreateMap<ListDevicesResult, Device>();
	}
}
