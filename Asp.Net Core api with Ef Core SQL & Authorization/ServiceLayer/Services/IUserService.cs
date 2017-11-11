namespace $safeprojectname$.Services
{
    public interface IUserService : IEntityService
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}