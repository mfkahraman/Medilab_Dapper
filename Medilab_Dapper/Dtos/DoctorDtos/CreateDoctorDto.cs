namespace Medilab_Dapper.Dtos.DoctorDtos
{
    public class CreateDoctorDto
    {
        public required string NameSurname { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public required string DepartmentId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
