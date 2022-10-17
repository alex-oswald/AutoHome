using AutoHome.Api;
using AutoHome.Client.Components.Devices;
using AutoHome.Client.Components.Triggers;
using AutoMapper;

namespace AutoHome.Client;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		CreateMap<AddDeviceForm, AddDeviceRequest>();
        CreateMap<EditDeviceForm, UpdateDeviceRequest>();
		CreateMap<ListDevicesResult, EditDeviceForm>();
		CreateMap<ListDevicesResult, Device>();

		CreateMap<AddTriggerForm, AddTriggerRequest>();
		CreateMap<EditTriggerForm, UpdateTriggerRequest>();
		CreateMap<ListTriggersResult, EditTriggerForm>();
	}
}
