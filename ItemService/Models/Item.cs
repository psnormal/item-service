using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Item
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid OwnerUserId { get; set; }
        [Required]
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int CostPerDay { get; set; }
        public int CostPerHour { get; set; }
        public ItemType ItemType { get; set; }
    }
}
