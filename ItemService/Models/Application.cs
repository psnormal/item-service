using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ItemService.Models
{
    public class Application
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public int OwnerUserId { get; set; }
        public string OwnerName { get; set; }
        [Required]
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public ApplicationState State { get; set; }
    }
}
