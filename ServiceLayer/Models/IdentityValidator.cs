using System;
using System.IdentityModel.Selectors;

namespace ServiceLayer.Models
{
    internal class IdentityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }

            if (!(userName.Equals("fayaz") && password.Equals("fayaz1")))
            {
                throw new Exception("Incorrect Username or Password");
            }
        }
    }
}