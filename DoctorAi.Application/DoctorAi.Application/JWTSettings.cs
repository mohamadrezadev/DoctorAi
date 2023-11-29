using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Application
{
   public class JWTSettings
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public string RefreshTokenValidityInDays { get; set; }
        public string TokenValidityInMinutes { get; set; }
        public int Expires { get; set; } = 15;
    }
}
