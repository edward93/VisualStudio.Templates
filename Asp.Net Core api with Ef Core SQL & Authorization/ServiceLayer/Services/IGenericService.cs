using $ext_safeprojectname$.DAL.GenericEntity;

namespace $safeprojectname$.Services
{
    public interface IGenericService<T> : IEntityService<T> where T : Entity
    {
    }
}