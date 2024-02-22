using University.Entity.Entities;
public interface IAuthService
{
    Task<string> Authenticate(User user);
}