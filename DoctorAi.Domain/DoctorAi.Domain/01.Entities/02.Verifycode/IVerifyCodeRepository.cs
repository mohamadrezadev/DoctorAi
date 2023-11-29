using DoctorAi.Application;
using DoctorAi.Domain._02.Common;
using Microsoft.EntityFrameworkCore;

namespace DoctorAi.Domain._01.Entities._02.Verifycode
{
    public interface IVerifyCodeRepository :IRepository<VerifyCode>
    {
        Task<Processingresult> add_or_update_verifycode( VerifyCode verifycode );
        Task<Processingresult> Isvalidcode( string phone_number, string code );

    }
}
