namespace HMSDataAccess.Entity
{
    public class Patient
    {
        public Patient()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
