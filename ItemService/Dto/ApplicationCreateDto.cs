using ItemService.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ItemService.Dto
{
    public class ApplicationCreateDto
    {
        [Required]
        public int OwnerUserId { get; set; }
        public string OwnerName { get; set; }
        [Required]
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public List<ApplItemsCreateDto> ItemsInApplication { get; set; }
    }
}
