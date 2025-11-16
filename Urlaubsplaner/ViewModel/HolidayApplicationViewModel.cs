using System.Windows;
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

        private DateTime _from = DateTime.Now;
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

        private DateTime _to = DateTime.Now;
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

        public HolidayApplicationViewModel(VacationApplicationService vacationApplicationService, User currentUser, int vacationDays)
        {
            _currentUser = currentUser;
            _vacationApplicationService = vacationApplicationService;
            VacationDays = vacationDays;
            AppliedClickCommand = new RelayCommand(execute => OnButtonClicked());
            UpdateVacationCalculation();
        }

        private void OnButtonClicked()
        {
            var application = new VacationApplication(From, To, _currentUser.Id);
            // validate data


            // save application
            if((VacationDays - application.GetBusinessDaysBetween()) < 0)
            {
                MessageBox.Show("Sie haben nicht genug Resturlaub, um den Urlaubsantrag vorzunehmen.");
                return;
            }


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
            // calculation of business days is done in model to reuse in datagrid.
            // I just initalize a VacationApplication for the calculation, could also live in helper class
            var vacationApplication = new VacationApplication(From, To, _currentUser.Id);
            RemainingVacationDays = VacationDays - vacationApplication.GetBusinessDaysBetween();

        }

       
    }
}

    
