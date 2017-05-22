using PlayerIOClient;

namespace EE.FutureProof
{
    public class FutureProofConnection : IConnectionWrapper
    {
        private readonly IConnectionWrapper _innerConnection;

        internal FutureProofConnection(IConnectionWrapper connection)
        {
            this._innerConnection = connection;
            this._innerConnection.OnMessage += this.InternalConnection_OnMessage;
        }

        public Connection InternalConnection => this._innerConnection.InternalConnection;
        public bool Connected => this._innerConnection.Connected;

        private void InternalConnection_OnMessage(object sender, Message m)
        {
            this.Receive(m);
        }

        protected virtual void Receive(Message m)
        {
            this.OnMessage?.Invoke(this, m);
        }

        public virtual void Send(Message m)
        {
            this._innerConnection.Send(m);
        }

        public void Send(string type, params object[] parameters)
        {
            this.Send(Message.Create(type, parameters));
        }

        public void Disconnect()
        {
            this._innerConnection.Disconnect();
        }

        public event MessageReceivedEventHandler OnMessage;

        public event DisconnectEventHandler OnDisconnect
        {
            add { this._innerConnection.OnDisconnect += value; }
            remove { this._innerConnection.OnDisconnect -= value; }
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
            this._innerConnection.Disconnect();
        }
    }
}