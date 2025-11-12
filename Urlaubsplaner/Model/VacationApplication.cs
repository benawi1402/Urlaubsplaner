namespace Urlaubsplaner.Model
{
    public class VacationApplication : ModelBase
    {
        private bool _confirmed = false;
        private User? _confirmedBy;

        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public User User { get; set; }

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
        public DateTime LastEdited { get; set; }
        public DateTime Added { get; set; }
        public int Duration => (To - From).Days;

        public VacationApplication(DateTime from, DateTime to, User user)
        {
            From = from;
            To = to;
            User = user;

            Added = DateTime.Now;
        }

        private void UpdateLastEdited()
        {
            LastEdited = DateTime.Now;
        }
    }
}
