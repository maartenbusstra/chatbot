using System;

namespace bot.Models
{
  public class Message
  {
    public string Id;
    public User User;
    public Chat Chat;
    public string Content;
    public DateTime CreatedAt;
    public void Reply(string s)
    {
      System.Console.WriteLine("reply " + s);
    }
  }
}
