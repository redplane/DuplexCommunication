using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace ServiceLayer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //const string serviceAddress = "http://localhost:27020/AccountAuthenticator";
            var serverPort = 0;

            while (true)
            {
                try
                {
                    Console.Write("Enter service port : ");
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

            // Create an isntance of service host which manages connection from clients.
            var serviceHost = new ServiceHost(typeof (AuthenticationService), new Uri(serviceAddress));

            // Create an instance of ServiceMetadataBehavior class to define what server will behave.
            var serviceMetadataBehavior = new ServiceMetadataBehavior();

            // Retrieve service credential from service host.
            var serviceCredentials = serviceHost.Description.Behaviors.Find<ServiceCredentials>();

            // Instance hasn't been found. Create a new one.
            if (serviceCredentials == null)
            {
                serviceCredentials = new ServiceCredentials();
                serviceHost.Description.Behaviors.Add(serviceCredentials);
            }

            // Update credential information.
            serviceCredentials.UserNameAuthentication.UserNamePasswordValidationMode =
                UserNamePasswordValidationMode.Custom;
            serviceCredentials.UserNameAuthentication.CustomUserNamePasswordValidator = new IdentityValidator();
            serviceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My,
                X509FindType.FindBySubjectName, "localhost");

            serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);
            serviceHost.AddServiceEndpoint(typeof (IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(),
                "mex");

            // Create an instance of BasicHttpBinding with default initialization.
            var wsDualHttpBinding = new WSDualHttpBinding();
            wsDualHttpBinding.Security.Mode = WSDualHttpSecurityMode.Message;
            wsDualHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            serviceHost.AddServiceEndpoint(typeof (IAuthenticationService), wsDualHttpBinding, serviceAddress);
            serviceHost.Open();

            Console.WriteLine("Service has started at port {0}. Press enter to terminate", serverPort);
            Console.ReadLine();
        }
    }
}