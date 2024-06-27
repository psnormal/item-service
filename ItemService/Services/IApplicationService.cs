using ItemService.Dto;
using ItemService.Models;

namespace ItemService.Services
{
    public interface IApplicationService
    {
        Task<int> CreateApplication(ApplicationCreateDto appl);
        List<ApplicationItems> GetAllItemsInAppl(int applId);
        GetApplDto GetApplication(int applId);
        List<GetApplDto> GetApplicationsOwnedByUser(int UserId);
        List<GetApplDto> GetIncomingApplications(int UserId);
        Task UpdateApplication(int id, ApplicationUpdateDto model);
        Task DeleteApplication(int id);
        Task ChangeItemInApplState(ApplicationItemStates state, int id);
        Task ChangeTimesItemInApplState(ResReservTimeDto times, int id);
        List<ResReservTimeDto> FindTimeForAppl(ReqReservTimeDto model);
    }
}
