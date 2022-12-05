using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementation
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {

        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetChannelPosts(int id)
        {
            return await _context.Posts.Where(p => p.ChannelId == id).ToListAsync();
        }
    }
}

