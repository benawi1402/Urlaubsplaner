using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urlaubsplaner.Model;

namespace Urlaubsplaner.Service
{
    class VacationApplicationService
    {

        private ObservableCollection<VacationApplication> applications;
        public void SaveApplication(VacationApplication application)
        {

        }

        public ObservableCollection<VacationApplication> GetVacationApplicationsByUser(User user)
        {
            return new ObservableCollection<VacationApplication>(applications.Where(application => application.User.Id == user.Id));
        }
    }
}
