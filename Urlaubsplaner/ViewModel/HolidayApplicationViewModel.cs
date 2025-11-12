using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Urlaubsplaner.Util;

namespace Urlaubsplaner.ViewModel
{
    class HolidayApplicationViewModel : BaseViewModel
    {
        public ICommand AppliedClickCommand { get; }

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

        public HolidayApplicationViewModel()
        {
            AppliedClickCommand = new RelayCommand(execute => OnButtonClicked());
            VacationDays = 100;
        }

        private void OnButtonClicked()
        {
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

    
