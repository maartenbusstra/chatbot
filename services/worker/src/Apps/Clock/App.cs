using System.Collections.Generic;
using bot.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bot.Apps.Clock
{
    public class Clock : BotApp, IBotApp
    {
        public Clock(IStorageAdapter storage) : base(storage) { }

        [Command(@"^(\d\d):(\d\d)$")]
        public async void HandleMove(Message message, Match match)
        {
            Game game = await GetGame(message);
            Move move = new Move { Message = message };

            string response = game.HandleMove(move);
            await message.Reply(response);
            await message.Reply(game.Scoreboard());

            _storage.SetItem(message.Chat.Id, game.State.ToJson());
        }

        private async Task<Game> RecalcGame(Game game, Message message)
        {
            await message.Reply("Just a sec.");
            // TODO: add "before: Message" param to GetMessages()
            // TODO: add way to stop fetching before this month
            List<Message> messages = await message.Chat.GetMessages();
            messages.Reverse();
            messages.RemoveAt(messages.Count - 1); // discard "Just a sec."
            messages.RemoveAt(messages.Count - 1); // discard last move

            foreach (var m in messages)
            {
                bool isSameMonth =
                    message.CreatedAt.Month == m.CreatedAt.Month &&
                    message.CreatedAt.Year == m.CreatedAt.Year;

                if (!isSameMonth) continue;

                if (new Regex(@"^(\d\d):(\d\d)$").Match(m.Content).Success)
                {
                    Move move = new Move() { Message = m };
                    game.HandleMove(new Move() { Message = m });
                }
            }

            return game;
        }

        private async Task<Game> GetGame(Message message)
        {
            string stateJson = _storage.GetItem(message.Chat.Id);
            Game game = new Game(stateJson);

            if (stateJson == null) return await RecalcGame(game, message);

            return game;

        }
    }
}
