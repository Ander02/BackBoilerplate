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
        }

        public class Full : Simple
        {
            public UserResult.Simple User { get; set; }
        }
    }
}
