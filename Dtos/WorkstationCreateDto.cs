namespace HybridWork.Dtos
{
    public class WorkstationCreateDto
    {
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Capacity { get; set; } = 1;
    }
}
