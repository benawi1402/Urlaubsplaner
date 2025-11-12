using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urlaubsplaner.Model;

namespace Urlaubsplaner.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<VacationApplication> Applications { get; set; }
                
        
        public MainWindowViewModel()
        {
            Applications = new ObservableCollection<VacationApplication>();
            Applications.Add(new VacationApplication(DateTime.Now.AddDays(2), DateTime.Now.AddDays(20), new User()
            {
                FirstName = "Frederik",
                LastName = "Maaßen"
            }));

            Console.WriteLine($"We have {Applications.Count} applications");
        }

    }
}
