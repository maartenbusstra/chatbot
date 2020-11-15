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

    public static void GetAttribute(Type t)
    {
      CommandAttribute att;

      // Get the method-level attributes.

      // Get all methods in this class, and put them
      // in an array of System.Reflection.MemberInfo objects.
      MemberInfo[] MyMemberInfo = t.GetMethods();

      // Loop through all methods in this class that are in the
      // MyMemberInfo array.
      for (int i = 0; i < MyMemberInfo.Length; i++)
      {
        att = (CommandAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(CommandAttribute));
        if (att == null)
        {
          Console.WriteLine("No attribute in member function {0}.\n", MyMemberInfo[i].ToString());
        }
        else
        {
          Console.WriteLine("The TextMatch Attribute for the {0} member is: {1}.",
              MyMemberInfo[i].ToString(), att.TextMatch);
        }
      }
    }

    private async void HandleMessage(object sender, MessageReceivedEventArgs args)
    {
      Message m = args.Message;

      foreach (var process in processes)
      {
        GetAttribute(process.GetType());
      }

      await m.Reply($"received {m.Content} at {m.CreatedAt}");
      List<Message> messages = await m.Chat.GetMessages();
      await m.Reply($"this chat has {messages.Count()} messages");
    }
  }
}
