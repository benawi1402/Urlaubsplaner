using System.Windows;
using Urlaubsplaner.Model;
using Urlaubsplaner.ViewModel;

namespace Urlaubsplaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel(currentUser);
            DataContext = viewModel;
        }
    }
}