using HTRS.Domain.Common;
using HTRS.Domain.Enums;

namespace HTRS.Domain.Entities
{
    public class PatientProfile : BaseEntity
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string? MedicalHistory { get; set; }
        public string? ContactNumber { get; set; }
    }
}