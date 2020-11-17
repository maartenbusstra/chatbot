using System.Collections.Generic;
using bot.Models;
using System.Text.RegularExpressions;

namespace bot
{
    public abstract class BotApp
    {
        protected IStorageAdapter _storage;

        protected BotApp(IStorageAdapter storage)
        {
            _storage = storage;
        }
    }
    public interface IBotApp
    {
        // public static List<string> commands;
        // void HandleMessage(Message message, Match match);
    }

}
