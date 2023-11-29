using DoctorAi.Infrastructure._02.UserService;
using DoctorAi.Infrastructure._03.SmsService;
using IraniValidator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DoctorAi.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISmsService _smsService;

        public AuthUsersController(IUserService userService,ISmsService smsService )
        {
            _userService = userService;
            _smsService = smsService;
        }
        [HttpPost]
        public async Task<IActionResult> Verify( )
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCode( [Required] string Phone_number )
        {
            if (!Phone_number.IsValidMobile(oprator: OpratorType.AllOpprator)) return BadRequest("Invalid phone number");
            if (!Request.QueryString.HasValue || Phone_number.Length < 10) return BadRequest("phone number field is required!");
            var Sendcode = await _smsService.SendVerifycode(Phone_number);
            if (Sendcode.IsSucess)
            {
               
                return Ok($"Send Code to phone number :{Phone_number}");
            }
            return BadRequest($"Try Agin later");
        }
    }
}
