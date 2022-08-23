using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanetWars.DTOs;
using PlanetWars.DTOs.Communication;

namespace PlanetWars.Communication
{
    public class HubService
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public HubService(IHubContext<MessageHub> messageHubContext)
        {
            _hubContext = messageHubContext;
        }

        public async Task<MessageResponseDto> NotifyOnNewPlayer(NewPlayerDto newPlayer)
        {
            await _hubContext.Clients.Group(newPlayer.SessionID.ToString()).SendAsync(newPlayer.ClientHandler, newPlayer);
            return new MessageResponseDto { IsSuccessful = true, Message = "New Player." };
        }

        public async Task<MessageResponseDto> NotifyOnGameChanges(GameUpdateDto param)
        {
            param.ClientHandler = "onGameUpdate";
            await _hubContext.Clients.Group(param.Session.ID.ToString()).SendAsync(param.ClientHandler, param);
            return new MessageResponseDto { IsSuccessful = true, Message = "Turn Played." };
            //FIXME: mislim da je ovaj message response dto suvisan
        }

        public async Task<MessageResponseDto> NotifyOnWinner(GameOverDto param)
        {
            param.ClientHandler = "onGameOver";
            await _hubContext.Clients.Group(param.SessionID.ToString()).SendAsync(param.ClientHandler, param);
            return new MessageResponseDto { IsSuccessful = true, Message = "Game Over." };
        }

        public Task NotifyPlayerLeft(LeaveGameNotificationDto param)
        {
            return _hubContext.Clients.Group(param.SessionID.ToString()).SendAsync("onPlayerLeave", param);
        }
    }
}