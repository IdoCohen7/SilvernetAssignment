namespace Silvernet.Services
{
    public interface IJwtService
    {
        string GenerateToken(long userId, string email);
    }
}