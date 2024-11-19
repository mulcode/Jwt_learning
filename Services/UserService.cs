using JwtTut.Models;

namespace JwtTut.Services
{
    public class UserService
    {
        private List<User> users = new List<User>{
            new User {UserName = "Nazmul", Password = "123"},
            new User {UserName = "Ahsan", Password = "124"}
        };

        public User GetUser(string username, string password) {
            // var user = users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();

            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if(user.UserName == username && user.Password == password) {
                    return user;
                }
            }

            return null;
        }

        internal bool GetUser(string token)
        {
            throw new NotImplementedException();
        }
    }
}