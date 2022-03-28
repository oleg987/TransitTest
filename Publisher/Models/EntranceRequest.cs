using System.Text.Json;

namespace Publisher.Models
{
    public class EntranceRequest
    {
        private DateTime _updated;
        private int _status;

        public Guid Id { get; }
        public string Name { get; }
        public int Status { get => _status; 
            set 
            {
                _status = value;
                _updated = DateTime.Now;
            }}
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get => _updated; }

        public EntranceRequest(string name, int status)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = status;
            CreatedAt = DateTime.Now;
            _updated = DateTime.Now;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
