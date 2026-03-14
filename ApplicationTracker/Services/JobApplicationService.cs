using ApplicationTracker.Data;
using ApplicationTracker.Models;
using ApplicationTracker.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTracker.Services
{

    public class JobApplicationService(AppDbContext db) : IJobApplicationService
    {
        // ------------------------------------------------------------------
        // Read
        // ------------------------------------------------------------------

        public async Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync(ApplicationStatus? statusFilter)
        {
            var query = db.JobApplications.AsQueryable();

            if (statusFilter.HasValue)
                query = query.Where(a => a.Status == statusFilter.Value);

            var applications = await query
                .OrderByDescending(a => a.AppliedDate)
                .ToListAsync();

            return applications.Select(ToResponseDto);
        }

        public async Task<JobApplicationResponseDto?> GetByIdAsync(int id)
        {
            var application = await db.JobApplications.FindAsync(id);
            return application is null ? null : ToResponseDto(application);
        }

        // ------------------------------------------------------------------
        // Write
        // ------------------------------------------------------------------

        public async Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationDto dto)
        {
            var application = new JobApplication
            {
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                JobUrl = dto.JobUrl,
                Notes = dto.Notes,
                Status = dto.Status,
                AppliedDate = dto.AppliedDate
            };

            db.JobApplications.Add(application);
            await db.SaveChangesAsync();

            return ToResponseDto(application);
        }

        public async Task<JobApplicationResponseDto?> UpdateAsync(int id, UpdateJobApplicationDto dto)
        {
            var application = await db.JobApplications.FindAsync(id);
            if (application is null) return null;

            // Only overwrite fields the caller actually provided.
            if (dto.CompanyName is not null) application.CompanyName = dto.CompanyName;
            if (dto.JobTitle is not null) application.JobTitle = dto.JobTitle;
            if (dto.JobUrl is not null) application.JobUrl = dto.JobUrl;
            if (dto.Notes is not null) application.Notes = dto.Notes;
            if (dto.Status is not null) application.Status = dto.Status.Value;

            application.LastUpdated = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return ToResponseDto(application);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var application = await db.JobApplications.FindAsync(id);
            if (application is null) return false;

            db.JobApplications.Remove(application);
            await db.SaveChangesAsync();
            return true;
        }

        // ------------------------------------------------------------------
        // Mapping helper — keeps entity internals out of the response shape
        // ------------------------------------------------------------------

        private static JobApplicationResponseDto ToResponseDto(JobApplication a) => new()
        {
            Id = a.Id,
            CompanyName = a.CompanyName,
            JobTitle = a.JobTitle,
            JobUrl = a.JobUrl,
            Notes = a.Notes,
            Status = a.Status.ToString(),
            AppliedDate = a.AppliedDate,
            LastUpdated = a.LastUpdated
        };
    }
}