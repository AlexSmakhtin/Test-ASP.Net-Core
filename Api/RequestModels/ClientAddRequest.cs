using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels
{
    public class ClientAddRequest
    {
        [Required]
        public long Itn { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public ClientTypes Type { get; set; }

        public List<Guid> FounderIds { get; set; } = [];
    }
}
