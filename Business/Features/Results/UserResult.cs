using Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Features.Results
{
    public class UserResult
    {
        public class Simple
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int? Age { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? DeletedAt { get; set; } = null;

            public Simple() { }

            public Simple(User user)
            {
                this.Id = user.Id;
                this.Name = user.Name;
                this.Password = user.Password;
                this.Email = user.Email;
                this.Age = user.Age;
                this.CreatedAt = user.CreatedAt;
                this.DeletedAt = user.DeletedAt;
            }
        }

        public class Full : Simple
        {
            public List<TaskResult.Simple> Tasks { get; set; } = new List<TaskResult.Simple>();

            public Full() { }

            public Full(User user) : base(user)
            {
                this.Tasks = user.Tasks?.Select(t => new TaskResult.Simple(t)).ToList();
            }
        }
    }
}
