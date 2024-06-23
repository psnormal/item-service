using ItemService.Dto;
using ItemService.Models;
using ItemService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        [Route("item/create")]
        public async Task<ActionResult<GetItemDto>> CreateItem(ItemCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var ItemId = await _itemService.CreateItem(model);
                return GetItemInfo(ItemId); //метод получения инфы
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("item/info/{id}")]
        public ActionResult<GetItemDto> GetItemInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _itemService.GetItemInfo(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This item does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("items")]
        public ActionResult<GetItemsWithTypesDto> GetAllItems()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _itemService.GetItemsInfo();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("items/{type}")]
        public ActionResult<GetItemsDto> GetItemsByType(ItemType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _itemService.GetItemsInfoByType(type);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("user/{ownerId}/items")]
        public ActionResult<GetItemsDto> GetItemsByOwner(int ownerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _itemService.GetItemsInfoByUser(ownerId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut]
        [Route("item/edit/{id}")]
        public async Task<ActionResult<GetItemDto>> UpdateItem(int id, ItemUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _itemService.UpdateItem(id, model);
                return GetItemInfo(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This item does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("item/delete/{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _itemService.DeleteItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This item does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
