using PlayerIOClient;

namespace EE.FutureProof
{
    class PlayerIOConnection : IConnectionWrapper
    {
        public Connection InternalConnection { get; }

        public PlayerIOConnection(Connection connection)
        {
            this.InternalConnection = connection;
        }

        public bool Connected => this.InternalConnection.Connected;

        public void Send(Message m)
        {
            this.InternalConnection.Send(m);
        }

        public void Send(string type, params object[] parameters)
        {
            this.Send(Message.Create(type, parameters));
        }

        public void Disconnect()
        {
            this.InternalConnection.Disconnect();
        }

        public event MessageReceivedEventHandler OnMessage
        {
            add { this.InternalConnection.OnMessage += value; }
            remove { this.InternalConnection.OnMessage -= value; }
        }

        public event DisconnectEventHandler OnDisconnect
        {
            add { this.InternalConnection.OnDisconnect += value; }
            remove { this.InternalConnection.OnDisconnect -= value; }
        }

        public void AddOnMessage(MessageReceivedEventHandler handler)
        {
            this.OnMessage += handler;
        }

        public void AddOnDisconnect(DisconnectEventHandler handler)
        {
            this.OnDisconnect += handler;
        }

        public void Dispose()
        {
            this.InternalConnection.Disconnect();
        }
    }
}
