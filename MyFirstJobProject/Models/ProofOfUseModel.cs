namespace MyFirstJobProject.Models
{
    public class ProofOfUseModel
    {
        public string ContactPerson { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string FileNumber { get; set; } = null!;
        public string Language { get; set; } = null!;
        public DateOnly Date { get; set; }
        public TimeSpan Arrival { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpan? Departure { get; set; }
        public string? Miscellaneous { get; set; }
        public string Signature { get; set; } = null!;
        public string Satisfaction { get; set; } = null!;

    }
}
