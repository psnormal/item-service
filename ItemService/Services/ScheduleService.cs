using ItemService.Dto;
using ItemService.Models;
using static Npgsql.PostgresTypes.PostgresCompositeType;
using System.Reflection.Metadata;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace ItemService.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateItemSchedule(ScheduleDto schedule, int itemId)
        {
            var newSchedule = new Schedule{
                ItemId = itemId,
                MonStart = (schedule?.MonStart != null) ? TimeOnly.Parse(schedule.MonStart) : null,
                MonEnd = (schedule?.MonEnd != null) ? TimeOnly.Parse(schedule.MonEnd) : null,
                TueStart = (schedule?.TueStart != null) ? TimeOnly.Parse(schedule.TueStart) : null,
                TueEnd = (schedule?.TueEnd != null) ? TimeOnly.Parse(schedule.TueEnd) : null,
                WedStart = (schedule?.WedStart != null) ? TimeOnly.Parse(schedule.WedStart) : null,
                WedEnd = (schedule?.WedEnd != null) ? TimeOnly.Parse(schedule.WedEnd) : null,
                ThuStart = (schedule?.ThuStart != null) ? TimeOnly.Parse(schedule.ThuStart) : null,
                ThuEnd = (schedule?.ThuEnd != null) ? TimeOnly.Parse(schedule.ThuEnd) : null,
                FriStart = (schedule?.FriStart != null) ? TimeOnly.Parse(schedule.FriStart) : null,
                FriEnd = (schedule?.FriEnd != null) ? TimeOnly.Parse(schedule.FriEnd) : null,
                SatStart = (schedule?.SatStart != null) ? TimeOnly.Parse(schedule.SatStart) : null,
                SatEnd = (schedule?.SatEnd != null) ? TimeOnly.Parse(schedule.SatEnd) : null,
                SunStart = (schedule?.SunStart != null) ? TimeOnly.Parse(schedule.SunStart) : null,
                SunEnd = (schedule?.SunEnd != null) ? TimeOnly.Parse(schedule.SunEnd) : null
            };

            await _context.ScheduleItems.AddAsync(newSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemSchedule(ScheduleDto schedule, int itemId)
        {
            var itemSchedule = _context.ScheduleItems.FirstOrDefault(c => c.ItemId == itemId);

            if (itemSchedule == null)
            {
                throw new ValidationException("This item does not exist");
            }

            itemSchedule.MonStart = (schedule?.MonStart != null) ? TimeOnly.Parse(schedule.MonStart) : null;
            itemSchedule.MonEnd = (schedule?.MonEnd != null) ? TimeOnly.Parse(schedule.MonEnd) : null;
            itemSchedule.TueStart = (schedule?.TueStart != null) ? TimeOnly.Parse(schedule.TueStart) : null;
            itemSchedule.TueEnd = (schedule?.TueEnd != null) ? TimeOnly.Parse(schedule.TueEnd) : null;
            itemSchedule.WedStart = (schedule?.WedStart != null) ? TimeOnly.Parse(schedule.WedStart) : null;
            itemSchedule.WedEnd = (schedule?.WedEnd != null) ? TimeOnly.Parse(schedule.WedEnd) : null;
            itemSchedule.ThuStart = (schedule?.ThuStart != null) ? TimeOnly.Parse(schedule.ThuStart) : null;
            itemSchedule.ThuEnd = (schedule?.ThuEnd != null) ? TimeOnly.Parse(schedule.ThuEnd) : null;
            itemSchedule.FriStart = (schedule?.FriStart != null) ? TimeOnly.Parse(schedule.FriStart) : null;
            itemSchedule.FriEnd = (schedule?.FriEnd != null) ? TimeOnly.Parse(schedule.FriEnd) : null;
            itemSchedule.SatStart = (schedule?.SatStart != null) ? TimeOnly.Parse(schedule.SatStart) : null;
            itemSchedule.SatEnd = (schedule?.SatEnd != null) ? TimeOnly.Parse(schedule.SatEnd) : null;
            itemSchedule.SunStart = (schedule?.SunStart != null) ? TimeOnly.Parse(schedule.SunStart) : null;
            itemSchedule.SunEnd = (schedule?.SunEnd != null) ? TimeOnly.Parse(schedule.SunEnd) : null;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemSchedule(int id)
        {
            var itemSchedule = _context.ScheduleItems.FirstOrDefault(c => c.ItemId == id);

            if (itemSchedule == null)
            {
                throw new ValidationException("This item does not exist");
            }

            _context.ScheduleItems.Remove(itemSchedule);
            await _context.SaveChangesAsync();
        }

        public ScheduleDto GetItemSchedule(int id)
        {
            var itemSchedule = _context.ScheduleItems.FirstOrDefault(c => c.ItemId == id);

            if (itemSchedule == null)
            {
                throw new ValidationException("This item does not exist");
            }

            ScheduleDto schedule = new ScheduleDto
            {
                MonStart = itemSchedule.MonStart.ToString(),
                MonEnd = itemSchedule.MonEnd.ToString(),
                TueStart = itemSchedule.TueStart.ToString(),
                TueEnd = itemSchedule.TueEnd.ToString(),
                WedStart = itemSchedule.WedStart.ToString(),
                WedEnd = itemSchedule.WedEnd.ToString(),
                ThuStart = itemSchedule.ThuStart.ToString(),
                ThuEnd = itemSchedule.ThuEnd.ToString(),
                FriStart = itemSchedule.FriStart.ToString(),
                FriEnd = itemSchedule.FriEnd.ToString(),
                SatStart = itemSchedule.SatStart.ToString(),
                SatEnd = itemSchedule.SatEnd.ToString(),
                SunStart = itemSchedule.SunStart.ToString(),
                SunEnd = itemSchedule.SunEnd.ToString()
            };

            return schedule;
        }
    }
}
