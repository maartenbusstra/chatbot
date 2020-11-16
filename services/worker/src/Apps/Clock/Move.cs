using System;
using System.Collections.Generic;
using bot.Models;

namespace bot.Apps.Clock
{

  public class Move
  {
    private static Dictionary<string, int> _specialMoves = new Dictionary<string, int>()
    {
      {"00:00", 2},
      {"11:11", 2},
      {"22:22", 2},
      {"12:34", 1},
      {"13:37", 3}
    };

    public Message Message;
    public string Hours => Message.Content.Split(":")[0];
    public string Minutes => Message.Content.Split(":")[1];
    public string MessageHours => PadZero(Message.CreatedAt.Hour.ToString());
    public string MessageMinutes => PadZero(Message.CreatedAt.Minute.ToString());

    private string PadZero(string unit)
    {
      return unit.Length < 2 ? $"0{unit}" : unit;
    }

    public int Score()
    {
      if (!(Hours == MessageHours && Minutes == MessageMinutes)) return 0;
      if (IsSpecialMove()) return _specialMoves[Message.Content];
      return Hours == Minutes ? 1 : 0;
    }

    public bool IsSpecialMove() => _specialMoves.ContainsKey(Message.Content);

    public string ToUniqueId() => String.Join("|", new string[] {
      Message.CreatedAt.Year.ToString(),
      Message.CreatedAt.Month.ToString(),
      Message.CreatedAt.Day.ToString(),
      MessageHours,
      MessageMinutes
    });
  }
}
