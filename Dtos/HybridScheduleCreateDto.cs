namespace HybridWork.Dtos
{
    public class HybridScheduleCreateDto
    {
        public string EmployeeId { get; set; } = null!;
        public string DayOfWeek { get; set; } = null!;
        public string WorkMode { get; set; } = "Office";
    }
}
