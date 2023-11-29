using DoctorAi.Domain._01.Entities._02.Verifycode;
using Kavenegar;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;


namespace DoctorAi.Infrastructure._03.SmsService;
public interface ISmsService
{
        Task<ResultSmsDto> SendVerifycode( string phonnumber );
        Task<ResultSmsDto> SendLookup( string phonnumber );
        Task Send_sms( string phon_number, string Message );
}
public class SmsService : ISmsService
{
    private readonly IOptions<KavenegarConfig> _kavenegarConfig;
    private readonly IVerifyCodeRepository _verifyCodeRepository;

    public SmsService(IOptions<KavenegarConfig> kavenegarConfig ,IVerifyCodeRepository verifyCodeRepository)
    {
        _kavenegarConfig = kavenegarConfig;
        _verifyCodeRepository = verifyCodeRepository;
    }

    public async Task<ResultSmsDto> SendLookup( string phonnumber )
    {
        try
        {
            KavenegarApi api = new KavenegarApi(_kavenegarConfig.Value.APIKey);
            var templetneme = "DoctorAi";
            string code = new Random((int)DateTime.Now.Ticks).Next(10000, 99999).ToString();
            var result =await  api.Send(phonnumber, code, templetneme);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ResetColor();
            //return Tuple.Create(true, code);
            return new ResultSmsDto() { IsSucess = true,Message="Send code Successful" };

        }
        catch (Kavenegar.Core.Exceptions.ApiException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Kavenegar.Core.Exceptions.HttpException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ResultSmsDto> SendVerifycode( string phonnumber )
    {
        // var verifyApiAddress = "https://api.kavenegar.com/v1/6545724758465579756F3758713548752F6B326C727531746B6C59302F583858785373726D5133596734593D/verify/lookup.json";
        var verifyApiAddress = "https://api.kavenegar.com/v1/" + _kavenegarConfig.Value.APIKey + "/verify/lookup.json";
        var queryStrings = new List<string>
            {
                "receptor=" + phonnumber,
                "template=" + "DoctorAi"
            };
        string code = new Random((int)DateTime.Now.Ticks).Next(10000, 99999).ToString();
        queryStrings.Add($"token={WebUtility.UrlEncode(code)}");

        verifyApiAddress += "?" + string.Join("&", queryStrings);
        using (var client = new HttpClient())
        {
            try
            {
                var result = JsonConvert.DeserializeObject<dynamic>(await client.GetStringAsync(verifyApiAddress));

                var verifycode = new VerifyCode()
                {
                    PhoneNumber = phonnumber,
                    code = code
                };
                var resultadd = await _verifyCodeRepository.add_or_update_verifycode(verifycode);
                if (resultadd.IsSucces)
                {
                    return new ResultSmsDto()
                    {
                        IsSucess = true,
                        Message = "Send Code Successfully"
                    };
                }
                return  new ResultSmsDto()
                {
                    IsSucess = false,
                    Message = "code dos not send "
                };
            }
            catch (Exception e)
            {
                return new ResultSmsDto()
                {
                    IsSucess = false,
                    Message = $"Error is :{e.Message} "
                };
            }
        }
    }


    public async Task Send_sms( string phon_number, string Message )
    {
        try
        {
            KavenegarApi api = new KavenegarApi(_kavenegarConfig.Value.APIKey);
            var result = await api.Send("1000500055500", phon_number, Message);

        }
        catch (Kavenegar.Core.Exceptions.ApiException ex)
        {

            Console.Write("Message : " + ex.Message);
            throw new Exception(ex.StackTrace);
        }
        catch (Kavenegar.Core.Exceptions.HttpException ex)
        {

            Console.Write("Message : " + ex.Message);
            throw new Exception(ex.StackTrace);
        }
    }

}





public class VerifycodeDto
{
    public string phone_number { get; set; }
    public string code { get; set; }
}

