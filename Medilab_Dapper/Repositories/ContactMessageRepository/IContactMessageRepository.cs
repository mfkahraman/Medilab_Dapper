using Medilab_Dapper.Dtos.AppointmentDtos;
using Medilab_Dapper.Dtos.ContactMessageDtos;

namespace Medilab_Dapper.Repositories.ContactMessageRepository
{
    public interface IContactMessageRepository
    {
        Task<IEnumerable<ResultContactMessageDto>> GetAllAsync();
        Task<ResultContactMessageDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateContactMessageDto dto);
        Task<bool> MakeReadAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
