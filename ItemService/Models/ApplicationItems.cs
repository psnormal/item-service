using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class ApplicationItems
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public ApplicationItemStates State { get; set; }
    }
}
