using UnityEngine;

namespace DS.Game.Message
{
    public enum MessageType  {
        DAMAGE,
        DIE,
        INVNLERABLE
    }

    public interface IMessageReceiver <T>
    {
        void OnMessageReceiver(MessageType type, object sender, T data);
    }
}
