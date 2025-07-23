using Dapper;
using Medilab_Dapper.Context;
using Medilab_Dapper.Dtos.DepartmentDtos;

namespace Medilab_Dapper.Repositories.DepartmentRepository
{
    public class DepartmentRepository(DapperContext context) : IDepartmentRepository
    {
        public async Task CreateDepartmentAsync(CreateDepartmentDto createDto)
        {
            string query = "INSERT INTO DEPARTMENTS (DepartmentName, Description) VALUES (@DepartmentName, @Description)";
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentName", createDto.DepartmentName);
            parameters.Add("Description", createDto.Description);
            var connection = context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            string query = "DELETE FROM Departments WHERE DepartmentId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            var connection = context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<ResultDepartmentDto>> GetAllDepartmentsAsync()
        {
            string query = "SELECT * FROM Departments";
            var connection = context.CreateConnection();
            return await connection.QueryAsync<ResultDepartmentDto>(query);
        }

        public async Task<GetDepartmentByIdDto> GetDepartmentByIdAsync(int id)
        {
            string query = "SELECT * FROM Departments WHERE DepartmentId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            var connection = context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<GetDepartmentByIdDto>(query, parameters);

            if (result == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }

            return result;
        }

        public async Task UpdateDepartmentAsync(UpdateDepartmentDto updateDto)
        {
            string query = "UPDATE Departments SET DepartmentName = @DepartmentName, Description = @Description WHERE DepartmentId = @DepartmentId";
            //Tek tek parametreleri eklemek yerine parametre isimleri dto parametresinin propları ile eşleşiyorsa DynamicParamters içine gönderebilirsin
            var parameters = new DynamicParameters(updateDto);
            var connection = context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
