namespace HTRS.Application.Interfaces
{
    public interface IAIService
    {
        Task<string> GetRecommendationAsync(string symptomsJson);
    }
}