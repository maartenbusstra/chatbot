using System.Collections.Generic;

namespace bot.Adapters
{
  public class MemoryAdapter : IStorageAdapter
  {

    private Dictionary<string, string> _state { get; set; }

    public MemoryAdapter()
    {
      _state = new Dictionary<string, string>();
    }

    public string GetItem(string key)
    {
      return "";
    }

    public void SetItem(string key, string value)
    {
    }
  }
}
