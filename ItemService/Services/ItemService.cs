﻿using ItemService.Dto;
using ItemService.Models;
using System.ComponentModel.DataAnnotations;

namespace ItemService.Services
{
    public class ItemsService : IItemService
    {
        private readonly ApplicationDbContext _context;
        private IScheduleService _scheduleService;

        public ItemsService(ApplicationDbContext context)
        {
            _context = context;
            _scheduleService = new ScheduleService(context);
        }

        public async Task<int> CreateItem(ItemCreateDto item)
        {
            var newItem = new Item
            {
                OwnerUserId = item.OwnerUserId,
                OwnerName = item.OwnerName,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                CostPerHour = item.CostPerHour,
                ItemType = item.ItemType,
                Pictures = item.Pictures
            };

            await _context.Items.AddAsync(newItem);
            await _context.SaveChangesAsync();

            await _scheduleService.CreateItemSchedule(item.Schedule, newItem.Id);

            return newItem.Id;
        }

        public GetItemDto GetItemInfo(int id)
        {
            var itemInfo = _context.Items.FirstOrDefault(c => c.Id == id);

            if (itemInfo == null)
            {
                throw new ValidationException("This item does not exist");
            }

            GetItemDto item = new GetItemDto
            {
                Id = itemInfo.Id,
                OwnerUserId = itemInfo.OwnerUserId,
                OwnerName = itemInfo.OwnerName,
                ItemName = itemInfo.ItemName,
                ItemDescription = itemInfo.ItemDescription,
                CostPerHour = itemInfo.CostPerHour,
                ItemType = itemInfo.ItemType,
                Pictures = itemInfo.Pictures,
                Schedule = _scheduleService.GetItemSchedule(id)
        };

            return item;
        }

        public GetItemsWithTypesDto GetItemsInfo()
        {
            GetItemsWithTypesDto itemsList = new GetItemsWithTypesDto {
                ClothesAndShoes = GetItemsInfoByType(ItemType.ClothesAndShoes),
                Requisite = GetItemsInfoByType(ItemType.Requisite),
                Equipment = GetItemsInfoByType(ItemType.Equipment),
                Place = GetItemsInfoByType(ItemType.Place),
                Photo = GetItemsInfoByType(ItemType.Photo),
                Video = GetItemsInfoByType(ItemType.Video),
                Makeup = GetItemsInfoByType(ItemType.Makeup),
                Other = GetItemsInfoByType(ItemType.Other)
            };
            return itemsList;
        }

        public GetItemsDto GetItemsInfoByUser(int ownerUserId)
        {
            var items = _context.Items.Where(c => c.OwnerUserId == ownerUserId).ToList();
            GetItemsDto itemsList = new GetItemsDto(items);
            return itemsList;
        }

        public GetItemsDto GetItemsInfoByType(ItemType type)
        {
            var items = _context.Items.Where(c => c.ItemType == type).ToList();
            GetItemsDto itemsList = new GetItemsDto(items);
            return itemsList;
        }

        public async Task UpdateItem(int id, ItemUpdateDto model)
        {
            var itemInfo = _context.Items.FirstOrDefault(c => c.Id == id);

            if (itemInfo == null)
            {
                throw new ValidationException("This item does not exist");
            }

            itemInfo.ItemName = model.ItemName;
            itemInfo.ItemDescription = model.ItemDescription;
            itemInfo.ItemType = model.ItemType;
            itemInfo.CostPerHour = model.CostPerHour;
            itemInfo.Pictures = model.Pictures;

            await _context.SaveChangesAsync();

            await _scheduleService.UpdateItemSchedule(model.Schedule, id);
        }

        public async Task DeleteItem(int id)
        {
            var itemInfo = _context.Items.FirstOrDefault(c => c.Id == id);

            if (itemInfo == null)
            {
                throw new ValidationException("This item does not exist");
            }

            _context.Items.Remove(itemInfo);
            await _context.SaveChangesAsync();
            await _scheduleService.DeleteItemSchedule(id);
        }
    }
}
