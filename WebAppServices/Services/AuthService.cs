using System;
using System.Collections.Generic;
using System.Text;
using WebApp.CoreModels.Models;

namespace WebAppServices.Services
{
    public class AuthService : IAuthService
    {
        public bool IsUserValid(AccountModel account) {

            //Hardcoded for now
            if (account.UserName == "wonder" && account.Password == "tree")
                return true;
            return false;
        }
    }
}
