using Medilab_Dapper.Dtos.AppointmentDtos;

namespace Medilab_Dapper.Repositories.AppointmentRepository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<ResultAppointmentDto>> GetAllAppointmentsAsync();
        Task<bool> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task<bool> ConfirmAppointment(int appointmentId);
        Task<bool> CancelAppointment(int appointmentId);
        Task<bool> DeleteAppointment(int appointmentId);
    }
}
