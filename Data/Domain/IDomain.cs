using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain
{
    public interface IDomain
    {
        Guid Id { get; set; }

        DateTime DeletedAt { get; set; }
    }
}
