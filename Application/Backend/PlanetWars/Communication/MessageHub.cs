using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanetWars.DTOs;

namespace PlanetWars.Communication
{
    public class MessageHub : Hub
    {
        public async Task<MessageResponseDto> JoinGameChat(JoinGameChatDto param)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, param.UserID.ToString());
            await Clients.Group(param.SessionID.ToString()).SendAsync(param.ClientHandler, param.UsernameWithTag + " has joined the game!");
            return new MessageResponseDto { IsSuccessful = true, Message = "Joined Game." };
        }

        public async Task<MessageResponseDto> LeaveGameChat(LeaveGameChatDto param)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, param.SessionID.ToString());
            await Clients.Group(param.SessionID.ToString()).SendAsync(param.ClientHandler, param.UsernameWithTag + " has left the game!");
            return new MessageResponseDto { IsSuccessful = true, Message = "Left Game." };
        }

        public async Task<MessageResponseDto> SendChatMessage(MessageDto message)
        {
            await Clients.Group(message.SessionID.ToString()).SendAsync(message.ClientHandler, message);
            return new MessageResponseDto { IsSuccessful = true, Message = "Sent Message." };;
        }
    }
}