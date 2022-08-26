using AutoHome.Data.EndpointObjects.Devices;
using AutoHome.Data.Entities;
using AutoMapper;

namespace AutoHome.Server;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		CreateMap<AddDeviceRequest, Device>();
		CreateMap<UpdateDeviceRequest, Device>();

		CreateMap<Device, AddDeviceResult>();
		CreateMap<Device, GetDeviceResult>();
		CreateMap<Device, ListDevicesResult>();
		CreateMap<Device, UpdateDeviceRequest>();
		CreateMap<Device, UpdateDeviceResult>();
	}
}
