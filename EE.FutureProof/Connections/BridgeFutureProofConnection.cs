using System;
using EE.FutureProof.Bridge;
using PlayerIOClient;

namespace EE.FutureProof
{
    internal class BridgeFutureProofConnection : FutureProofConnection, IFutureProofConnection
    {
        private readonly IUpgrader _adapter;

        public BridgeFutureProofConnection(IConnectionWrapper connection, IUpgrader adapter) 
            : base(connection)
        {
            this._adapter = adapter;
        }

        protected override void Receive(Message m)
        {
            base.Receive(this._adapter.DowngradeReceive(this, m));
        }

        public override void Send(Message m)
        {
            base.Send(this._adapter.UpgradeSend(this, m));
        }

        void IFutureProofConnection.Receive(Message m)
        {
            this.Receive(m);
        }
    }
}