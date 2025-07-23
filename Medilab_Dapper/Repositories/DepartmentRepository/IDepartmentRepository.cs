using Medilab_Dapper.Dtos.DepartmentDtos;

namespace Medilab_Dapper.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<ResultDepartmentDto>> GetAllDepartmentsAsync();
        Task<GetDepartmentByIdDto> GetDepartmentByIdAsync(int id);

        Task CreateDepartmentAsync(CreateDepartmentDto createDto);
        Task UpdateDepartmentAsync(UpdateDepartmentDto updateDto);
        Task DeleteDepartmentAsync(int id);
    }
}
