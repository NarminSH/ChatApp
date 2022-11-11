using System;

namespace Infrastructure.Repositories.Implementation
{
    public class ConnectionRepository : GenericRepository<Connection>, IConnectionRepository
    {
        public ConnectionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

