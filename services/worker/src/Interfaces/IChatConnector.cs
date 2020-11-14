using System.Threading.Tasks;
using System;

namespace bot
{
  public class MessageReceivedEventArgs : EventArgs
  {
    public Models.Message Message;
  }
  public interface IChatConnector
  {
    event EventHandler<MessageReceivedEventArgs> MessageReceived;
    Task Connect();
  }
}
