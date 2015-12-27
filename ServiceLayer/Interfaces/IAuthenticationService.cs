using System.ServiceModel;
using SharedLayer.Models;

namespace ServiceLayer.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof (IAuthenticationCallback))]
    public interface IAuthenticationService
    {
        [OperationContract]
        ClientIdentity AuthenticateAccount(string userName, string password);
    }
}