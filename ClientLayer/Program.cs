using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using ClientLayer.Models;
using ClientLayer.Service_References.AuthenticateService;

namespace ClientLayer
{
    internal class Program
    {
        //private const string ServiceAddress = "http://localhost:27020/AccountAuthenticator";

        private static void Main(string[] args)
        {
            var serverPort = 0;
            while (true)
            {
                try
                {
                    Console.Write("Enter server port : ");
                    serverPort = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid server port. Please try again");
                    Console.ReadLine();
                }
            }

            var serviceAddress = string.Format("http://localhost:{0}/AccountAuthenticator", serverPort);
            var endpointAddress = new EndpointAddress(new Uri(serviceAddress));
            var wsDualHttpBinding = new WSDualHttpBinding();
            wsDualHttpBinding.Security.Mode = WSDualHttpSecurityMode.Message;
            wsDualHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            var callbackHandler = new AuthenticationServiceHandler();
            var client = new AuthenticationServiceClient(new InstanceContext(callbackHandler), wsDualHttpBinding,
                endpointAddress);

            client.ClientCredentials.UserName.UserName = "redplane";
            client.ClientCredentials.UserName.Password = "fayaz1";
            client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My,
                X509FindType.FindBySubjectName, "localhost");
            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
                X509CertificateValidationMode.None;

            try
            {
                client.AuthenticateAccount("Akai", "Akai");
            }
            catch (Exception exceptionInfo)
            {
                Console.WriteLine("Error happended");
            }
            Console.ReadLine();
        }
    }
}