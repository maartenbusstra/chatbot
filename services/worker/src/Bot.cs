using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using bot.Models;

namespace bot
{
  public class Bot
  {
    private readonly IConnector _connector;
    private readonly List<IBotApp> _apps;
    private readonly IStorageAdapter _storage;

    private IEnumerable<IBotApp> processes { get; set; }

    public Bot(IConnector connector, List<IBotApp> apps, IStorageAdapter storage)
    {
      _connector = connector;
      _connector.MessageReceived += HandleMessage;
      _apps = apps;
      _storage = storage;
    }

    public async Task Start()
    {
      processes = _apps.Select(app =>
      {
        return app;
      });

      await _connector.Connect();
    }

    private async void HandleMessage(object sender, MessageReceivedEventArgs args)
    {
      Message m = args.Message;

      System.Console.WriteLine("received " + m.Content + " " + m.CreatedAt);
      m.Reply("reply");
      List<Message> messages = await m.Chat.GetMessages();
      if (messages.Count > 0)
        System.Console.WriteLine("received " + messages[3].Content);
    }
  }
}
