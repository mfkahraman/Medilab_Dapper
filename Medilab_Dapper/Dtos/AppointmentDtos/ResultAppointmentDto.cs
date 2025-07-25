namespace Medilab_Dapper.Dtos.AppointmentDtos
{
    public class ResultAppointmentDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
        public string? Message { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string DoctorNameSurname { get; set; } = string.Empty;

    }
}
