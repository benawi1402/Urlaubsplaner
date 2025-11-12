using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urlaubsplaner.Model
{
    class VacationApplication : ModelBase
    {
        private bool _confirmed = false;
        private User? _confirmedBy;

        public int Id;
        public DateTime From;
        public DateTime To;
        public User User;

        public bool Confirmed { 
            get => _confirmed;
            set {
                if (SetProperty(ref _confirmed, value))
                {
                    UpdateLastEdited();
                }

            }
        }
        public User? ConfirmedBy
        {
            get => _confirmedBy;
            set
            {
                if (SetProperty(ref _confirmedBy, value))
                {
                    UpdateLastEdited();
                }

            }
        }
        public DateTime LastEdited = DateTime.Now;
        public DateTime Added = DateTime.Now;
        public int Duration => (To - From).Days;

        public VacationApplication(DateTime from, DateTime to, User user)
        {
            From = from;
            To = to;
            User = user;
        }

        private void UpdateLastEdited()
        {
            LastEdited = DateTime.Now;
        }
    }
}
