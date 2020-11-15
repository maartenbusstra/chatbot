using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using bot.Models;

namespace bot
{
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  public class CommandAttribute : Attribute
  {
    public Regex TextMatch { get; set; }
    public CommandAttribute(string pattern)
    {
      TextMatch = new Regex(pattern);
    }
  }
  public class Bot
  {
    private readonly IConnector _connector;
    private readonly List<IBotApp> _apps;
    private readonly IStorageAdapter _storage;

    public Bot(IConnector connector, List<IBotApp> apps, IStorageAdapter storage)
    {
      _connector = connector;
      _connector.MessageReceived += HandleMessage;
      _apps = apps;
      _storage = storage;
    }

    public async Task Start()
    {
      await _connector.Connect();
    }

    private void RouteMessage(Message m)
    {
      foreach (var app in _apps)
      {
        Type t = app.GetType();
        CommandAttribute att;
        MemberInfo[] memberInfo = t.GetMethods();

        for (int i = 0; i < memberInfo.Length; i++)
        {
          att = (CommandAttribute)Attribute.GetCustomAttribute(memberInfo[i], typeof(CommandAttribute));
          if (att == null) continue;
          var match = att.TextMatch.Match(m.Content);
          if (!match.Success) continue;
          MethodInfo method = t.GetMethod(memberInfo[i].Name);
          method.Invoke(app, new object[] { m, att.TextMatch.Match(m.Content) });

        }
      }
    }

    private async void HandleMessage(object sender, MessageReceivedEventArgs args)
    {
      Message m = args.Message;
      RouteMessage(args.Message);

      if (!(m.Chat.Id == "discord:773180517470044180")) return;
      await m.Reply($"chat id: {m.Chat.Id}");
      await m.Reply($"received {m.Content} at {m.CreatedAt}");
      List<Message> messages = await m.Chat.GetMessages();
      await m.Reply($"this chat has {messages.Count()} messages");
    }
  }
}
