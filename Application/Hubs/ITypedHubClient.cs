using System;
namespace Application.Hubs
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(Post post);
    }
}

