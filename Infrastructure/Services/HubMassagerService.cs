using Application.Contracts;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services;

public class HubMassagerService : IHubMassagerService
{
    private readonly IHubContext<JoinRoomHub> _hubContext;

    public HubMassagerService(IHubContext<JoinRoomHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendGroupNotify(string groupId, string notify)
    {
        await _hubContext.Clients
            .Group(groupId)
            .SendAsync("Notify", notify);
    }
}