namespace ApiMendis.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task SignUpAsync(User user)
        {
            await _repository.InsertAsync(user);
        }
    }
}
