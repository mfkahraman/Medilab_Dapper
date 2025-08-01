﻿namespace Medilab_Dapper.Dtos.DepartmentDtos
{
    public class CreateDepartmentDto
    {
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }

    }
}
