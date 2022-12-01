using System;
namespace Application.Repositories.Abstraction
{
    public interface IConnectionRepository : IGenericRepository<Connection>
    {
        public IQueryable<Connection> GetUsersChannels(string id);
        public IQueryable<Connection> GetUserDirectMessages(string id);

    }
}

