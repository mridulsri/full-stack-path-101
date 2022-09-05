using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Microservices.AuthServer.Models.Domain
{
    public class AuthenticatedUser
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; }
    }
}
