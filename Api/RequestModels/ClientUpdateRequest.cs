using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels
{
    public class ClientUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        public long? Itn { get; set; }

        public string? Name { get; set; }

        public ClientTypes? Type { get; set; }

        public List<Guid> FounderIds { get; set; } = [];
    }
}
