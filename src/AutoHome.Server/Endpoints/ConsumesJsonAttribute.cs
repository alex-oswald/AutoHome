using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Server.Endpoints;

public class ConsumesJsonAttribute : ConsumesAttribute
{
    public ConsumesJsonAttribute() : base("application/json")
    {
    }
}
