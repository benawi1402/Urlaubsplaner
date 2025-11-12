using System.Windows;
using Urlaubsplaner.Model;
using Urlaubsplaner.Service;
using Urlaubsplaner.ViewModel;

namespace Urlaubsplaner.View
{
    /// <summary>
    /// Interaction logic for HolidayApplication.xaml
    /// </summary>
    public partial class HolidayApplicationWindow : Window
    {
        public HolidayApplicationWindow(HolidayApplicationViewModel holidayApplicationViewModel)
        {
            InitializeComponent();
            DataContext = holidayApplicationViewModel;
        }
    }
}
