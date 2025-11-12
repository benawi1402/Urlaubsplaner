using System.Windows;
using System.Windows.Input;
using Urlaubsplaner.Model;
using Urlaubsplaner.Service;
using Urlaubsplaner.Util;

namespace Urlaubsplaner.ViewModel
{
    internal class LoginWindowViewModel : ViewModelBase
    {

        public ICommand LoginCommand { get; }
        public string Username { get; set; }
        public string Password { private get; set; }

        private UserService userService;

        public event EventHandler<LoginSuccessfulEventArgs> OnLoginSuccessful;

        public LoginWindowViewModel()
        {
            userService = new UserService();
            LoginCommand = new RelayCommand(execute => OnLoginClicked());
        }

        private void OnLoginClicked()
        {
            var user = userService.FindUser(Username);
            if (user == null)
            {
                MessageBox.Show("Login fehlgeschlagen. Es wurde kein Benutzer mit dem Benutzernamen gefunden.");
                return;
            }

            if (user.Password != Password)
            {
                MessageBox.Show("Login fehlgeschlagen. Das Passwort war nicht korrekt.");
                return;
            }

            OnLoginSuccessful(this, new LoginSuccessfulEventArgs() { User = user });
        }

        public class LoginSuccessfulEventArgs : EventArgs
        {
            public User User { get; set; }
        }
    }
}
