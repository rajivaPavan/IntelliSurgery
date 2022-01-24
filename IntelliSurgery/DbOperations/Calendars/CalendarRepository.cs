namespace IntelliSurgery.DbOperations
{
    public class CalendarRepository:ICalendarRepository
    {
        private readonly AppDbContext context;

        public CalendarRepository(AppDbContext context)
        {
            this.context = context;
        }
    }
}
