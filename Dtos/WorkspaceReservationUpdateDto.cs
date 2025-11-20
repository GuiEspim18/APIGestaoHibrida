namespace HybridWork.Dtos
{
    public class WorkspaceReservationUpdateDto
    {
        public string? EmployeeId { get; set; }
        public string? WorkstationId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Status { get; set; }
    }
}
