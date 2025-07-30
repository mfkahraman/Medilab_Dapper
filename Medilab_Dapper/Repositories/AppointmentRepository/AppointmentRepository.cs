using Dapper;
using Medilab_Dapper.Context;
using Medilab_Dapper.Dtos.AppointmentDtos;
using System.Data;

namespace Medilab_Dapper.Repositories.AppointmentRepository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DapperContext _context;

        public AppointmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId)
        {
            var query = "UPDATE Appointments SET IsActive = 0, IsConfirmed = 0 WHERE Id = @AppointmentId";
            var parameters = new { AppointmentId = appointmentId };
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, parameters);
            return result > 0;
        }

        public async Task<bool> ConfirmAppointmentAsync(int appointmentId)
        {
            var query = "UPDATE Appointments SET IsConfirmed = 1 WHERE Id = @AppointmentId";
            var parameters = new { AppointmentId = appointmentId };
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, parameters);
            return result > 0;
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

            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, parameters);
            return result > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            var query = "DELETE FROM Appointments WHERE Id = @AppointmentId";
            var parameters = new { AppointmentId = appointmentId };
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, parameters);
            return result > 0;
        }

        public async Task<IEnumerable<ResultAppointmentDto>> GetAllAppointmentsAsync()
        {
            var query = @" SELECT A.*, D.DepartmentName, DR.NameSurname
                        FROM Appointments A
                        INNER JOIN Departments D ON A.DepartmentId = D.DepartmentId
                        INNER JOIN Doctors DR ON A.DoctorId = DR.DoctorId
                        ORDER BY A.AppointmentDate ASC";

            using var connection = _context.CreateConnection();
            var appointments = await connection.QueryAsync<ResultAppointmentDto>(query);
            return appointments;
        }

        public async Task<ResultAppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var query = @"SELECT A.*, D.DepartmentName, DR.NameSurname
                        FROM Appointments A
                        INNER JOIN Departments D ON A.DepartmentId = D.DepartmentId
                        INNER JOIN Doctors DR ON A.DoctorId = DR.DoctorId
                        WHERE A.Id = @AppointmentId";

            var parameters = new { AppointmentId = id };
            using var connection = _context.CreateConnection();
            var appointment = await connection.QuerySingleOrDefaultAsync<ResultAppointmentDto>(query, parameters);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            return appointment;
        }
    }
}
