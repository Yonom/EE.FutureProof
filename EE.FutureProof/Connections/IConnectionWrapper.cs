using PlayerIOClient;

namespace EE.FutureProof
{
    public interface IConnectionWrapper
    {
        Connection InternalConnection { get; }
        bool Connected { get; }
        void Send(Message m);
        void Send(string type, params object[] parameters);
        void Disconnect();
        event MessageReceivedEventHandler OnMessage;
        event DisconnectEventHandler OnDisconnect;
        void AddOnMessage(MessageReceivedEventHandler handler);
        void AddOnDisconnect(DisconnectEventHandler handler);
        void Dispose();
    }
}