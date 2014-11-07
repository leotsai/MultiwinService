using System;

namespace MultiwinService.Core.Data
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime? LastUpdatedTime { get; set; }

        public EntityBase()
        {
            this.Id = Guid.Empty;
            this.CreatedTime = DateTime.Now;
        }

        public void NewId()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
