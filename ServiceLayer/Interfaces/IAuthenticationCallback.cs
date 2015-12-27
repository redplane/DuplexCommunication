using System.ServiceModel;

namespace ServiceLayer.Interfaces
{
    [ServiceContract]
    public interface IAuthenticationCallback
    {
        [OperationContract]
        void OnClientAuthenticationDone(bool success);
    }
}