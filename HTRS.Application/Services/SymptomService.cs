using HTRS.Application.Interfaces;
using HTRS.Domain.Entities;

namespace HTRS.Application.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly IGenericRepository<Symptom> _symptomRepository;
        private readonly IGenericRepository<FollowUpQuestion> _followUpRepository;

        public SymptomService(
            IGenericRepository<Symptom> symptomRepository,
            IGenericRepository<FollowUpQuestion> followUpRepository)
        {
            _symptomRepository = symptomRepository;
            _followUpRepository = followUpRepository;
        }

        public async Task<IEnumerable<Symptom>> GetAllSymptomsAsync()
        {
            return await _symptomRepository.GetAllAsync();
        }

        public async Task<Symptom> GetSymptomByIdAsync(int id)
        {
            return await _symptomRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<FollowUpQuestion>> GetFollowUpQuestionsAsync(int symptomId)
        {
            var questions = await _followUpRepository.GetAllAsync();
            return questions.Where(q => q.SymptomId == symptomId);
        }

        public async Task AddSymptomAsync(Symptom symptom)
        {
            await _symptomRepository.AddAsync(symptom);
        }

        public async Task UpdateSymptomAsync(Symptom symptom)
        {
            await _symptomRepository.UpdateAsync(symptom);
        }

        public async Task DeleteSymptomAsync(int id)
        {
            await _symptomRepository.DeleteAsync(id);
        }
    }
}