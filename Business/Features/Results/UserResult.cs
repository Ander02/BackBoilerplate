using System;
using System.Collections.Generic;

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
        }

        public class Full : Simple
        {
            public List<TaskResult.Simple> Tasks { get; set; } = new List<TaskResult.Simple>();
        }
    }
}
