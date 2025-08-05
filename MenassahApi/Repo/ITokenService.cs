namespace MenassahApi.Repo
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string username, List<string> roles);
    }
}
