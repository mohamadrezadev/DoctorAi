using DoctorAi.Application;
using DoctorAi.Domain._01.Entities.Users;
using DoctorAi.Persistance._01.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Persistance._02.Users;
public class UserRepository : Repository<UserApp>, IUserRepository
{
    public UserRepository( DoctorAiDbContext dbContext ) : base(dbContext)
    {
    }

    public async Task<Processingresult<Token>> AddRreshToken( Guid UserId, Token Token, CancellationToken cancellationToken )
    {
        var finduser = await DbContext.Users.Include(p => p.Token)
            .FirstOrDefaultAsync(p => p.Id.Equals(UserId));
        if (finduser == null)
        {
            finduser.Token = Token;
            await DbContext.Tokens.AddAsync(Token, cancellationToken);
            await DbContext.SaveChangesAsync();
            return new Processingresult<Token>()
            {
                IsSucces = true,
                Message = "add successfully Token",
                Value = Token
            };

        }
        return new Processingresult<Token>()
        {
            IsSucces = false,
            Message = $"Can not find User By Id:{UserId}"
        };

    }

    public async Task<Processingresult> DeleteRreshToken( Guid TokenId )
    {
        var findtoken = await DbContext.Tokens.Include(p => p.AccessToken)
            .FirstOrDefaultAsync(p => p.Id.Equals(TokenId));
        if (findtoken == null)
        {
            return new Processingresult()
            {
                IsSucces = false,
                Message = $"dos not exist Token with Id:{TokenId}"
            };

        }
        return new Processingresult()
        {
            IsSucces = true
        };
    }

    public Task<Processingresult<Token>> GetToken( Guid userId )
    {
        throw new NotImplementedException();
    }

    public Task<Processingresult> UpdateRreshToken( Guid UserId, Token token )
    {
        throw new NotImplementedException();
    }
}

