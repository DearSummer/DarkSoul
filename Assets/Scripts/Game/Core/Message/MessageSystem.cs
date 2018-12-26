namespace DS.Game.Core.Message
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
