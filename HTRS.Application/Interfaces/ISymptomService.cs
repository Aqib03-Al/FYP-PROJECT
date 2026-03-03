using HTRS.Domain.Entities;

namespace HTRS.Application.Interfaces
{
    public interface ISymptomService
    {
        Task<IEnumerable<Symptom>> GetAllSymptomsAsync();
        Task<Symptom> GetSymptomByIdAsync(int id);
        Task<IEnumerable<FollowUpQuestion>> GetFollowUpQuestionsAsync(int symptomId);
        Task AddSymptomAsync(Symptom symptom);
        Task UpdateSymptomAsync(Symptom symptom);
        Task DeleteSymptomAsync(int id);
    }
}