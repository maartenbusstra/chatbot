using System.Collections.Generic;
using System.Text.Json;

namespace bot.Apps.Clock
{
  public class Player
  {
    public string UserId { get; set; }
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
      // todo
      return new GameState() { Players = new List<Player>(), Hits = new List<Hit>() };
    }

    // [JsonPropertyName("players")]
    public List<Player> Players { get; set; } // = new List<Player>();

    // [JsonPropertyName("hits")]
    public List<Hit> Hits { get; set; } // = new List<Hit>();

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
