using ItemService.Dto;

namespace ItemService.Services
{
    public interface IScheduleService
    {
        Task CreateItemSchedule(ScheduleDto schedule, int itemId);
        Task UpdateItemSchedule(ScheduleDto schedule, int itemId);
        Task DeleteItemSchedule(int id);
        ScheduleDto GetItemSchedule(int id);
    }
}
