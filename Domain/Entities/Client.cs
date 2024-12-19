using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : IEntity
    {
        public Guid Id { get; init; }

        private long _itn;
        public long Itn
        {
            get => _itn;
            set
            {
                if (value.ToString().Length != 10 && value.ToString().Length != 12)
                {
                    throw new ArgumentException(
                        "ITN must be 10 or 12 length number",
                        nameof(value));
                }
                _itn = value;
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty", nameof(value));
                _name = value;
            }
        }

        private ClientTypes _type;
        public ClientTypes Type
        {
            get => _type;
            set => _type = value;
        }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Founder> Founders { get; set; } = [];

        public Client(long itn, string name, ClientTypes type)
        {
            Itn = itn;
            Name = name;
            Type = type;
        }
    }
}
