namespace DoctorAi.Infrastructure._03.SmsService;

public class ResultSmsDto
{
    public bool IsSucess { get; set; }
    public string Message { get; set; }

}
public class ResultSmsDto<T>
{
    public bool IsSucess { get; set; }
    public string Message { get; set; }
    public T  Value { get; set; }
}

