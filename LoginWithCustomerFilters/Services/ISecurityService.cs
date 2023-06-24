using WebAppStorePractice.Models;

namespace WebAppStorePractice.Services
{
    public interface ISecurityService
    {
        public bool IsValid(UserModel user);
        public bool IsUnique(string userName);
    }
}
