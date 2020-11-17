using System.Collections.Generic;
using System.Text.Json;

namespace bot.Apps.Clock
{
    public class Player
    {
        public string UserId { get; init; }
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public class Hit
    {
        public string UserId { get; set; }
        public string MoveId { get; set; }
    }

    public class GameState
    {
        public static GameState Parse(string json)
        {
            if (json == null) return new GameState() { Players = new List<Player>(), Hits = new List<Hit>() };
            GameState state = JsonSerializer.Deserialize<GameState>(
              json, options: new JsonSerializerOptions
              {
                  PropertyNamingPolicy = JsonNamingPolicy.CamelCase
              });
            return state;
        }

        public List<Player> Players { get; set; }
        public List<Hit> Hits { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }
    }
}
