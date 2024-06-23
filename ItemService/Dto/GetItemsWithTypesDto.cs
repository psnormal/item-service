using ItemService.Models;

namespace ItemService.Dto
{
    public class GetItemsWithTypesDto
    {
        public GetItemsDto ClothesAndShoes { get; set; }
        public GetItemsDto Requisite { get; set; }
        public GetItemsDto Equipment { get; set; }
        public GetItemsDto Place { get; set; }
        public GetItemsDto Photo { get; set; }
        public GetItemsDto Video { get; set; }
        public GetItemsDto Makeup { get; set; }
        public GetItemsDto Other { get; set; }
    }
}
