using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using bot.Models;

namespace bot.Connectors
{
  public class DiscordConnector : IChatConnector
  {
    private DiscordSocketClient _client;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
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
      Message m = new Message()
      {
        Id = message.Id.ToString(),
        Content = message.Content,
        ChatId = message.Channel.Id,
        User = new User() { Id = message.Author.Id, Name = message.Author.Username },
        Channel = message.Channel,
        CreatedAt = message.TimeStamp
      };

      EventHandler<MessageReceivedEventArgs> handler = MessageReceived;

      handler?.Invoke(this, new MessageReceivedEventArgs() { Message = m });



      if (message.Content == "!ping")
      {
        await message.Channel.SendMessageAsync("Pong!");
      }
    }
  }
}
