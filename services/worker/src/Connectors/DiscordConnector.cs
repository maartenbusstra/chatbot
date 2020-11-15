using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using bot.Models;

namespace bot.Connectors
{
  public class DiscordConnector : IConnector
  {
    private DiscordSocketClient _client;
    private DiscordRestClient _restClient;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    private string _token;

    public DiscordConnector(string token)
    {
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

      _restClient = new DiscordRestClient();
      await _restClient.LoginAsync(TokenType.Bot, _token);

      // Block this task until the program is closed.
      await Task.Delay(-1);
    }

    private async Task<IMessageChannel> GetChannel(string id)
    {
      IMessageChannel channel = await _restClient.GetChannelAsync(Convert.ToUInt64(id.Split(":")[1])) as IMessageChannel;
      return channel;
    }

    public async Task<List<Message>> GetChatMessages(string id)
    {
      IMessageChannel channel = await GetChannel(id);
      var messages = await channel.GetMessagesAsync(999999).FlattenAsync();
      return messages.Select(m => ConvertMessage(m)).ToList<Message>();
    }

    public async Task SendMessage(string chatId, string content)
    {
      IMessageChannel channel = await GetChannel(chatId);
      await channel.SendMessageAsync(content);
    }

    private Message ConvertMessage(IMessage message)
    {
      Chat c = new Chat(connector: this, connectorId: message.Channel.Id.ToString())
      {
        Id = "discord:" + message.Channel.Id.ToString(),
      };
      Message m = new Message()
      {
        Id = message.Id.ToString(),
        Content = message.Content,
        User = new User() { Id = "discord:" + message.Author.Id.ToString(), Name = message.Author.Username },
        Chat = c,
        CreatedAt = message.CreatedAt.,
      };
      return m;
    }

    private async Task DiscordMessageReceived(SocketMessage message)
    {
      if (message.Author.Id == _client.CurrentUser.Id) return;

      Message m = ConvertMessage(message);

      EventHandler<MessageReceivedEventArgs> handler = MessageReceived;
      handler?.Invoke(this, new MessageReceivedEventArgs() { Message = m });
      await Task.CompletedTask;
    }
  }
}
