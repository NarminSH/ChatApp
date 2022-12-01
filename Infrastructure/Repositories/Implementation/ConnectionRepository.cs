using System;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

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
            var res = _context.Connections.Where(x => x.EmployeeChannels.Any(c => c.EmployeeId == id)
            && x.IsPrivate == true);
            return res;
        }

        public IQueryable<Connection> GetUsersChannels(string id)
        {
            IQueryable<Connection> res = _context.Connections.Where(x => x.EmployeeChannels.Any(c => c.EmployeeId == id)
            && x.IsChannel == true);
            return res;
        }

    }
}

