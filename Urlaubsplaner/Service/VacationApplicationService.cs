using System.Collections.ObjectModel;
using Urlaubsplaner.Model;
using Urlaubsplaner.Repository;

namespace Urlaubsplaner.Service
{
    public class VacationApplicationService
    {

        private VacationApplicationRepository _repository;
        private UserService _userService;

        public VacationApplicationService(VacationApplicationRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public void SaveApplication(VacationApplication application)
        {
            _repository.AddApplication(application);
            _repository.SaveApplications();
        }

        public ObservableCollection<VacationApplication> GetFilteredApplications(Func<VacationApplication, bool> predicate)
        {
            var applications = _repository.GetVacationApplications();
            // not optimal, but we need to load the users for all applications because predicate might filter by user information
            // this would be no issue with a decent ORM implementation and navigation properties
            // 
            // caching would be great here
            foreach (var application in applications ?? [])
            {
                application.User = _userService.GetUserById(application.UserId);
                if(application.ConfirmedById != null)
                {
                    application.ConfirmedBy = _userService.GetUserById(application.ConfirmedById ?? 0);
                }
            }
            var filteredApplications = applications?.Where(predicate);

            

            if (filteredApplications != null && filteredApplications.Any())
            {
                return new ObservableCollection<VacationApplication>(filteredApplications);
            }
            return [];
        }

        public ObservableCollection<VacationApplication> GetAllVacationApplications()
        {
            return GetFilteredApplications(application => true);
        }

        public ObservableCollection<VacationApplication> GetVacationApplicationsByUser(User user)
        {
            return GetFilteredApplications(application => application.UserId == user.Id);
            
        }

        public ObservableCollection<VacationApplication> GetTeamVacationApplications(int teamId, Func<VacationApplication, bool> predicate)
        {
            return GetFilteredApplications(application => application.User?.TeamId == teamId && predicate(application));
        }

        public int GetRemainingVacationForUser(User user)
        {
            // we assume that all booked vacation (confirmed or not) is subtracted from the total vacation days to avoid special cases
            var vacationDays = user.TotalVacationDays;
            foreach(var application in GetVacationApplicationsByUser(user))
            {
                vacationDays -= application.Duration;
            }

            return vacationDays;
        }

        public void ConfirmApplication(int vacationApplicationId, int userId)
        {
            _repository.ConfirmApplication(vacationApplicationId, userId);
        }
    }
}
