using System;
using System.Threading.Tasks;
using $safeprojectname$.Infrastructure;
using $ext_safeprojectname$.DAL.GenericEntity;
using $ext_safeprojectname$.ServiceLayer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace $safeprojectname$.Controllers
{
    [Route("api/[controller]")]
    public class BaseRestFulController<T> : BaseController where T : Entity
    {
        protected readonly IGenericService<T> _genericService;
        public BaseRestFulController(ILoggerFactory loggerFactory, IOptions<AppSettings> settings, IGenericService<T> genericService, IHostingEnvironment env) : base(loggerFactory, settings, env)
        {
            _genericService = genericService;
        }

        private void AddUserInfoToModel(ref T model)
        {
            var currentUserId = TryGetUserId();
            model.CreatedBy = currentUserId;
            model.UpdatedBy = currentUserId;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return await MakeActionCall(async () =>
            {
                var result = await _genericService.GetAllEntitiesAsync();
                return result;
            });
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(long id)
        {
            return await MakeActionCall(async () =>
            {
                var result = await _genericService.GetByIdAsync(id);
                return result;
            });
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]T model)
        {
            return await MakeActionCall(async () =>
            {
                AddUserInfoToModel(ref model);

                await TryUpdateModelAsync(model);

                if (!ModelState.IsValid)
                {
                    throw new Exception(GetModelStateErrors());
                }

                var result = await _genericService.CreateAsync(model);
                return result;
            });
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody]T model)
        {
            return await MakeActionCall(async () =>
            {
                AddUserInfoToModel(ref model);

                await TryUpdateModelAsync(model);

                if (!ModelState.IsValid)
                {
                    throw new Exception(GetModelStateErrors());
                }

                var result = await _genericService.UpdateAsync(model);
                return result;
            });
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            return await MakeActionCall(async () =>
            {
                var result = await _genericService.RemoveEntityAsync(id);
                return result;
            });
        }

    }
}