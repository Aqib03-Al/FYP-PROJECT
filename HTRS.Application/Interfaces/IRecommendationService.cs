using HTRS.Domain.Entities;

namespace HTRS.Application.Interfaces
{
    public interface IRecommendationService
    {
        Task<SymptomSession> CreateSessionAsync(int patientProfileId);
        Task<IEnumerable<TestRecommendation>> GetRecommendationsAsync(int sessionId);
        Task ProcessRecommendationsAsync(int sessionId, string symptomsJson);
        Task<IEnumerable<SymptomSession>> GetPatientSessionsAsync(int patientProfileId);
    }
}