namespace HTRS.Application.DTOs
{
    public class SymptomSubmitDto
    {
        public int PatientProfileId { get; set; }
        public List<SelectedSymptomDto> Symptoms { get; set; }
    }

    public class SelectedSymptomDto
    {
        public int SymptomId { get; set; }
        public string SymptomName { get; set; }
        public int Duration { get; set; }
        public int Intensity { get; set; }
        public List<FollowUpAnswerDto> Answers { get; set; }
    }

    public class FollowUpAnswerDto
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}