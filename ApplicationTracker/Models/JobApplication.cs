namespace ApplicationTracker.Models
{

    public enum ApplicationStatus
    {
        Applied,
        InterviewScheduled,
        OfferReceived,
        Rejected,
        Declined
    }

    public class JobApplication
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public required string JobTitle { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; }
    }
}