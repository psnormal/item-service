using ItemService.Models;
using System.ComponentModel.DataAnnotations;

namespace ItemService.Dto
{
    public class ApplItemsCreateDto
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
    }
}
