using ApplicationTracker.Models;
using ApplicationTracker.DTOs;

namespace ApplicationTracker.Services
{

    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync(ApplicationStatus? statusFilter);
        Task<JobApplicationResponseDto?> GetByIdAsync(int id);
        Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationDto dto);
        Task<JobApplicationResponseDto?> UpdateAsync(int id, UpdateJobApplicationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}