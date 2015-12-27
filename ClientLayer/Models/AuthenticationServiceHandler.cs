using System;
using ClientLayer.Service_References.AuthenticateService;

namespace ClientLayer.Models
{
    public class AuthenticationServiceHandler : IAuthenticationServiceCallback
    {
        public void OnClientAuthenticationDone(bool success)
        {
            Console.WriteLine("Authentication result = {0}", success);
        }
    }
}