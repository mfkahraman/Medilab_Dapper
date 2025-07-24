namespace Medilab_Dapper.Dtos.DoctorDtos
{
    public class ResultDoctorWithDepartmentDto
    {
        public int DoctorId { get; set; }
        public required string NameSurname { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? DepartmentName { get; set; }
    }
}
