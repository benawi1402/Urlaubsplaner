using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Urlaubsplaner.ViewModel;

namespace Urlaubsplaner.View
{
    /// <summary>
    /// Interaction logic for HolidayApplication.xaml
    /// </summary>
    public partial class HolidayApplication : Window
    {
        public HolidayApplication()
        {
            InitializeComponent();
            HolidayApplicationViewModel applicationViewModel = new HolidayApplicationViewModel();
            DataContext = applicationViewModel;
        }
    }
}
