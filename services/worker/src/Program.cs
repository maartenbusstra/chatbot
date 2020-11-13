using System.Collections.Generic;
using System.Threading.Tasks;
using bot.Adapters;
using bot.Connectors;
using bot.Apps;

namespace bot
{
  public class Program
  {
    public static void Main(string[] args)
      => new Program().MainAsync().GetAwaiter().GetResult();
    public async Task MainAsync()
    {
      var bot = new Bot(
        new DiscordConnector(""),
        new List<IBotApp>(new IBotApp[] { new Clock() }),
        new MemoryAdapter()
      );

      await bot.Start();
    }
  }
}
