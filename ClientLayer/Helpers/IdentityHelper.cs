using SharedLayer.Models;

namespace ClientLayer.Helpers
{
    public class IdentityHelper
    {
        private static IdentityHelper _instance;

        public static IdentityHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IdentityHelper();

                return _instance;
            }
        }

        public ClientIdentity Identity { get; set; }
    }
}