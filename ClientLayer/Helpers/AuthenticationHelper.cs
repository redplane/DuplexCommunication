using System;
using System.ServiceModel;
using ClientLayer.Models;
using ClientLayer.Service_References.AuthenticateService;
using SharedLayer.Models;

namespace ClientLayer.Helpers
{
    public class AuthenticationHelper
    {
        private static AuthenticationHelper _instance;

        public static AuthenticationHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AuthenticationHelper();

                return _instance;
            }
        }

        public ClientIdentity AuthenticateAccount(string serviceAddress)
        {
            // Create a channel factory to connect to service with default basic http binding.
            var callbackHandler = new AuthenticationServiceHandler();

            // Create an instance of BasicHttpBinding class.
            var wsDualHttpBinding = new WSDualHttpBinding();

            // Create a channel factory to connect to service with default basic http binding.        
            var channelFactory = new DuplexChannelFactory<IAuthenticationService>(callbackHandler, wsDualHttpBinding);

            // Create End Point 
            var endpointAddress = new EndpointAddress(serviceAddress);

            // Create Channel 
            var proxy = channelFactory.CreateChannel(endpointAddress);

            // Invalid proxy.
            if (proxy == null)
                return null;

            // Authenticate account and retrieve identity sent back from server.
            return proxy.AuthenticateAccount(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    }
}