using System.Collections.Generic;
using System.Threading.Tasks;

namespace bot.Models
{
    public class Chat
    {

        public string Id;
        private string _connectorId;
        private readonly IConnector _connector;

        public Chat(string connectorId, IConnector connector)
        {
            _connectorId = connectorId;
            _connector = connector;
        }

        public async Task<List<Message>> GetMessages()
        {
            return await _connector.GetChatMessages(Id);
        }

        public async Task SendMessage(string content)
        {
            await _connector.SendMessage(Id, content);
        }
    }
}
