﻿using ItemService.Models;
using System.ComponentModel.DataAnnotations;

namespace ItemService.Dto
{
    public class GetItemDto
    {
        public int Id { get; set; }
        public Guid OwnerUserId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int CostPerDay { get; set; }
        public int CostPerHour { get; set; }
        public ItemType ItemType { get; set; }
    }
}
