namespace HybridWork.Dtos
{
    public class WorkspaceReservationReadDto
    {
        public string? Id { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string WorkstationId { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Status { get; set; } = null!;
    }
}
