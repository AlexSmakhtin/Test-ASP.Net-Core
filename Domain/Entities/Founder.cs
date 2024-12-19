using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Founder : IEntity
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

        private string _fullname;
        public string Fullname
        {
            get => _fullname;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Fullname cannot be empty", nameof(value));
                _fullname = value;
            }
        }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Client? Client { get; set; }

        public Founder(long itn, string fullname)
        {
            Itn = itn;
            Fullname = fullname;
        }
    }
}
