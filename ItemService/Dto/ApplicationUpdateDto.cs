using System.ComponentModel.DataAnnotations;

namespace ItemService.Dto
{
    public class ApplicationUpdateDto
    {
        [Required]
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public List<ApplItemsCreateDto> ItemsInApplication { get; set; }
    }
}
