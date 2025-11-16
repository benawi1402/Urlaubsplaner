using System.Xml.Serialization;

namespace Urlaubsplaner.Model
{
    public class VacationApplication : ModelBase
    {
        private bool _confirmed = false;
        private int? _confirmedById;

        public int Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int UserId { get; set; }

        // needs to be loaded separately
        public User? User { get; set; }

        // needs to be loaded separately
        public User? ConfirmedBy { get; set; }

        public bool Confirmed { 
            get => _confirmed;
            set {
                if (SetProperty(ref _confirmed, value))
                {
                    UpdateLastEdited();
                }

            }
        }

        public int? ConfirmedById
        {
            get => _confirmedById;
            set
            {
                if (SetProperty(ref _confirmedById, value))
                {
                    UpdateLastEdited();
                }

            }
        }
        public DateTime LastEdited { get; set; }
        public DateTime Added { get; set; }
        public int Duration => GetBusinessDaysBetween();


        public VacationApplication(DateTime from, DateTime to, int userId)
        {
            From = from;
            To = to;
            UserId = userId;

            Added = DateTime.Now;
        }

        public VacationApplication()
        {
            From = DateTime.Now;
            To = DateTime.Now;
            UserId = -1;
            Added = DateTime.Now;
        }

        private void UpdateLastEdited()
        {
            LastEdited = DateTime.Now;
        }

        // only use business days for calculating remaining vacation
        public int GetBusinessDaysBetween()
        {
            double calcBusinessDays =
                1 + ((To - From).TotalDays * 5 -
                (From.DayOfWeek - To.DayOfWeek) * 2) / 7;

            if (To.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (From.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return (int)calcBusinessDays;
        }
    }
}
