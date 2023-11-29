using DoctorAi.Application;
using DoctorAi.Domain._01.Entities._02.Verifycode;
using DoctorAi.Persistance._01.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Persistance._03.VerifyCodes
{
    public class VerifyCodeRepository : Repository<VerifyCode>, IVerifyCodeRepository
    {
        public VerifyCodeRepository( DoctorAiDbContext dbContext ) : base(dbContext)
        {
        }
        public override async Task AddAsync( VerifyCode entity, CancellationToken cancellationToken, bool saveNow = true )
        {


            var isexist = DbContext.VerifyCodes.Any(p => p.PhoneNumber== entity.PhoneNumber);
            if (isexist)
            {
                var updateverifycode = DbContext.VerifyCodes.FirstOrDefault(p => p.PhoneNumber == entity.PhoneNumber);
                updateverifycode.code = entity.code;
                entity.SendCodeTime= entity.SendCodeTime;
                DbContext.VerifyCodes.Update(updateverifycode);
                await DbContext.SaveChangesAsync();
               
            }
            await DbContext.VerifyCodes.AddAsync(entity);
            await DbContext.SaveChangesAsync();

        }

       
        public async Task<Processingresult> Isvalidcode( string phone_number, string code )
        {
            var findCode =await DbContext.VerifyCodes
                .FirstOrDefaultAsync(p => p.PhoneNumber.Equals(phone_number) && p.code.Equals(code));
            if (findCode is not null)
            {
                return new()
                {
                    IsSucces = true,
                    Message = "Find code"
                };
            }
            return new()
            {
                IsSucces = false,
                Message = "Not Fond code and phone number "
            };
        }

       public async Task<Processingresult> add_or_update_verifycode( VerifyCode verifycode )
        {
            var isexist = DbContext.VerifyCodes.FirstOrDefaultAsync(p => p.PhoneNumber == verifycode.PhoneNumber);
            if (isexist is not null)
            {
                var updateverifycode = DbContext.VerifyCodes.FirstOrDefault(p => p.PhoneNumber == verifycode.PhoneNumber);
                updateverifycode.code = verifycode.code;
                verifycode.SendCodeTime = verifycode.SendCodeTime;
                await DbContext.SaveChangesAsync();
                return new Processingresult()
                {
                    IsSucces = true,
                    Message = "Saved Code in db"

                };
            }
            await DbContext.VerifyCodes.AddAsync(verifycode);
            await DbContext.SaveChangesAsync();
            return new Processingresult()
            {
                IsSucces = false,
                Message = "Error for saving verify code "

            };
        }
    }
}
