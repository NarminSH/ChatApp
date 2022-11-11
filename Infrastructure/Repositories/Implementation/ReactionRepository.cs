using System;

namespace Infrastructure.Repositories.Implementation
{
    public class ReactionRepository : GenericRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

