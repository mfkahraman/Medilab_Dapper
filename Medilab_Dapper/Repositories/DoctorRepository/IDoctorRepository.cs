using Medilab_Dapper.Dtos.DoctorDtos;

namespace Medilab_Dapper.Repositories.DoctorRepository
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<ResultDoctorDto>> GetAllDoctorsAsync();
        Task<GetDoctorByIdDto> GetDoctorByIdAsync(int id);
        Task<IEnumerable<ResultDoctorDto>> GetDoctorsByDepartmentIdAsync(int departmentId);
        Task CreateDoctorAsync(CreateDoctorDto createDto);
        Task UpdateDoctorAsync(UpdateDoctorDto updateDto);
        Task DeleteDoctorAsync(int id);
    }
}
