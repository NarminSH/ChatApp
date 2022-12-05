using System;
namespace Application.Hubs
{
    public interface ITypedHubClient
    {
        Task BroadcastPost(Post post);
    }
}

