using Domain.Enums;

namespace Api.ResponseModels
{
    public class ClientResponse
    {
        public Guid Id { get; set; }

        public long Itn { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public ClientTypes ClientType { get; set; }

        public ICollection<FounderResponse> Founders { get; set; } = [];

        public ClientResponse(
            Guid id,
            long itn,
            string name,
            DateTime createdAt,
            ClientTypes clientType,
            ICollection<FounderResponse> founders)
        {
            Id = id;
            Itn = itn;
            Name = name;
            CreatedAt = createdAt;
            ClientType = clientType;
            Founders = founders;
        }
    }
}
