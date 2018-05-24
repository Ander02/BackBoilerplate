using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Features
{
    public class BaseFindManyQuery
    {
        public bool ShowDeleteds { get; set; } = false;
        public int Limit { get; set; } = 100;
        public int Page { get; set; } = 0;
    }
}
