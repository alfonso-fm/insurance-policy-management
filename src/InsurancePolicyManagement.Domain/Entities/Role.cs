using System.Security.Permissions;
using InsurancePolicyManagement.Domain.Exceptions;

namespace InsurancePolicyManagement.Domain.Entities;
public class Role
{
    public int Id { get;  set; }
    public string Name { get; set; } = default!;
    public ICollection<User> Users{get; set; } = new List<User>();

    public Role() { }

    public Role(string name, int id )
    {
        Id = id;
        Name = name;
    }
}