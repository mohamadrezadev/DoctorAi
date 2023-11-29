namespace DoctorAi.Domain._01.Entities.Users;

public class Token : IEntity<Guid>
{
    public Token()
    {
        this.Expire=  DateTime.UtcNow.AddYears(1);
    }
    public Guid Id { get; set; }
    public RefreshToken RefreshToken { get; set; }
    public string AccessToken { get; set; }
    public DateTime Expire { get; set; }
    
    public UserApp User { get; set; }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= this.Expire;
    }

    public void UpdateExpirationTime( int yearsToAdd )
    {
        Expire = DateTime.UtcNow.AddYears(yearsToAdd);
    }
}
