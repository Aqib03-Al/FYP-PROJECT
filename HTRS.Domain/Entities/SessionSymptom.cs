using HTRS.Domain.Common;

namespace HTRS.Domain.Entities
{
    public class SessionSymptom : BaseEntity
    {
        public int SymptomSessionId { get; set; }
        public int SymptomId { get; set; }
        public string? FollowUpAnswers { get; set; }
        public int Duration { get; set; }
        public int Intensity { get; set; }

        // Navigation Properties
        public SymptomSession SymptomSession { get; set; }
        public Symptom Symptom { get; set; }
    }
}