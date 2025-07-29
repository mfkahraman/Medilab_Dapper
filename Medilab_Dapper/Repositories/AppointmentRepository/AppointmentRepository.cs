using Dapper;
using Medilab_Dapper.Context;
using Medilab_Dapper.Dtos.AppointmentDtos;
using System.Data;

namespace Medilab_Dapper.Repositories.AppointmentRepository
{
    public class AppointmentRepository(DapperContext context) : IAppointmentRepository
    {
        private readonly IDbConnection dbConnection = context.CreateConnection();
        public Task<bool> CancelAppointment(int appointmentId)
        {
            var query = "UPDATE Appointments SET IsActive = 0 WHERE AppointmentId = @AppointmentId";
            var parameters = new { AppointmentId = appointmentId };
            return dbConnection.ExecuteAsync(query, parameters).ContinueWith(t => t.Result > 0);
        }

        public Task<bool> ConfirmAppointment(int appointmentId)
        {
            var query = "UPDATE Appointments SET IsConfirmed = 1 WHERE AppointmentId = @AppointmentId";
            var parameters = new { AppointmentId = appointmentId };
            return dbConnection.ExecuteAsync(query, parameters).ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            string query = @"
                INSERT INTO Appointments (FullName, Email, Phone, AppointmentDate, DoctorId, DepartmentId, Message, IsActive, CreateDate, IsConfirmed) VALUES 
                (@FullName, @Email, @Phone, @AppointmentDate, @DoctorId, @DepartmentId, @Message, @IsActive, @CreateDate, @IsConfirmed)";
            var parameters = new DynamicParameters(createAppointmentDto);
            parameters.Add("CreateDate", DateTime.UtcNow);
            parameters.Add("IsActive", true);
            parameters.Add("IsConfirmed", false);
            int result = await dbConnection.ExecuteAsync(query, parameters);
            return result > 0;
        }

        public Task<bool> DeleteAppointment(int appointmentId)
        {
            var query = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
            var parameters = new { AppointmentId = appointmentId };
            return dbConnection.ExecuteAsync(query, parameters).ContinueWith(t => t.Result > 0);
        }

        public Task<IEnumerable<ResultAppointmentDto>> GetAllAppointmentsAsync()
        {
            var query = @" SELECT A.*, D.DepartmentName, DR.NameSurname
                        FROM Appointmets A
                        INNER JOIN Departments D ON A.DepartmentId = D.DepartmentId
                        INNER JOIN Doctors DR ON A.DoctorId = DR.DoctorId";
            return dbConnection.QueryAsync<ResultAppointmentDto>(query);
        }

        public async Task<ResultAppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var query = @"SELECT A.*, D.DepartmentName, DR.NameSurname
                        FROM Appointments A
                        INNER JOIN Departments D ON A.DepartmentId = D.DepartmentId
                        INNER JOIN Doctors DR ON A.DoctorId = DR.DoctorId
                        WHERE A.AppointmentId = @AppointmentId";
            var parameters = new { AppointmentId = id };
            var appointment = await dbConnection.QuerySingleOrDefaultAsync<ResultAppointmentDto>(query, parameters);
            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            return appointment;
        }
    }
}
