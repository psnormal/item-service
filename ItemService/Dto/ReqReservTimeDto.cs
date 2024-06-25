namespace ItemService.Dto
{
    public class ReqReservTimeDto
    {
        public List<int> ItemsId { get; set; }
        public int Hours { get; set; }
        public DateOnly ApplicationDate { get; set; }
    }
}
