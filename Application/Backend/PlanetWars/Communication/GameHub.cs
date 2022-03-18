using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanetWars.DTOs;

namespace PlanetWars.Communication
{
    public class GameHub : Hub
    {
        //JoinGame i LeaveGame obradjeni u MessageHub(?)
        //potencijalno ne treba, zavisi od gore napisanog komentara
        //begin
        public async Task<MessageResponseDto> JoinGame(JoinGameChatDto param)
        {
            //FIXME: ne treba poruka da se salje
            await Groups.AddToGroupAsync(Context.ConnectionId, param.SessionID.ToString());
            MessageDto message = new MessageDto
            {
                UserID = param.UserID,
                ClientHandler = param.ClientHandler,
                SessionID = param.SessionID,
                UsernameWithTag = param.UsernameWithTag,
                Contents = param.UsernameWithTag + " has joined the room."
            };
            await Clients.Group(param.SessionID.ToString()).SendAsync(param.ClientHandler, message);
            return new MessageResponseDto { IsSuccessful = true, Message = "Joined game." };
        }

        public async Task<MessageResponseDto> LeaveGame(LeaveGameChatDto param)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, param.SessionID.ToString());
            await Clients.Group(param.SessionID.ToString()).SendAsync(param.ClientHandler, param.UsernameWithTag + " has left the game.");
            return new MessageResponseDto { IsSuccessful = true, Message = "Left game." };
        }
        //end

        //FIXME: ova metoda se poziva iz SessionService, prima GameUpdateDto, rename to NotifyOnGameChanges
        public async Task<MessageResponseDto> PlayTurn(PlayedMoveDto param)
        {
            await Clients.Group(Context.ConnectionId).SendAsync(param.ClientHandler, param);
            return new MessageResponseDto { IsSuccessful = true, Message = "Turn Played." };
        }
    }
}