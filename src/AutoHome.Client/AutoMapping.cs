using AutoHome.Api;
using AutoHome.Client.Components;
using AutoHome.PluginCore;
using AutoMapper;

namespace AutoHome.Client;

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
