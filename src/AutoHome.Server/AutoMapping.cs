using AutoHome.Server.Endpoints.Devices;
using AutoHome.Server.Endpoints.TriggerEvents;
using AutoHome.Server.Endpoints.Triggers;
using AutoHome.Server.Endpoints.Variables;
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

		CreateMap<TriggerEvent, ListTriggerEventsResult>();

		CreateMap<AddTriggerRequest, Trigger>();
		CreateMap<UpdateTriggerRequest, Trigger>();
		CreateMap<Trigger, AddTriggerResult>();
		CreateMap<Trigger, GetTriggerResult>();
		CreateMap<Trigger, ListTriggersResult>();
		CreateMap<Trigger, UpdateTriggerRequest>();
		CreateMap<Trigger, UpdateTriggerResult>();

		CreateMap<AddVariableRequest, Variable>();
		CreateMap<UpdateVariableRequest, Variable>();
		CreateMap<Variable, AddVariableResult>();
		CreateMap<Variable, GetVariableResult>();
		CreateMap<Variable, ListVariablesResult>();
		CreateMap<Variable, UpdateVariableRequest>();
		CreateMap<Variable, UpdateVariableResult>();

		CreateMap<Cirrus.Models.Device, WeatherReading>();
	}
}
