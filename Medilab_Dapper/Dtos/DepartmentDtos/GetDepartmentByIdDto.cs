namespace Medilab_Dapper.Dtos.DepartmentDtos
{
    public class GetDepartmentByIdDto
    {
        public required int DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }


    }
}
