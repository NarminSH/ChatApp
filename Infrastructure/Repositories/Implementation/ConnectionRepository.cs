using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementation
{
    public class ConnectionRepository : GenericRepository<Connection>, IConnectionRepository
    {
        private readonly ApplicationDbContext _context;
        public ConnectionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Connection> GetUserDirectMessages(string id)
        {
            IQueryable<Connection> res = _context.Connections.Where(u => u.ReceiverId == id || u.SenderId == id)
                .Where(u => u.IsPrivate == true);
            return res;
        }

        public IQueryable<Connection> GetUsersChannels(string id)
        {
            IQueryable<Connection> res = _context.Connections.Where(u => u.ReceiverId == id || u.SenderId == id)
                .Where(u=>u.IsChannel == true);
            return res;
        }
    }
}

