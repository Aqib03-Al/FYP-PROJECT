using HTRS.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HTRS.Application.DTOs
{
    public class PatientProfileDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string? MedicalHistory { get; set; }
        public string? ContactNumber { get; set; }
    }
}
