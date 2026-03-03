using HTRS.Domain.Enums;

namespace HTRS.Application.DTOs
{
    public class SymptomSessionDto
    {
        public int Id { get; set; }
        public int PatientProfileId { get; set; }
        public string PatientName { get; set; }
        public SeverityLevel SeverityLevel { get; set; }
        public DateTime SessionDate { get; set; }
        public List<TestRecommendationDto> Recommendations { get; set; }
    }

    public class TestRecommendationDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Category { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}