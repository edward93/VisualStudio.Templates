using $ext_safeprojectname$.DAL.Entities;

namespace $safeprojectname$.Services
{
    public interface IUserService : IEntityService<User>
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}