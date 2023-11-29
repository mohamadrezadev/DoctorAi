using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Domain._01.Entities._02.Verifycode
{
    public class VerifyCode:IEntity
    {
        public VerifyCode()
        {
            this.SendCodeTime = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string code { get; set; }
        public DateTime SendCodeTime { get; set; }
        public string PhoneNumber { get; set; }
    }
}
