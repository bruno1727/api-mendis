namespace ApiMendis.User
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public required string Email { get; set; }
    }
}
