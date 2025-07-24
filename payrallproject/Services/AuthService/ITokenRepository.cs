using payrallproject.Models.Domains;

namespace payrallproject.Services.AuthService
{
    public interface ITokenRepository
    {
        string CreateJwtToken(User user, List<string> roles);
    }
}
