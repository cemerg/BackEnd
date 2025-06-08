using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/application")]
[Produces("application/json")]
public class ApplicationController(IApplicationService applicationService) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(ApplicationConfigurationDto), 200)]
    public async Task<IActionResult> GetApplicationConfiguration()
    {
        var response = await applicationService.GetApplicationConfiguration();
        return Ok(response);
    }

}
