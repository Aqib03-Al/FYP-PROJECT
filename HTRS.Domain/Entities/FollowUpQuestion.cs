using HTRS.Domain.Common;

namespace HTRS.Domain.Entities
{
    public class FollowUpQuestion : BaseEntity
    {
        public int SymptomId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string? Options { get; set; }

        // Navigation Property
        public Symptom Symptom { get; set; }
    }
}