using IntelliSurgery.DbOperations;
using IntelliSurgery.DbOperations.WorkingBlocks;
using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using IntelliSurgery.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IntelliSurgery.Controllers
{
    //[ApiController]
    //[Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
        private readonly IWorkingBlockRepository workingBlockRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ISurgeryRepository surgeryRepository;
        string dateFormat = "yyyy-MM-dd HH:mm:ss.ffffff";
        CultureInfo provider = CultureInfo.InvariantCulture;

        public DataController(IWorkingBlockRepository workingBlockRepository, IAppointmentRepository appointmentRepository,
            ISurgeryRepository surgeryRepository) 
        {
            this.workingBlockRepository = workingBlockRepository;
            this.appointmentRepository = appointmentRepository;
            this.surgeryRepository = surgeryRepository;
        }

        [HttpGet]
        public IActionResult Load()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Load(LoadDataViewModel model)
        {
            appointmentRepository.DeleteAllAppointments().Wait();
            workingBlockRepository.DeleteAllWorkingBlocks().Wait();
            surgeryRepository.DeleteAllScheduledSurgeries().Wait();
            surgeryRepository.DeleteAllSurgeryEvents().Wait();

            int scenario = model.ScenarioNumber;
            string path = @"../IntelliSurgery/Scenarios/WorkingBlocks/scenario" + scenario.ToString() + ".csv";
            List<WorkingBlock> workingBlocks = ReadWorkingBlocksInCSV(path);
            await workingBlockRepository.AddWorkingBlocks(workingBlocks);

            path = @"../IntelliSurgery/Scenarios/Appointments/scenario" + scenario.ToString() + ".csv";
            List<Appointment> appointments = ReadAppointmentsInCSV(path);
            await appointmentRepository.AddAppointments(appointments);

            return View(model);
        }

        private List<Appointment> ReadAppointmentsInCSV(string csvPath)
        {
            List<Appointment> appointments = new List<Appointment>();
            List<string> lines = System.IO.File.ReadAllLines(csvPath).ToList();
            for (int i = 1; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(',');

                Appointment appointment = new Appointment()
                {
                    Id = int.Parse(columns[0]),
                    PatientId = int.Parse(columns[1]),
                    SurgeonId = int.Parse(columns[2]),
                    SurgeryTypeId = int.Parse(columns[3]),
                    PriorityLevel = (PriorityLevel)int.Parse(columns[4]),
                    AnesthesiaType = (AnesthesiaType)int.Parse(columns[5]),
                    SystemPredictedDuration = columns[6] == "NULL" ? null : TimeSpan.Parse(columns[6]),
                    Status = (Status)int.Parse(columns[7]),
                    DateAdded = DateTime.ParseExact(columns[8].Trim('\"'), dateFormat, provider),
                    ScheduledSurgeryId = columns[9] == "NULL" ? null : int.Parse(columns[9]),
                    TheatreTypeId = int.Parse(columns[10]),
                    TheatreId = columns[11] == "NULL" ? null : int.Parse(columns[11]),
                    ComplicationPossibility = int.Parse(columns[12]) == 1 ? true : false,
                    ApproximateProcedureDate = columns[13] == "NULL" ? null : DateTime.ParseExact(columns[13].Trim('\"'), dateFormat, provider),
                    SurgeonsPredictedDuration = columns[14] == "NULL" ? null : TimeSpan.Parse(columns[14]),
                    WorkingBlockId = columns[15] == "NULL" ? null : int.Parse(columns[15])
                };

                appointments.Add(appointment);

            }

            return appointments;
        }
        private List<WorkingBlock> ReadWorkingBlocksInCSV(string csvPath)
        {
            List<WorkingBlock> workingBlocks = new List<WorkingBlock>();
            List<string> lines = System.IO.File.ReadAllLines(csvPath).ToList();
            for (int i = 1; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(',');

                WorkingBlock workingBlock = new WorkingBlock()
                {
                    Id = int.Parse(columns[0]),
                    RemainingTime = TimeSpan.Parse(columns[1]),
                    Start = DateTime.ParseExact(columns[2].Trim('\"'), dateFormat, provider),
                    End = DateTime.ParseExact(columns[3].Trim('\"'), dateFormat, provider),
                    Duration = TimeSpan.Parse(columns[4]),
                    TheatreId = int.Parse(columns[5]),
                    SurgeonId = int.Parse(columns[6])
                };
                workingBlocks.Add(workingBlock);

            }

            return workingBlocks;
        }
    }
}
