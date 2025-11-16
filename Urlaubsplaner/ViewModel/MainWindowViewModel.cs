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

        public ObservableCollection<VacationApplication> TeamApplications { 
            get {
                if (CurrentUser.Roles.Contains(Enum.Role.TEAMLEADER))
                {
                    return _vacationApplicationService.GetTeamVacationApplications(CurrentUser.TeamId, e => e.UserId != CurrentUser.Id);
                }else if (CurrentUser.Roles.Contains(Enum.Role.ADMIN))
                {
                    return _vacationApplicationService.GetAllVacationApplications();
                }
                else return [];
            } 
        }
        public ObservableCollection<VacationApplication> Applications { 
            get { return _applications; } 
            set
            {
                _applications = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentUserRemainingVacation));
                OnPropertyChanged(nameof(TeamApplications));
            } 
        }

        public bool IsTeamLeader { get
            {
                return CurrentUser.Roles.Contains(Enum.Role.TEAMLEADER) || CurrentUser.Roles.Contains(Enum.Role.ADMIN);
            } 
        }

        private bool _teamTabActive { get; set; }

        public bool TeamTabActive { 
            get
            {
                return _teamTabActive;
            }
            set
            {
                _teamTabActive = value;
                OnPropertyChanged();
            }
        }

        private bool _personalTabActive { get; set; }

        public bool PersonalTabActive
        {
            get
            {
                return _personalTabActive;
            }
            set
            {
                _personalTabActive = value;
                SelectedApplication = null;
                OnPropertyChanged();
            }
        }
        public bool CanConfirm
        {
            get
            {
                return TeamTabActive && (CurrentUser.Roles.Contains(Enum.Role.TEAMLEADER) || CurrentUser.Roles.Contains(Enum.Role.ADMIN)) && SelectedApplication != null;
            }
        }

        public int DetailViewHeight { get; set; }


        private bool _detailViewVisible { get; set; }
        public bool DetailViewVisible {
            get
            {
                return _detailViewVisible;
            }
            set
            {
                _detailViewVisible = value;
                // fix for detail height, tried with Visibility.Collapsed, most likely a bug in my XAML
                if(_detailViewVisible)
                {
                    DetailViewHeight = 220;
                }else
                {
                    DetailViewHeight = 0;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(DetailViewHeight));
            } 
        }
        private VacationApplication? _selectedApplication { get; set; }

        public VacationApplication? SelectedApplication { 
            get
            {
                return _selectedApplication;
            } 
            set
            {
                _selectedApplication = value;
                if(_selectedApplication != null)
                {
                    DetailViewVisible = true;
                } else DetailViewVisible = false;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanConfirm));
            }
        }
        public ICommand VacationApplicationClickCommand { get; }
        public ICommand ConfirmApplicationClickCommand { get; }

        private HolidayApplicationWindow? _holidayApplicationWindow;

        private readonly VacationApplicationService _vacationApplicationService;
        public User CurrentUser { get; }

        private int _currentUserRemainingVacation => _vacationApplicationService.GetRemainingVacationForUser(CurrentUser);
        public int CurrentUserRemainingVacation { 
            get {
                return _currentUserRemainingVacation;
            } 
            set
            {
                OnPropertyChanged(nameof(CurrentUserRemainingVacation));
            }
        }

        public MainWindowViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            _vacationApplicationService = new VacationApplicationService(new Repository.VacationApplicationRepository(), new UserService());
            UpdateApplications();

            VacationApplicationClickCommand = new RelayCommand(execute => OnVacationApplicationClicked());
            ConfirmApplicationClickCommand = new RelayCommand(execute => OnConfirmApplicationClicked());
            PersonalTabActive = true;
            DetailViewVisible = false;
        }

        private void UpdateApplications()
        {
            Applications = _vacationApplicationService.GetVacationApplicationsByUser(CurrentUser);
            Trace.WriteLine($"New Application, now at {Applications.Count} applications");
        }

        private void OnConfirmApplicationClicked()
        {
            // just recheck whether user is allowed
            if(!CurrentUser.Roles.Contains(Enum.Role.TEAMLEADER) && !CurrentUser.Roles.Contains(Enum.Role.ADMIN))
            {
                return;
            }

            if(CurrentUser.Roles.Contains(Enum.Role.TEAMLEADER))
            {
                if(SelectedApplication?.User?.TeamId != CurrentUser.TeamId)
                {
                    return;
                }
            }

            if (SelectedApplication != null)
            {
                _vacationApplicationService.ConfirmApplication(SelectedApplication.Id, CurrentUser.Id);
                UpdateApplications();
            }
            


        }

        private void OnVacationApplicationClicked()
        {
            // make sure only one application window is open at any time
            if (_holidayApplicationWindow != null)
            {
                _holidayApplicationWindow.Focus();
                return;
            }

            HolidayApplicationViewModel viewModel = new HolidayApplicationViewModel(_vacationApplicationService, CurrentUser, _currentUserRemainingVacation);
            

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
