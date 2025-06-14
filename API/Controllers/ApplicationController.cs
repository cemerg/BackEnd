using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/application")]
public class ApplicationController(IApplicationService applicationService) : BaseController
{

    [HttpGet]
    [ProducesResponseType(typeof(ApplicationConfigurationDto), 200)]
    public async Task<IActionResult> GetApplicationConfiguration()
    {
        var response = await applicationService.GetApplicationConfiguration();
        return Ok(response);
    }

}
