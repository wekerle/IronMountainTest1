using BusinessLayer.DTO;
using DataAccessLayer.DbModels;
using DataAccessLayer.DbRepository;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserService
    {
        private IUserRepository _repository;

        public UserService(string connectionString)
        {
            _repository = new UserRepository(connectionString);
        }

        public LoginResult Login(LoginRequest loginRequest) {

            var user = _repository.GetUserByUsername(loginRequest.Username);
            
            var result = new LoginResult();
            if (user == null)
            {
                result.IsSucces = false;
                result.ErrorMessage = "Incorrect username";
            }
            else if (user.PasswordHash != GetHashedPassword(loginRequest.Password)) 
            {
                result.IsSucces = false;
                result.ErrorMessage = "Incorrect password";
            }
            else
            {
                result.IsSucces = true;
            }

            return result;
        }

        public UserDto GetUser(int userId)
        {
            var data = _repository.GetUser(userId);
            return UserToUserDtoConvert(data);
        }

        public void AddUser(UserDto user) {
            _repository.AddUser(UserDtoToUserConvert(user));
        }

        public void DeleteUser(int id)
        {
            _repository.DeleteUser(id);
        }

        public void UpdateUser(UserDto user) {
            _repository.UpdateUser(UserDtoToUserConvert(user));
        }

        private User UserDtoToUserConvert(UserDto user) {
            return new User()
            {
                PasswordHash = user.PasswordHash,
                Username = user.UserName,
                UserId = user.UserId
            };
        }

        private UserDto UserToUserDtoConvert(User user)
        {
            return new UserDto()
            {
                PasswordHash = user.PasswordHash,
                UserName = user.Username,
                UserId = user.UserId
            };
        }

        private string GetHashedPassword(string password) {
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(password);
            var sha1data = sha1.ComputeHash(data);

            var hashedPassword = string.Empty;
            foreach (byte b in sha1data)
            {
                hashedPassword += string.Format("{0,2:X2}", b);
            }

            return hashedPassword;
        }
    }
}
