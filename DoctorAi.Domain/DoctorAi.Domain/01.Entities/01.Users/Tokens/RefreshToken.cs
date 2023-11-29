namespace DoctorAi.Domain._01.Entities.Users
{
    public class RefreshToken : IEntity<Guid>
    {
        public RefreshToken()
        {
            this.CreateTime = DateTime.UtcNow;
            this.ExpireTime = DateTime.UtcNow.AddDays(15);
        }

        public Guid Id { get; set; }
        public DateTime ExpireTime { get; set; }
        public DateTime CreateTime { get; set; }
        public string Content { get; set; }

        public bool IsExpire( )
        {
               return DateTime.Now>=this.ExpireTime;
        }
        public void Update(int add_day)
        {
               this.ExpireTime=DateTime.UtcNow.AddDays(add_day);
        }
    }
}