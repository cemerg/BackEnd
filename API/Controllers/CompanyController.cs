using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApplicationConfigurationDto), 200)]
    public async Task<IActionResult> GetApplicationConfiguration([FromQuery] Guid companyId)
    {
        var response = await _companyService.GetApplicationConfiguration(companyId);
        return Ok(response);
    }
   
}
