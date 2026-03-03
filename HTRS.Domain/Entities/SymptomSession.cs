using HTRS.Domain.Common;
using HTRS.Domain.Enums;

namespace HTRS.Domain.Entities
{
    public class SymptomSession : BaseEntity
    {
        public int PatientProfileId { get; set; }
        public SeverityLevel SeverityLevel { get; set; }
        public string? AIResponse { get; set; }
        public DateTime SessionDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public PatientProfile PatientProfile { get; set; }
    }
}