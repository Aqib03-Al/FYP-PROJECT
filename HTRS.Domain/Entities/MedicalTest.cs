using HTRS.Domain.Common;

namespace HTRS.Domain.Entities
{
    public class MedicalTest : BaseEntity
    {
        public string TestName { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Category { get; set; }
    }
}