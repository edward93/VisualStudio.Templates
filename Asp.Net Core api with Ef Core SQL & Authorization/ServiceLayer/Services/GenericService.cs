using $ext_safeprojectname$.DAL.GenericEntity;
using $safeprojectname$.Repositories;

namespace $safeprojectname$.Services
{
    public class GenericService<T> : EntityService<T>, IGenericService<T> where T : Entity
    {
        public GenericService(IEntityRepository<T> entityRepository) : base(entityRepository)
        {
        }
    }
}