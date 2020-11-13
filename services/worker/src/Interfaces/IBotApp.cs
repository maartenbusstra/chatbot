using System.Collections.Generic;
using bot.Models;
using System.Text.RegularExpressions;

namespace bot
{

  public interface IBotApp
  {
    public static List<string> commands;
    void HandleMessage(Message message, Match match);
  }

}
