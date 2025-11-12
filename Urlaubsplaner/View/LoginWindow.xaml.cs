using System.Windows;
using System.Windows.Controls;
using Urlaubsplaner.ViewModel;
using static Urlaubsplaner.ViewModel.LoginWindowViewModel;

namespace Urlaubsplaner.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginWindowViewModel viewModel = new LoginWindowViewModel();
            viewModel.OnLoginSuccessful += (s,e) => onLoginSuccessful(e);
            DataContext = viewModel;
        }

        private void onLoginSuccessful(LoginSuccessfulEventArgs args)
        {
            Application.Current.MainWindow = new MainWindow(args.User);
            Application.Current.MainWindow.Show();
            this.Close();
        }


        // PasswordBox does not have data binding capabilities, I use this workaround from Stackoverflow https://stackoverflow.com/questions/1483892/how-to-bind-to-a-passwordbox-in-mvvm
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((LoginWindowViewModel)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }
    }
}
