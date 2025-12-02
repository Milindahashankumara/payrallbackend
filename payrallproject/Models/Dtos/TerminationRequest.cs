namespace payrallproject.Models.Dtos
{
    public class TerminationRequest
    {
        public DateTime TerminationDate { get; set; }
        public string? Reason { get; set; }
    }
}
