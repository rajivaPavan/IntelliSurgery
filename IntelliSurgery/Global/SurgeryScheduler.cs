
using IntelliSurgery.DbOperations;
using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Global
{
    public class SurgeryScheduler
    {
        private readonly ISurgeryRepository surgeryRepository;

        public SurgeryScheduler(ISurgeryRepository surgeryRepository)
        {
            this.surgeryRepository = surgeryRepository;
        }

        public Task CreateSchedule(List<Appointment> appointments)
        {
            //
            //implement algorithm
            //make necessary updates in the database
            //
            //

            return Task.CompletedTask;
        }
        
    }
}
