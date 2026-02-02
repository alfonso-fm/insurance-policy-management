using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InsurancePolicyManagement.Api.Controllers
{
  [ApiController]
  [Route("api/health")]
  public class HealthController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("API is running");
    }
  }
}