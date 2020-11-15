using bot.Models;
using System.Text.RegularExpressions;

namespace bot.Apps.Clock
{
  public class Clock : IBotApp
  {

    [Command(@"^(\d\d):(\d\d)$")]
    public async void HandleMove(Message message, Match match)
    {
      // blabla
      await message.Reply("SCORE!");
    }
  }
}
