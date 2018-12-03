using UnityEngine;

namespace DS.Game.Message
{
    public enum MessageType  {
        DAMAGE,
        DIE
    }

    public interface IMessageReceiver <T>
    {
        void OnMessageReceiver(MessageType type, object sender, T data);
    }
}
