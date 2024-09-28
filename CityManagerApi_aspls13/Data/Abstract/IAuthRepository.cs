using CityManagerApi_aspls13.Entities;

namespace CityManagerApi_aspls13.Data.Abstract
{
    public interface IAuthRepository
    {
        Task<User>Register(User user,string password);
        Task<User>Login(string username,string password);
        Task<bool> UserExists(string username);

    }
}
