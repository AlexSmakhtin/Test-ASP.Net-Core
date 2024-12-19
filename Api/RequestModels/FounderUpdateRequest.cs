using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels
{
    public class FounderUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        public long? Itn { get; set; }

        public string? Fullname { get; set; }

        public Guid? ClientId { get; set; }
    }
}
