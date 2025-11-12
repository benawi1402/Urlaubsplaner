using System.Windows.Input;
using Urlaubsplaner.Model;
using Urlaubsplaner.Service;
using Urlaubsplaner.Util;

namespace Urlaubsplaner.ViewModel
{
    public class HolidayApplicationViewModel : ViewModelBase
    {
        public ICommand AppliedClickCommand { get; }

        private readonly VacationApplicationService _vacationApplicationService;
        private readonly User _currentUser;

        private DateTime _from;
        public DateTime From
        {
            get => _from;
            set
            {
                _from = value;
                OnPropertyChanged();
                UpdateVacationCalculation();
            }
        }

        private DateTime _to;
        public DateTime To
        {
            get => _to;
            set
            {
                _to = value;
                OnPropertyChanged();
                UpdateVacationCalculation();
            }
        }

        private int _vacationDays;
        public int VacationDays
        {
            get => _vacationDays;
            set
            {
                _vacationDays = value;
                OnPropertyChanged();
            }
        }

        private int _remainingVacationDays;
        public int RemainingVacationDays
        {
            get => _remainingVacationDays;
            set
            {
                _remainingVacationDays = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler? OnApplicationAdded;

        public HolidayApplicationViewModel(VacationApplicationService vacationApplicationService, User currentUser)
        {
            _currentUser = currentUser;
            _vacationApplicationService = vacationApplicationService;
            AppliedClickCommand = new RelayCommand(execute => OnButtonClicked());
            VacationDays = 100;
        }

        private void OnButtonClicked()
        {
            // validate data

            // save application
            var application = new VacationApplication(From, To, _currentUser);
            _vacationApplicationService.SaveApplication(application);
            OnApplicationAdded?.Invoke(this, new EventArgs());
        }

        private void UpdateVacationCalculation()
        {
            // make sure To is bigger than From
            if(To < From)
            {
                return;
            }

            var diff = To - From;

            RemainingVacationDays = VacationDays - diff.Days;

        }
    }
}

    
