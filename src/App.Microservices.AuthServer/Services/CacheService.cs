namespace Services.AuthServer.Services;

public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid UserId{ get; set; }
    public string Token { get; set; }

    public DateTime ExpireOn { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string AgentDetails { get; set; }
}
public class CacheService
{
    public IList<RefreshToken> tokenList = null;

    public CacheService()
    {
        tokenList = new List<RefreshToken>();
    }

    public RefreshToken GetById(Guid id)
    {
        return tokenList.FirstOrDefault(x => x.Id == id);
    }
}
