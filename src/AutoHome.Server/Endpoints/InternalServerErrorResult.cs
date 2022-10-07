using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Server.Endpoints;

public class InternalServerErrorResult : StatusCodeResult
{
    private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

    public InternalServerErrorResult() : base(DefaultStatusCode)
    {
    }
}
