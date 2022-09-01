using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanetWars.DTOs;

namespace PlanetWars.Communication
{
    public class MessageHub : Hub
    {
        public async Task<ResponseParam> JoinGameGroup(JoinSessionGroupDto param)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, param.SessionID);
            MessageDto mess = new MessageDto {
                ClientHandler = "receiveMessage",
                Contents = param.UsernameWithTag + " has joined the game!",
                SessionID = param.SessionID,
                UserID = param.UserID,
                UsernameWithTag = "ChatBot"
            };
            await Clients.Group(param.SessionID).SendAsync("receiveMessage", mess);
            return new ResponseParam { IsSuccessful = true, Message = "Joined Game." };
        }

        public async Task<ResponseParam> LeaveGameGroup(LeaveSessionGroupDto param)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, param.SessionID.ToString());
            await Clients.Group(param.SessionID.ToString()).SendAsync(param.ClientHandler, param.UsernameWithTag + " has left the game!");
            return new ResponseParam { IsSuccessful = true, Message = "Left Game." };
        }

        public async Task<ResponseParam> SendChatMessage(MessageDto message)
        {
            await Clients.Group(message.SessionID).SendAsync("receiveMessage", message);
            return new ResponseParam { IsSuccessful = true, Message = "Sent Message." };
        }
    }
}