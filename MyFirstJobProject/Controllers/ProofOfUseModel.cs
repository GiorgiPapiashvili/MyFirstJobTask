namespace MyFirstJobProject.Controllers
{
    public class ProofOfUseModel
    {
        public string ContactPerson { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string FileNumber { get; set; } = null!;
        public string Language { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan? Arrival { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpan? Departure { get; set; }
        public string? Miscellaneous { get; set; }
        public string Signature { get; set; } = null!;
        public string Satisfaction { get; set; } = null!;

        public override string ToString()
        {
            string miscellaneousText = string.IsNullOrEmpty(Miscellaneous) ? "" : $"Miscellaneous - {Miscellaneous}";

            return $"ContactPerson - {ContactPerson}\t Location - {{Location}}";


                      //FileNumber - {FileNumber}
                      //Language - {Language}
                      //Date - {Date}
                      //Arrival - {Arrival}
                      //Start - {Start}
                      //End - {End}
                      //Departure - {Departure}
                      //{miscellaneousText}
                      //Signature - {Signature}
                      //Satisfaction - {Satisfaction}";
        }

    }
}
