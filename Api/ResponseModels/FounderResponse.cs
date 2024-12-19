namespace Api.ResponseModels
{
    public class FounderResponse
    {
        public Guid Id { get; set; }

        public long Itn { get; set; }

        public string Fullname { get; set; }

        public DateTime CreatedAt { get; set; }

        public ClientResponse? Client { get; set; }

        public FounderResponse(
            Guid id,
            long itn,
            string fullname,
            DateTime createdAt,
            ClientResponse? client = null)
        {
            Id = id;
            Itn = itn;
            Fullname = fullname;
            CreatedAt = createdAt;
            Client = client;
        }
    }
}
