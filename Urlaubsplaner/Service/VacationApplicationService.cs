using System.Collections.ObjectModel;
using Urlaubsplaner.Model;

namespace Urlaubsplaner.Service
{
    public class VacationApplicationService
    {

        private ObservableCollection<VacationApplication> applications;

        public VacationApplicationService()
        {
            applications = new ObservableCollection<VacationApplication>();
        }

        public void SaveApplication(VacationApplication application)
        {
            applications.Add(application);
        }

        public ObservableCollection<VacationApplication> GetVacationApplicationsByUser(User user)
        {
            return new ObservableCollection<VacationApplication>(applications.Where(application => application.User.Id == user.Id));
        }
    }
}
