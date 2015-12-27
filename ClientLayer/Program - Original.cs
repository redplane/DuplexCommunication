using System;
using ClientLayer.Helpers;

namespace ClientLayer
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    var serviceAddress = "http://localhost:27020/DuplexService";

        //    var handler = new DuplexCallbackHandler();

        //    // Create an instance of HttpBinding with default initialization.
        //    var wsDualHttpBinding = new WSDualHttpBinding();
        //    wsDualHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
        //    wsDualHttpBinding.Security.Mode = WSDualHttpSecurityMode.Message;

        //    // Create a channel factory to connect to service with default basic http binding.        
        //    var channelFactory = new DuplexChannelFactory<IDuplexService>(handler, wsDualHttpBinding);

        //    // Create End Point 
        //    var endpointAddress = new EndpointAddress(serviceAddress);

        //    // Create Channel 
        //    var proxy = channelFactory.CreateChannel(endpointAddress);

        //    if (proxy == null)
        //    {
        //        Console.Write("Invalid proxy\n");
        //        Console.ReadLine();
        //        return;
        //    }

        //    var clientIdentity = proxy.RegisterClient(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        //    if (clientIdentity != null)
        //    {
        //        Console.WriteLine("Client identification is :");
        //        Console.WriteLine("Name: {0}", clientIdentity.Name);
        //        Console.WriteLine("Password: {0}", clientIdentity.Password);
        //    }
        //    else
        //        Console.WriteLine("Client registration is failed");

        //    proxy.FormatString("This is a formatted string");
        //    Console.ReadLine();
        //}

        private static void Main(string[] args)
        {
            const string serviceAddress = "http://localhost:27020/DuplexService";

            var identity = AuthenticationHelper.Instance.AuthenticateAccount(serviceAddress);
            if (identity == null)
            {
                Console.WriteLine("Authentication is failed");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Authentication is successful");
            Console.WriteLine("Client identity");
            Console.WriteLine("Name = {0}", identity.Name);
            Console.WriteLine("Password = {0}", identity.Password);
            Console.WriteLine("Secret key = {0}", identity.SecretKey);
            Console.ReadLine();
        }
    }
}