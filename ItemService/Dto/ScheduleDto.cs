namespace ItemService.Dto
{
    public class ScheduleDto
    {
        public TimeOnly MonStart { get; set; }
        public TimeOnly MonEnd { get; set; }
        public TimeOnly TueStart { get; set; }
        public TimeOnly TueEnd { get; set; }
        public TimeOnly WedStart { get; set; }
        public TimeOnly WedEnd { get; set; }
        public TimeOnly ThuStart { get; set; }
        public TimeOnly ThuEnd { get; set; }
        public TimeOnly FriStart { get; set; }
        public TimeOnly FriEnd { get; set; }
        public TimeOnly SatStart { get; set; }
        public TimeOnly SatEnd { get; set; }
        public TimeOnly SunStart { get; set; }
        public TimeOnly SunEnd { get; set; }
    }
}
