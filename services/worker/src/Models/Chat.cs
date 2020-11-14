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
      var msgs = await _connector.GetChatMessages(Id);
      return new List<Message>();
    }
  }
}
