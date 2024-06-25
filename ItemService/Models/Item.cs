using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Item
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public int OwnerUserId { get; set; }
        public string OwnerName { get; set; }
        [Required]
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int CostPerHour { get; set; }
        public ItemType ItemType { get; set; }
        public string Pictures { get; set; }
    }
}
