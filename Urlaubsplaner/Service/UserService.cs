using Urlaubsplaner.Enum;
using Urlaubsplaner.Model;

namespace Urlaubsplaner.Service
{
    public class UserService
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
                TeamId = 1,
                TotalVacationDays = 28,
                Roles = [Role.USER]
            });

            users.Add(new User()
            {
                Id = 3,
                FirstName = "Annika",
                LastName = "Muster",
                Username = "annmus00",
                Password = "test123",
                TeamId = 1,
                TotalVacationDays = 30,
                Roles = [Role.USER]
            });
            users.Add(new User()
            {
                Id = 4,
                FirstName = "Lara",
                LastName = "Leader",
                Username = "larlea00",
                Password = "test123",
                TeamId = 1,
                TotalVacationDays = 30,
                Roles = [Role.TEAMLEADER]
            });

            users.Add(new User()
            {
                Id = 5,
                FirstName = "Manfred",
                LastName = "Meerfeld",
                Username = "manmer00",
                Password = "test123",
                TeamId = 2,
                TotalVacationDays = 28,
                Roles = [Role.USER]
            });

            users.Add(new User()
            {
                Id = 6,
                FirstName = "Gudrun",
                LastName = "Gans",
                Username = "gudgan00",
                Password = "test123",
                TeamId = 2,
                TotalVacationDays = 30,
                Roles = [Role.USER]
            });
            users.Add(new User()
            {
                Id = 7,
                FirstName = "Leon",
                LastName = "Leader",
                Username = "leolea00",
                Password = "test123",
                TeamId = 2,
                TotalVacationDays = 30,
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

        public User? GetUserById(int id) { 
            return users.FirstOrDefault(u => u.Id == id); 
        }
    }
}
