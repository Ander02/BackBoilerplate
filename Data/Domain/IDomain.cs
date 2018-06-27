using System;

namespace Data.Domain
{
    public interface IDomain
    {
        Guid Id { get; set; }
        DateTime DeletedAt { get; set; }
    }
}
