using HTRS.Application.Interfaces;

namespace HTRS.Infrastructure.MockAI
{
    public class MockAIService : IAIService
    {
        public Task<string> GetRecommendationAsync(string symptomsJson)
        {
            var response = """
            {
                "severity": "Moderate",
                "recommended_tests": ["CBC", "Blood Sugar", "Urine Test"],
                "reason": "Based on your symptoms, these tests are recommended to rule out infection and metabolic issues."
            }
            """;

            return Task.FromResult(response);
        }
    }
}