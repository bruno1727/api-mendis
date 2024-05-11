namespace ApiMendis.User
{
    public interface IUserService
    {
        public Task SignUpAsync(User user);
    }
}
