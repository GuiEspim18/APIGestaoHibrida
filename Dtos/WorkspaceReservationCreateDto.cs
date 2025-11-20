namespace HybridWork.Dtos
{
    public class WorkspaceReservationCreateDto
    {
        public string EmployeeId { get; set; } = null!;
        public string WorkstationId { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
