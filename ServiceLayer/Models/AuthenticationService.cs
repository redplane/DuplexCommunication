using System;
using System.Collections.Generic;
using System.ServiceModel;
using ServiceLayer.Interfaces;
using SharedLayer.Models;

namespace ServiceLayer.Models
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Dictionary<string, IAuthenticationCallback> _callbacksList;

        public AuthenticationService()
        {
            _callbacksList = new Dictionary<string, IAuthenticationCallback>();
        }

        public ClientIdentity AuthenticateAccount(string name, string password)
        {
            if (_callbacksList.ContainsKey(name))
            {
                _callbacksList[name].OnClientAuthenticationDone(false);
                return null;
            }

            var a = OperationContext.Current;
            var clientIdentity = new ClientIdentity();
            clientIdentity.Name = name;
            clientIdentity.Password = password;
            clientIdentity.SecretKey = Guid.NewGuid().ToString();

            var callbackChannel = OperationContext.Current.GetCallbackChannel<IAuthenticationCallback>();
            _callbacksList.Add(name, callbackChannel);

            callbackChannel.OnClientAuthenticationDone(true);
            return clientIdentity;
        }
    }
}