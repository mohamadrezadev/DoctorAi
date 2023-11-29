using DoctorAi.Application;
using DoctorAi.Domain._02.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace DoctorAi.Domain._01.Entities.Users;

public interface IUserRepository : IRepository<UserApp>
{
    Task<Processingresult<Token>> AddRreshToken( Guid UserId, Token Token ,CancellationToken cancellationToken);
    Task<Processingresult> UpdateRreshToken( Guid UserId, Token token );
    Task<Processingresult> DeleteRreshToken(Guid TokenId );
    Task<Processingresult<Token>> GetToken( Guid userId );                
}
