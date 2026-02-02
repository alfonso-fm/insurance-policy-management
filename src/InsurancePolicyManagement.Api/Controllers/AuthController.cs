using InsurancePolicyManagement.Domain.Entities;
using InsurancePolicyManagement.Infrastructure.Persistence.Context;
using InsurancePolicyManagement.Application.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
  private readonly InsuranceDBContext _context;
  private readonly IConfiguration _config;
  public AuthController(InsuranceDBContext context, IConfiguration config)
  {
    _context = context;
    _config = config;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var user = await _context.Users
    .Include(u => u.Role)
    .FirstOrDefaultAsync(u => u.Email == request.Email);

    if (user == null)
    {
      return Unauthorized("Invalid credentials, user not found");
    }

    var hashed = PasswordHasher.Hash(request.Password);
    if (hashed != user.PasswordHash)
    {
      return Unauthorized("Error Invalid credentials");
    }

    var token = GenerateJwt(user);
    return Ok(new { token });
  }

  private string GenerateJwt(User user)
  {
    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.Role, user.Role.Name),
      new Claim("clientId", user.ClientId?.ToString() ?? "")
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: _config["Jwt:Issuer"],
      audience: _config["Jwt:Audience"],
      claims: claims,
      expires: DateTime.UtcNow.AddHours(4),
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  private static string Hash(string password)
  {
    using var sha = System.Security.Cryptography.SHA256.Create();
    return Convert.ToBase64String(
    sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
  }
}

public record LoginRequest(string Email, string Password);
