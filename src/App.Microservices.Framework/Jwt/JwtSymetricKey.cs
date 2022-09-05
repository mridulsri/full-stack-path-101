using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.Microservices.Framework.Jwt;
public class JwtSymetricKey
{
    public SecurityKey PrivateKey { get; }
    public SecurityKey PublicKey { get; set; }

    public SigningCredentials SigningCredentials { get; set; }

    public JwtSymetricKey(string secret)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var publicKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

        var signinCred = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        PrivateKey = signingKey; 
        PublicKey = publicKey;
        SigningCredentials = signinCred;
    }
}
