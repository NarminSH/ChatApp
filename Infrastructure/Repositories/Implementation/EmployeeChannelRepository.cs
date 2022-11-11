using System;

namespace Infrastructure.Repositories.Implementation
{
    public class EmployeeChannelRepository : GenericRepository<EmployeeChannel>, IEmployeeChannelRepository
    {
        public EmployeeChannelRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

