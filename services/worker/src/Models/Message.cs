using System;

namespace bot.Models
{
  public class Message
  {
    public string Id;
    public string ChatId;
    public User user;
    public Channel Channel;
    public string Content;
    public DateTime CreatedAt;
    public void Reply(string s)
    {
      System.Console.WriteLine("reply " + s);
    }
  }
}
