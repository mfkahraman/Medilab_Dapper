namespace Medilab_Dapper.Dtos.ContactMessageDtos
{
    public class ResultContactMessageDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Subject { get; set; }
        public required string Message { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
