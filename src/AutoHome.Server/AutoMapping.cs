using AutoHome.Server.Endpoints.Devices;
using AutoHome.Server.Endpoints.TimeTriggers;
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

		CreateMap<AddTimeTriggerRequest, TimeTrigger>();
		CreateMap<UpdateTimeTriggerRequest, TimeTrigger>();
		CreateMap<TimeTrigger, AddTimeTriggerResult>();
		CreateMap<TimeTrigger, GetTimeTriggerResult>();
		CreateMap<TimeTrigger, ListTimeTriggersResult>();
		CreateMap<TimeTrigger, UpdateTimeTriggerRequest>();
		CreateMap<TimeTrigger, UpdateTimeTriggerResult>();
	}
}
