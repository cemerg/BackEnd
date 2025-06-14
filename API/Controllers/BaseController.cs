using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(400)]

public class BaseController : ControllerBase
{
}