using System;
namespace Application.Repositories.Abstraction
{
    public interface IPostRepository: IGenericRepository<Post>
    {
        public Task<IEnumerable<Post>> GetChannelPosts(int id);
    }
}

