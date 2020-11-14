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

    private void HandleMessage(object sender, MessageReceivedEventArgs args)
    {
      System.Console.WriteLine("received" + args.Message.Content);
    }
  }
}
