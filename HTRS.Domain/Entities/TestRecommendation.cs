using HTRS.Domain.Common;
using HTRS.Domain.Enums;

namespace HTRS.Domain.Entities
{
    public class TestRecommendation : BaseEntity
    {
        public int SymptomSessionId { get; set; }
        public int MedicalTestId { get; set; }
        public string Reason { get; set; }
        public RecommendationStatus Status { get; set; } = RecommendationStatus.Pending;

        // Navigation Properties
        public SymptomSession SymptomSession { get; set; }
        public MedicalTest MedicalTest { get; set; }
    }
}