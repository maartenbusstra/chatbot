using bot.Models;
using bot;
using System.Text.RegularExpressions;

namespace bot.Apps
{
  public class Clock : IBotApp
  {

    [Command(@"^(\d\d):(\d\d)$")]
    public void HandleMove(Message message, Match match)
    {
      // blabla
      message.Reply("SCORE!");
    }
  }
}
