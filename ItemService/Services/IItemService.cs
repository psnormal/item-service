using ItemService.Dto;
using ItemService.Models;

namespace ItemService.Services
{
    public interface IItemService
    {
        Task<int> CreateItem(ItemCreateDto item);
        GetItemDto GetItemInfo(int id);
        GetItemsWithTypesDto GetItemsInfo();
        GetItemsDto GetItemsInfoByUser(int ownerUserId);
        GetItemsDto GetItemsInfoByType(ItemType type);
        Task UpdateItem(int id, ItemUpdateDto model);
        Task DeleteItem(int id);
    }
}
