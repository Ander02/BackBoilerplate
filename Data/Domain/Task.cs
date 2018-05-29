using System;

namespace Data.Domain
{
    public class Task : IDomain
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        #region Navigation Props

        public virtual User User { get; set; }
        #endregion
    }
}
