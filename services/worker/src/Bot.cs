

namespace bot {
  public class Bot
  {

    private readonly ChatConnector Connector;
    private readonly List<BotApp> Apps;
    private readonly IStorageAdapter Storage;

    public Bot(ChatConnector c, List<BotApp> a, IStorageAdapter s)
    {
      Connector = c;
      Apps = a;
      Storage = s;
    }
  }
}
