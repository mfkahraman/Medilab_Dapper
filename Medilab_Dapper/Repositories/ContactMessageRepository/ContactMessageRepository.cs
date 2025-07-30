using Dapper;
using Medilab_Dapper.Context;
using Medilab_Dapper.Dtos.ContactMessageDtos;
using System.Data;

namespace Medilab_Dapper.Repositories.ContactMessageRepository
{
    public class ContactMessageRepository(DapperContext context) : IContactMessageRepository
    {
        private readonly IDbConnection dbConnection = context.CreateConnection();
        public async Task<bool> CreateAsync(CreateContactMessageDto dto)
        {
            var query = @"INSERT INTO ContactMessages (FullName, Email, Subject, Message, IsRead, IsDeleted, CreateDate) 
                         VALUES (@FullName, @Email, @Subject, @Message, @IsRead, @IsDeleted, @CreateDate)";
            var parameters = new DynamicParameters(dto);
            parameters.Add("CreateDate", DateTime.UtcNow);
            parameters.Add("IsRead", false);
            parameters.Add("IsDeleted", false);
            int result = await dbConnection.ExecuteAsync(query, parameters);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "UPDATE ContactMessages SET IsDeleted = 1 WHERE Id = @Id";
            var parameters = new { Id = id };
            var result = await dbConnection.ExecuteAsync(query, parameters);
            return result > 0;
        }

        public async Task<IEnumerable<ResultContactMessageDto>> GetAllAsync()
        {
            var query = @"SELECT * FROM ContactMessages 
                         WHERE IsDeleted = 0 
                         ORDER BY CreateDate DESC";
            return await dbConnection.QueryAsync<ResultContactMessageDto>(query);
        }

        public async Task<ResultContactMessageDto> GetByIdAsync(int id)
        {
            var query = @"SELECT * FROM ContactMessages 
                         WHERE Id = @Id AND IsDeleted = 0";
            var parameters = new { Id = id };
            var contactMessage = await dbConnection.QueryFirstOrDefaultAsync<ResultContactMessageDto>(query, parameters);
            if (contactMessage == null)
            {
                throw new KeyNotFoundException($"Contact message with ID {id} not found.");
            }
            return contactMessage;
        }

        public async Task<bool> MakeReadAsync(int id)
        {
            var query = "UPDATE ContactMessages SET IsRead = 1 WHERE Id = @Id AND IsDeleted = 0";
            var parameters = new { Id = id };
            var result = await dbConnection.ExecuteAsync(query, parameters);
            return result > 0;
        }
    }
}
