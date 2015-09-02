using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Hadouken
{
    public class HadoukenClient
    {
        public ServerInfo Server { get; set; }

        /// <summary>
        /// Creates a new Hadouken client.
        /// </summary>
        /// <param name="ServerIP"></param>
        /// <param name="ServerPort"></param>
        public HadoukenClient(ServerInfo serverInfo)
        {
            this.Server = serverInfo;
        }

        /// <summary>
        /// Creates a Hadouken client with the default IP address (127.0.0.1) and port (7070). 
        /// </summary>
        public HadoukenClient()
        {
            this.Server = new ServerInfo(IPAddress.Parse("127.0.0.1"), 7070, "admin", "admin");
        }

        public Core.SystemInfo GetSystemInfo()
        {
            RequestMaker request = new RequestMaker(Server);
            var result = request.DoJSONRPCRequest("core.getSystemInfo");
            var obj = JSONRPCReader.GetResult<Core.SystemInfo>(result);
            return obj;
        }       



    }

    public class ServerInfo
    {
        /// <summary>
        /// IP address of the servers' WebUI.
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// Port the servers' WebUI listens on.
        /// </summary>
        public int Port { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public ServerInfo(IPAddress iPAddress, int port, string username, string password)
        {
            IPAddress = iPAddress;
            Port = port;
            Username = username;
            Password = password;
        }        
    }

    class RequestMaker
    {
        private ServerInfo Server;

        public RequestMaker(ServerInfo serverInfo)
        {
            this.Server = serverInfo;
        }

        public string HTTPGet(string path)
        {
            var webclient = new WebClient();
            webclient.Credentials = new NetworkCredential(Server.Username, Server.Password);
            var result = webclient.DownloadString("http://" + Server.IPAddress.ToString() + ":" + Server.Port.ToString() + "/api/" + path);
            return result;
        }

        public string HTTPPost(string path, string data)
        {
            var webclient = new WebClient();
            webclient.Credentials = new NetworkCredential(Server.Username, Server.Password);
            var result = webclient.UploadString("http://" + Server.IPAddress.ToString() + ":"+Server.Port.ToString()+"/api/" + path, data);
            return result;
        }

        public string HTTPPost(string data)
        {
            return HTTPPost("", data);
        }

        public string DoJSONRPCRequest(string method, string[] _params = null)
        {
            var message = new JSONRPCMessage(method, _params).ToString();
            return HTTPPost(message);
        }

    }
}
