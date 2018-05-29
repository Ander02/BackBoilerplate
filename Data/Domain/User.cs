using System;
using System.Collections.Generic;

namespace Data.Domain
{
    public class User : IDomain
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        #region Navigation Props
        public virtual ICollection<Task> Tasks { get; set; }
        #endregion
    }
}
