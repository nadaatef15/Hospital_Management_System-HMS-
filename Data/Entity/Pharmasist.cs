namespace HMSDataAccess.Entity
{
    public class Pharmasist
    {
        public Pharmasist()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
