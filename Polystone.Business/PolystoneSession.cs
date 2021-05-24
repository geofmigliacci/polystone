using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Polystone.Business
{
    public sealed class PolystoneSession : TcpSession
    {
        bool Reading = false;
        StringBuilder CurrentMessage = new StringBuilder();

        public PolystoneSession(NetCoreServer.TcpServer server) : base(server) { }

        protected override void OnConnected()
        {
            Reading = false;
            CurrentMessage = new StringBuilder();
            var o = this;
        }

        protected override void OnDisconnected()
        {
            
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string receiveMessage = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            if (receiveMessage.StartsWith("{"))
            {
                Reading = true;
            }

            if (Reading)
            {
                CurrentMessage.Append(receiveMessage);
            }
       
            if(receiveMessage.EndsWith("}"))
            {
                Reading = false;
            }

            if(!Reading)
            {

                CurrentMessage.Clear();
            }
            // binaire
            // doit convertir en json (objet generaliste avec héritage) Payload
            // { type:"CATCH", pokemonId: 14...}
            // conversion automatique vers l'objet Catch
        }

        protected override void OnError(SocketError error)
        {

        }
    }
}
