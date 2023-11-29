using DoctorAi.Application;
using DoctorAi.Infrastructure._02.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Infrastructure._02.UserService;
public interface IUserService
    {
        Task<Processingresult> CreateUser( CreateUserDto createUser, CancellationToken cancellationToken );
        Task<Processingresult> UpdateUser( UpdateUserDto updateUser,CancellationToken cancellationToken );
        

    }


public class UserService : IUserService
{
    public Task<Processingresult> CreateUser( CreateUserDto createUser, CancellationToken cancellationToken )
    {
        throw new NotImplementedException();
    }

    public Task<Processingresult> UpdateUser( UpdateUserDto updateUser, CancellationToken cancellationToken )
    {
        throw new NotImplementedException();
    }
}

public class ResultCreateUser
{
    
}
public class JwtDto
{
    public string AccessToken { get; set; }
    public string Refreshtoken { get; set; }
}