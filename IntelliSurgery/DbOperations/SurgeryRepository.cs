namespace IntelliSurgery.DbOperations
{
    public class SurgeryRepository : ISurgeryRepository
    {
        private readonly AppDbContext context;

        public SurgeryRepository(AppDbContext context)
        {
            this.context = context;
        }
    }
}
