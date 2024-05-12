namespace ApiMendis.User
{
    public interface IUserService
    {
        public Task<bool> SignUpAsync(User user);
    }
}
