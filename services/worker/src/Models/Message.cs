namespace bot.Models
{
  public class Message
  {
    public string Id;
    public string Content;
    public void Reply(string s)
    {
      System.Console.WriteLine("reply " + s);
    }
  }
}
