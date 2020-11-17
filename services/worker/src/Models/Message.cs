using System;
using System.Threading.Tasks;

namespace bot.Models
{
    public class Message
    {
        public string Id;
        public User User;
        public Chat Chat;
        public string Content;
        public DateTimeOffset CreatedAt;
        public async Task Reply(string content)
        {
            await Chat.SendMessage(content);
        }
    }
}
