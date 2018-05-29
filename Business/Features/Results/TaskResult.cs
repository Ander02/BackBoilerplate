using Data.Domain;
using System;

namespace Business.Features.Results
{
    public class TaskResult
    {
        public class Simple
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? CompletedAt { get; set; }
            public DateTime? DeletedAt { get; set; }

            //public Simple() { }

            //public Simple(Task task)
            //{
            //    this.Id = task.Id;
            //    this.Name = task.Name;
            //    this.Description = task.Description;
            //    this.CreatedAt = task.CreatedAt;
            //    this.CompletedAt = task.CompletedAt;
            //    this.DeletedAt = task.CompletedAt;
            //}
        }

        public class Full : Simple
        {
            public UserResult.Simple User { get; set; }

            //public Full() { }

            //public Full(Task task) : base(task)
            //{
            //    this.User = task.User != null ? new UserResult.Simple(task.User) : null;
            //}
        }
    }
}
