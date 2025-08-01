﻿namespace Medilab_Dapper.Dtos.DepartmentDtos
{
    public class UpdateDepartmentDto
    {
        public required int DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
