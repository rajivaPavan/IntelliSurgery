using System.ComponentModel.DataAnnotations;

namespace IntelliSurgery.ViewModels
{
    public class LoadDataViewModel
    {
        public int ScenarioNumber { get; set; }
        [Display(Name = "Wait for")]
        public double WaitingDays { get; set; }
        [Display(Name = "Schedule for")]
        public double ScheduleDays { get; set; }
    }
}
