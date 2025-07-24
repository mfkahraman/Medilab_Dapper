namespace Medilab_Dapper.Dtos.DoctorDtos
{
    public class ResultDoctorDto
    {
        public int DoctorId { get; set; }
        public required string NameSurname { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int DepartmentId { get; set; }
    }
}
