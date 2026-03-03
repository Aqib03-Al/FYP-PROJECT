using HTRS.Application.Interfaces;
using HTRS.Domain.Entities;
using HTRS.Domain.Enums;
using System.Text.Json;

namespace HTRS.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IGenericRepository<SymptomSession> _sessionRepository;
        private readonly IGenericRepository<TestRecommendation> _recommendationRepository;
        private readonly IGenericRepository<MedicalTest> _medicalTestRepository;
        private readonly IAIService _aiService;

        public RecommendationService(
            IGenericRepository<SymptomSession> sessionRepository,
            IGenericRepository<TestRecommendation> recommendationRepository,
            IGenericRepository<MedicalTest> medicalTestRepository,
            IAIService aiService)
        {
            _sessionRepository = sessionRepository;
            _recommendationRepository = recommendationRepository;
            _medicalTestRepository = medicalTestRepository;
            _aiService = aiService;
        }

        public async Task<SymptomSession> CreateSessionAsync(int patientProfileId)
        {
            var session = new SymptomSession
            {
                PatientProfileId = patientProfileId,
                SeverityLevel = SeverityLevel.Mild,
                SessionDate = DateTime.UtcNow
            };
            await _sessionRepository.AddAsync(session);
            return session;
        }

        public async Task<SymptomSession> GetSessionByIdAsync(int sessionId)
        {
            return await _sessionRepository.GetByIdAsync(sessionId);
        }

        public async Task<IEnumerable<TestRecommendation>> GetRecommendationsAsync(int sessionId)
        {
            var recommendations = await _recommendationRepository.GetAllAsync();
            return recommendations.Where(r => r.SymptomSessionId == sessionId);
        }

        public async Task ProcessRecommendationsAsync(int sessionId, string symptomsJson)
        {
            var aiResponse = await _aiService.GetRecommendationAsync(symptomsJson);

            var response = JsonSerializer.Deserialize<JsonElement>(aiResponse);
            var severity = response.GetProperty("severity").GetString();
            var tests = response.GetProperty("recommended_tests").EnumerateArray();
            var reason = response.GetProperty("reason").GetString();

            var session = await _sessionRepository.GetByIdAsync(sessionId);
            session.SeverityLevel = severity == "Severe" ? SeverityLevel.Severe
                                  : severity == "Moderate" ? SeverityLevel.Moderate
                                  : SeverityLevel.Mild;
            session.AIResponse = aiResponse;
            await _sessionRepository.UpdateAsync(session);

            var allTests = await _medicalTestRepository.GetAllAsync();
            foreach (var test in tests)
            {
                var testName = test.GetString();
                var medicalTest = allTests.FirstOrDefault(t => t.TestName == testName);
                if (medicalTest != null)
                {
                    await _recommendationRepository.AddAsync(new TestRecommendation
                    {
                        SymptomSessionId = sessionId,
                        MedicalTestId = medicalTest.Id,
                        Reason = reason,
                        Status = RecommendationStatus.Pending
                    });
                }
            }
        }

        public async Task<IEnumerable<SymptomSession>> GetPatientSessionsAsync(int patientProfileId)
        {
            var sessions = await _sessionRepository.GetAllAsync();
            return sessions.Where(s => s.PatientProfileId == patientProfileId);
        }
    }
}