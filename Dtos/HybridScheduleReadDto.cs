namespace HybridWork.Dtos
{
    public class HybridScheduleReadDto
    {
        public string? Id { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string DayOfWeek { get; set; } = null!;
        public string WorkMode { get; set; } = null!;
    }
}
