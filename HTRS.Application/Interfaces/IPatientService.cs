using HTRS.Domain.Entities;

namespace HTRS.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientProfile> GetPatientByUserIdAsync(string userId);
        Task CreatePatientProfileAsync(PatientProfile patient);
        Task UpdatePatientProfileAsync(PatientProfile patient);
    }
}