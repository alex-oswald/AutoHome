using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Server.Endpoints;

public class ProducesJsonAttribute : ProducesAttribute
{
    public ProducesJsonAttribute() : base("application/json")
    {
    }
}
