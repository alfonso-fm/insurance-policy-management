using InsurancePolicyManagement.Application.DTOs;
using InsurancePolicyManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/policies")]
[Authorize]
public class PoliciesController : ControllerBase
{
  private readonly IPolicyService _service;
  public PoliciesController(IPolicyService service)
  {
      _service = service;
  }

  [HttpGet("my")]
  [Authorize(Roles = "CLIENT")]
  public async Task<IActionResult> GetMyPolicies()
  {
    var clientId = User.FindFirst("clientId")?.Value;
    if (string.IsNullOrEmpty(clientId))
      return Forbid();

    return Ok(await _service.GetAllByClientIdAsync(Guid.Parse(clientId)));
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(Guid Id)
      => Ok(await _service.GetByIdAsync(Id));

  [HttpGet]
  public async Task<IActionResult> GetAll()
    => Ok(await _service.GetAllAsync());

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreatePolicyDto dto)
  {
    Console.Write("Llego al Controller");
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    return Ok(await _service.CreatePolicyAsync(dto));
  }
    

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(Guid id, UpdatePolicyDto dto)
  {
    await _service.UpdatePolicyAsync(id, dto);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    await _service.DeletePolicyAsync(id);
    return NoContent();
  }
}
