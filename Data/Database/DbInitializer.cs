using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class DbInitializer
    {
        public static async Task Initialize(Db db)
        {
            await db.SaveChangesAsync();
        }
    }
}
