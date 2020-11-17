using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bot.Models;

namespace bot
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public Models.Message Message;
    }
    public interface IConnector
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        Task Connect();
        Task<List<Message>> GetChatMessages(string id);
        Task SendMessage(string chatId, string content);
    }
}
