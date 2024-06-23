using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ItemService.Models
{
    public class Schedule
    {
        [Required]
        [Key]
        public int ItemId { get; set; }
        [AllowNull]
        public TimeOnly? MonStart { get; set; }
        public TimeOnly? MonEnd { get; set; }
        public TimeOnly? TueStart { get; set; }
        public TimeOnly? TueEnd { get; set; }
        public TimeOnly? WedStart { get; set; }
        public TimeOnly? WedEnd { get; set; }
        public TimeOnly? ThuStart { get; set; }
        public TimeOnly? ThuEnd { get; set; }
        public TimeOnly? FriStart { get; set; }
        public TimeOnly? FriEnd { get; set; }
        public TimeOnly? SatStart { get; set; }
        public TimeOnly? SatEnd { get; set; }
        public TimeOnly? SunStart { get; set; }
        public TimeOnly? SunEnd { get; set; }
    }
}
