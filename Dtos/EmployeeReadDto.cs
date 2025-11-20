namespace HybridWork.Dtos
{
    public class EmployeeReadDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
