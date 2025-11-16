using Urlaubsplaner.Entity;
using Urlaubsplaner.Model;

namespace Urlaubsplaner.Mapper
{
    public class VacationApplicationMapper
    {
        public static VacationApplication Map(VacationApplicationEntity entity)
        {
            var model = new VacationApplication();
            model.Id = entity.Id;
            model.Added = entity.Added;
            model.Confirmed = entity.Confirmed;
            model.ConfirmedById = entity.ConfirmedBy;
            model.UserId = entity.UserId;
            model.From = entity.From;
            model.To = entity.To;

            return model;
        }

        public static VacationApplicationEntity Map(VacationApplication model)
        {
            var entity = new VacationApplicationEntity();
            entity.Id = model.Id;
            entity.Added = model.Added;
            entity.Confirmed = model.Confirmed;
            entity.ConfirmedBy = model.ConfirmedById;
            entity.UserId = model.UserId;
            entity.From = model.From;
            entity.To = model.To;
            return entity;
        }
    }
}
