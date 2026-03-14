using ApplicationTracker.Models;

namespace ApplicationTracker.DTOs
{
    /// <summary>
    /// Used when submitting a new job application entry.
    /// </summary>
    public class CreateJobApplicationDto
    {
        public required string CompanyName { get; set; }
        public required string JobTitle { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Used when updating an existing application. All fields are optional
    /// so callers can patch only what changed.
    /// </summary>
    public class UpdateJobApplicationDto
    {
        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }
        public ApplicationStatus? Status { get; set; }
    }

    /// <summary>
    /// Response shape returned to callers
    /// </summary>
    public class JobApplicationResponseDto
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public required string JobTitle { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime AppliedDate { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}