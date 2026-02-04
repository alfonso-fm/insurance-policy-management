using System.Text.RegularExpressions;
using InsurancePolicyManagement.Domain.Exceptions;

namespace InsurancePolicyManagement.Domain.Entities;
public class Client
{
  public Guid Id { get; set; }
  public string NumericId { get; set; } = "";
  public string Name { get; set; } = "";  
  public string Email { get; set; } = "";
  public string Phone { get; set; } = "";
  public string? Address { get; set; } = "-";
  public Client() { } // EF

  public Client(string _numericId, string _name, string _email, string _phone)
  {
    ValidateNumericId(_numericId);
    ValidateName(_name);
    ValidateEmail(_email);

    Id = Guid.NewGuid();
    NumericId = _numericId;
    Name = _name;
    Email = _email;
    Phone = _phone;
  }

  private void ValidateEmail(string _email)
  {
    if (!Regex.IsMatch(_email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
    throw new DomainException("Invalid email format.");
  }

  private void ValidateName(string _name)
  {
    if (!Regex.IsMatch(_name, @"^[a-zA-Z\s]+$"))
      throw new DomainException("Full name cannot contain numbers or special characters.");
  }

  private void ValidateNumericId(string _numericId)
  {
    if (!Regex.IsMatch(_numericId, @"^\d{10}$"))
      throw new DomainException("Identification number must be exactly 10 digits.");
  }

  public void UpdateContactInfo(string _phone, string _address)
  {
      Phone = _phone;
      Address = _address;
  }
}