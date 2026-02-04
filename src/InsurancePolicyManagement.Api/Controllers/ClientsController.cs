using InsurancePolicyManagement.Application.DTOs;
using InsurancePolicyManagement.Application.Interfaces;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/clients")]
[Authorize(Roles = "ADMIN")]
public class ClientsController : ControllerBase
{
  private readonly IClientService _service;

    public ClientsController(IClientService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientRequest dto)
        => Ok(await _service.CreateClientAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateClientRequest dto)
    {
        await _service.UpdateClientAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}