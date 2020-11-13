using System.Collections.Generic;
using bot.Models;
using bot;
using System.Text.RegularExpressions;

namespace bot.Apps
{
  public class Clock : IBotApp
  {
    public static List<string> commands = new List<string>(new string[] {
      @"^(\d\d):(\d\d)$"
    });
    public void HandleMessage(Message message, Match match)
    {
      // blabla
      message.Reply("YAY");
    }

  }

}
