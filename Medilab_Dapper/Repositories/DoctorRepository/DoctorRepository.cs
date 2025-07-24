using Dapper;
using Medilab_Dapper.Context;
using Medilab_Dapper.Dtos.DoctorDtos;
using System.Data;

namespace Medilab_Dapper.Repositories.DoctorRepository
{
    public class DoctorRepository(DapperContext context) : IDoctorRepository
    {
        private readonly IDbConnection dbConnection = context.CreateConnection();
        public async Task CreateDoctorAsync(CreateDoctorDto createDto)
        {
            var query = "INSERT INTO DOCTORS (NameSurname, ImageUrl, Description, DepartmentId) VALUES (@NameSurname, @ImageUrl, @Description, @DepartmentId)";
            var parameters = new DynamicParameters(createDto);
            await dbConnection.ExecuteAsync(query, parameters);
        }

        public Task DeleteDoctorAsync(int id)
        {
            var query = "DELETE FROM DOCTORS WHERE DoctorId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            return dbConnection.ExecuteAsync(query, parameters);
        }

        public Task<IEnumerable<ResultDoctorDto>> GetAllDoctorsAsync()
        {
            var query = "SELECT * FROM DOCTORS";
            return dbConnection.QueryAsync<ResultDoctorDto>(query);
        }

        public async Task<GetDoctorByIdDto> GetDoctorByIdAsync(int id)
        {
            var query = "SELECT * FROM DOCTORS WHERE DoctorId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            var doctor = await dbConnection.QueryFirstOrDefaultAsync<GetDoctorByIdDto>(query, parameters);
            if (doctor == null)
            {
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");
            }
            return doctor;
        }

        public Task<IEnumerable<ResultDoctorDto>> GetDoctorsByDepartmentIdAsync(int departmentId)
        {
            var query = "SELECT * FROM DOCTORS WHERE DepartmentId = @DepartmentId";
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", departmentId);
            return dbConnection.QueryAsync<ResultDoctorDto>(query, parameters);
        }

        public Task<IEnumerable<ResultDoctorWithDepartmentDto>> GetDoctorsWithDepartmentAsync()
        {
            var query = @"
                SELECT d.DoctorId, d.NameSurname, d.ImageUrl, d.Description, d.DepartmentId, 
                       dep.DepartmentName 
                FROM DOCTORS d
                INNER JOIN DEPARTMENTS dep ON d.DepartmentId = dep.DepartmentId";
            return dbConnection.QueryAsync<ResultDoctorWithDepartmentDto>(query);
        }

        public Task UpdateDoctorAsync(UpdateDoctorDto updateDto)
        {
            var query = "UPDATE DOCTORS SET NameSurname = @NameSurname, ImageUrl = @ImageUrl, Description = @Description, DepartmentId = @DepartmentId WHERE DoctorId = @DoctorId";
            var parameters = new DynamicParameters(updateDto);
            return dbConnection.ExecuteAsync(query, parameters);
        }
    }
}
