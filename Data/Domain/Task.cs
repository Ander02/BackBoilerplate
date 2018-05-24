using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        #region Navigation Props

        public virtual User User { get; set; }
        #endregion
    }
}
