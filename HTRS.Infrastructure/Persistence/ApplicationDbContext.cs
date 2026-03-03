using HTRS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HTRS.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PatientProfile> PatientProfiles { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<FollowUpQuestion> FollowUpQuestions { get; set; }
        public DbSet<SymptomSession> SymptomSessions { get; set; }
        public DbSet<SessionSymptom> SessionSymptoms { get; set; }
        public DbSet<MedicalTest> MedicalTests { get; set; }
        public DbSet<TestRecommendation> TestRecommendations { get; set; }
    }
}