using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/policies")]
[Authorize]
public class PoliciesController : ControllerBase
{
  private readonly InsuranceDBContext _context;

  public PoliciesController(InsuranceDBContext context)
  {
    _context = context;
  }

  [HttpGet("my")]
  [Authorize(Roles = "CLIENT")]
  public async Task<IActionResult> GetMyPolicies()
  {
    var clientId = User.FindFirst("clientId")?.Value;
    if (string.IsNullOrEmpty(clientId))
      return Forbid();

    return Ok(await _context.Policies
      .Where(p => p.ClientId == Guid.Parse(clientId))
      .ToListAsync());
  }
}
