using System.Threading.Tasks;
using System;

namespace bot
{
  public interface IChatConnector
  {
    event EventHandler MessageReceived;
    Task Connect();
  }
}
