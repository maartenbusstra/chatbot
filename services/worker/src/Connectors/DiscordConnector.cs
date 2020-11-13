using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace bot.Connectors
{
  public class DiscordConnector : IChatConnector
  {
    private DiscordSocketClient _client;
    public event EventHandler MessageReceived;
    private string _token;
    public DiscordConnector(string token)
    {
      System.Console.WriteLine("hello");
      _token = token;
    }

    private Task Log(LogMessage msg)
    {
      Console.WriteLine(msg.ToString());
      return Task.CompletedTask;
    }

    public async Task Connect()
    {
      _client = new DiscordSocketClient();

      _client.Log += Log;
      _client.MessageReceived += DiscordMessageReceived;

      await _client.LoginAsync(TokenType.Bot, _token);
      await _client.StartAsync();

      // Block this task until the program is closed.
      await Task.Delay(-1);
    }
    private async Task DiscordMessageReceived(SocketMessage message)
    {
      if (message.Content == "!ping")
      {
        await message.Channel.SendMessageAsync("Pong!");
      }
    }
  }
}
// public event Func<SocketMessage, Task> MessageReceived