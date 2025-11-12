using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Urlaubsplaner.Model;
using Urlaubsplaner.Service;
using Urlaubsplaner.Util;
using Urlaubsplaner.View;

namespace Urlaubsplaner.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        private ObservableCollection<VacationApplication> _applications;
        public ObservableCollection<VacationApplication> Applications { 
            get { return _applications; } 
            set
            {
                _applications = value;
                OnPropertyChanged();
            } 
        }

        public ICommand VacationApplicationClickCommand { get; }

        private HolidayApplicationWindow? _holidayApplicationWindow;

        private VacationApplicationService _vacationApplicationService;
        private readonly User _currentUser;

        public MainWindowViewModel(User currentUser)
        {
            _currentUser = currentUser;
            _vacationApplicationService = new VacationApplicationService();
            UpdateApplications();

            VacationApplicationClickCommand = new RelayCommand(execute => OnVacationApplicationClicked());


        }

        private void UpdateApplications()
        {
            Applications = _vacationApplicationService.GetVacationApplicationsByUser(_currentUser);
            Trace.WriteLine($"New Application, now at {Applications.Count} applications");
        }

        private void OnVacationApplicationClicked()
        {
            // make sure only one application window is open at any time
            if (_holidayApplicationWindow != null)
            {
                _holidayApplicationWindow.Focus();
                return;
            }

            HolidayApplicationViewModel viewModel = new HolidayApplicationViewModel(_vacationApplicationService, _currentUser);
            

            _holidayApplicationWindow = new HolidayApplicationWindow(viewModel);
            _holidayApplicationWindow.Owner = App.Current.MainWindow;

            // ensure that cached window is cleared when window closes
            _holidayApplicationWindow.Closed += (s, e) => _holidayApplicationWindow = null;

            // allow the view model to request closing the window
            viewModel.OnApplicationAdded += (s, e) => { 
                _holidayApplicationWindow.Close();
                UpdateApplications();
            };
            _holidayApplicationWindow.Show();
        }
    }
}
