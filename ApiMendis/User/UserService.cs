using ApiMendis.Notifications;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ApiMendis.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly INotificationService _notificationService;


        public UserService(
            INotificationService notificationService,
            IRepository<User> repository)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<bool> SignUpAsync(User user)
        {
            if (!IsValidEmail(user.Email))
            {
                _notificationService.Add(ValidationMessages.InvalidEmail);
                return false;
            }

            return await _repository.InsertAsync(user);
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
