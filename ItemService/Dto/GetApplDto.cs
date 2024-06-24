using ItemService.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ItemService.Dto
{
    public class GetApplDto
    {
        public int Id { get; set; }
        public int OwnerUserId { get; set; }
        public string OwnerName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public ApplicationState State { get; set; }
        public List<ApplicationItems> ItemsInApplication { get; set; }
    }
}
