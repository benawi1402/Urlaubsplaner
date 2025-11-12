using Urlaubsplaner.Enum;
using Urlaubsplaner.Model;

namespace Urlaubsplaner.Service
{
    internal class UserService
    {
        private List<User> users;
        public UserService() { 
            users = new List<User>();

            users.Add(new User()
            {
                Id = 2,
                FirstName = "Max",
                LastName = "Muster",
                Username = "maxmus00",
                Password = "test123",
                Roles = [Role.USER]
            });

            users.Add(new User()
            {
                Id = 3,
                FirstName = "Annika",
                LastName = "Muster",
                Username = "annmus00",
                Password = "test123",
                Roles = [Role.USER]
            });
            users.Add(new User()
            {
                Id = 4,
                FirstName = "Lara",
                LastName = "Leader",
                Username = "larlea00",
                Password = "test123",
                Roles = [Role.TEAMLEADER]
            });

            users.Add(new User()
            {
                Id = 1,
                FirstName = "Adrian",
                LastName = "Admin",
                Username = "adradm00",
                Password = "test123",
                Roles = [Role.ADMIN]
            });


        }

        public User? FindUser(string username)
        {
            return users.FirstOrDefault(e => e.Username == username);
        }
    }
}
