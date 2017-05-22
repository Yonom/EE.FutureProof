using PlayerIOClient;

namespace EE.FutureProof.Bridge
{
    public interface IFutureProofConnection
    {
        void Send(Message m);
        void Receive(Message m);
    }
}