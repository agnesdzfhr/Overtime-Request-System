using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("{key}")]
        public virtual ActionResult<Entity> Get(Key key)
        {
            var result = repository.Get(key);
            return Ok(result);
        }

        [HttpPost]
        public virtual ActionResult<Entity> Insert(Entity entity)
        {
            var result = repository.Insert(entity);
            return Ok(result);
        }

        [HttpPut]
        public virtual ActionResult<Entity> Update(Entity entity)
        {
            try
            {
                var result = repository.Update(entity);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete]
        //public virtual ActionResult<Entity> Delete(Entity entity)
        //{
        //    var result = repository.Delete(entity);
        //    return Ok(result);
        //}

        [HttpDelete("{key}")]
        public virtual ActionResult<Entity> Delete(Key key)
        {
            var result = repository.Delete(key);
            return Ok(result);
        }
    }
}
