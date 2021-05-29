using NetCoreServer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POGOProtos.Rpc;
using Polystone.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Polystone.Business
{
    public sealed class Payload
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("proto")]
        public string Proto { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lng")]
        public double Lng { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("level")]
        public string Level { get; set; }
        [JsonProperty("account_name")]
        public string AccountName { get; set; }
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        public Method GetMethod() => (Method)Type;
        public byte[] ConvertFromBase64String() => Convert.FromBase64String(Proto);
    }

    public sealed class Content
    {
        [JsonProperty("payloads")]
        public List<Payload> Payloads { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Payloads = Payloads.Where(p_ => p_.AccountName != null && p_.AccountName != "null").ToList();
        }
    }

    public class PolystoneSession : TcpSession
    {
        private bool _reading = false;
        private StringBuilder _currentMessage = new StringBuilder();
        private readonly Regex _multipleContent = new Regex(@"(\{""payloads"":.*?,""key"":""[a-z0-9]+""\})", RegexOptions.IgnoreCase);

        public PolystoneSession(TcpServer server) : base(server) { }

        protected override void OnConnected()
        {
            _reading = false;
            _currentMessage = new StringBuilder();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string receiveMessage = Encoding.UTF8.GetString(buffer, (int)offset, (int)size).Replace("\n", string.Empty).Replace("\r", string.Empty).Trim();

            if (!_reading && receiveMessage.StartsWith("{"))
            {
                _reading = true;
                _currentMessage.Clear();
                _currentMessage = new StringBuilder();
            }

            if (_reading)
            {
                _currentMessage.Append(receiveMessage);
            }

            if (_reading && receiveMessage.EndsWith("}"))
            {
                _reading = false;
            }

            if (!_reading && _currentMessage.Length > 0)
            {
                try
                {
                    Content content = JsonConvert.DeserializeObject<Content>(_currentMessage.ToString());
                    foreach (Payload payload in content.Payloads)
                    {
                        PolystoneHandler.Handle(payload);
                    }
                }
                catch (Exception)
                {
                    foreach (string messageString in _multipleContent.Matches(_currentMessage.ToString()).Cast<Match>().Select(match => match.Value))
                    {
                        Content content = JsonConvert.DeserializeObject<Content>(messageString);
                        foreach (Payload payload in content.Payloads)
                        {
                            PolystoneHandler.Handle(payload);
                        }
                    }
                }

                _reading = false;
                _currentMessage.Clear();
                _currentMessage = new StringBuilder();
            }
        }
    }
}
