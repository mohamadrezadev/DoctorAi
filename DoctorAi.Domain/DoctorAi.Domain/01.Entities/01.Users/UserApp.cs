using Microsoft.AspNetCore.Identity;
using System.Globalization;
namespace DoctorAi.Domain._01.Entities.Users;

public class UserApp : IdentityUser<Guid>, IEntity
{
    public UserApp( )
    {

    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid TokenId { get; set; }
    public Token Token { get; set; }

}
