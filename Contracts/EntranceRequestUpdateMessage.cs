using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contracts
{
    public class EntranceRequestUpdateMessage
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Status { get; }
        public DateTime Created { get; }
        public DateTime Updated { get; }

        public EntranceRequestUpdateMessage(Guid id, string name, int status, DateTime created, DateTime updated)
        {
            Id = id;
            Name = name;
            Status = status;
            Created = created;
            Updated = updated;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
