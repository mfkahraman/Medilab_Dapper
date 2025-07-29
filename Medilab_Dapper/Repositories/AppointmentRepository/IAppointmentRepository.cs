using Medilab_Dapper.Dtos.AppointmentDtos;

namespace Medilab_Dapper.Repositories.AppointmentRepository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<ResultAppointmentDto>> GetAllAppointmentsAsync();
        Task<ResultAppointmentDto>GetAppointmentByIdAsync(int id);
        Task<bool> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task<bool> ConfirmAppointmentAsync(int appointmentId);
        Task<bool> CancelAppointmentAsync(int appointmentId);
        Task<bool> DeleteAppointmentAsync(int appointmentId);
    }
}
