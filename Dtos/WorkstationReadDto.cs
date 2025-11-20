namespace HybridWork.Dtos
{
    public class WorkstationReadDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
