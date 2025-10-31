
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbClient
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DBClient _context;

        public DatabaseInitializer(DBClient context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.Migrate();
        }
    }
}
