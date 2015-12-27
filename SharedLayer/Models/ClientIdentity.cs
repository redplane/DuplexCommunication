using System.Runtime.Serialization;
using SharedLayer.Interfaces;

namespace SharedLayer.Models
{
    [DataContract]
    public class ClientIdentity : IClientIdentity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string SecretKey { get; set; }
    }
}