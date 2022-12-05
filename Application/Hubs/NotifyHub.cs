using System;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs
{
    public class NotifyHub : Hub<ITypedHubClient>
    {
    }
}

