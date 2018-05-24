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
