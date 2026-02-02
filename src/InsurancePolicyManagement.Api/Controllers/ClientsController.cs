using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/clients")]
[Authorize(Roles = "ADMIN")]
public class ClientsController : ControllerBase
{
  private readonly InsuranceDBContext _context;

  public ClientsController(InsuranceDBContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<IActionResult> Get()
    => Ok(await _context.Clients.ToListAsync());
}