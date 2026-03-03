using HTRS.Domain.Common;

namespace HTRS.Domain.Entities
{
    public class Symptom : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BodyPart { get; set; }
    }
}