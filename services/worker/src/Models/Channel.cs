using System.Collections.Generic;

namespace bot.Models
{
  public class Channel
  {

    public string Id;
    private string _connectorId;
    private readonly IChatConnector _connector;

    public Channel(string connectorId, IChatConnector connector)
    {
      _connectorId = connectorId;
      _connector = connector;
    }

    public List<Message> GetMessages()
    {
      return new List<Message>();
    }
  }
}
