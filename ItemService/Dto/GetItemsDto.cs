using ItemService.Models;

namespace ItemService.Dto
{
    public class GetItemsDto
    {
        public List<Item> Items { get; set; }
        public GetItemsDto(List<Item> items)
        {
            Items = items;
        }
    }
}
