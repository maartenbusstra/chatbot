using bot.Models;
using System.Text.RegularExpressions;

namespace bot.Apps.Clock
{
  public class Clock : BotApp, IBotApp
  {
    public Clock(IStorageAdapter storage) : base(storage) { }

    [Command(@"^(\d\d):(\d\d)$")]
    public async void HandleMove(Message message, Match match)
    {
      string stateJson = _storage.GetItem(message.Chat.Id);
      await message.Reply($"state: {stateJson}");

      Game game = new Game(stateJson);
      Move move = new Move() { Message = message };

      string response = game.HandleMove(move);
      await message.Reply(response);
      await message.Reply(game.State.ToJson());

    }
  }
}
