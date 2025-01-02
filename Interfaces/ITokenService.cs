using bettersociety.Models;

namespace bettersociety.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
