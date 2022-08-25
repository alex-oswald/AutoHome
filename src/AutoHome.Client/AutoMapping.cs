using AutoHome.Client.Components;
using AutoHome.Data.EndpointObjects.Devices;
using AutoMapper;

namespace AutoHome.Server;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		CreateMap<AddDeviceForm, AddDeviceRequest>();
	}
}
