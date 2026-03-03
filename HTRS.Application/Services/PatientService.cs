using HTRS.Application.Interfaces;
using HTRS.Domain.Entities;
//using HTRS.Infrastructure.Persistence.Repositories;

namespace HTRS.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IGenericRepository<PatientProfile> _repository;

        public PatientService(IGenericRepository<PatientProfile> repository)
        {
            _repository = repository;
        }

        public async Task<PatientProfile> GetPatientByUserIdAsync(string userId)
        {
            var patients = await _repository.GetAllAsync();
            return patients.FirstOrDefault(p => p.UserId == userId);
        }

        public async Task CreatePatientProfileAsync(PatientProfile patient)
        {
            await _repository.AddAsync(patient);
        }

        public async Task UpdatePatientProfileAsync(PatientProfile patient)
        {
            await _repository.UpdateAsync(patient);
        }
    }
}