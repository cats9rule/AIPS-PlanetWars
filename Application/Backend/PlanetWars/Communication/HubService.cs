using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanetWars.DTOs;

namespace PlanetWars.Communication
{
    public class HubService
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public HubService(IHubContext<MessageHub> messageHubContext)
        {
            _hubContext = messageHubContext;
        }

        public async Task<MessageResponseDto> NotifyOnGameChanges(PlayedMoveDto param)
        {
            await _hubContext.Clients.Group(param.SessionID.ToString()).SendAsync(param.ClientHandler, param);
            return new MessageResponseDto { IsSuccessful = true, Message = "Turn Played." };
            //FIXME: mislim da je ovaj message response dto suvisan, ne znam sta mi bilo da ga pravim uopste na cemu li sam ja...
        }

        public Task NotifyPlayerLeft(LeaveGameNotificationDto param)
        {
            return _hubContext.Clients.Group(param.SessionID.ToString()).SendAsync("onPlayerLeave", param);
        }
    }
}