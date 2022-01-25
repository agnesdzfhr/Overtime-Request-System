using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        //public virtual int Delete(Entity entity)
        //{
        //    if (entity == null)
        //        throw new ArgumentException("entity");
        //    entities.Remove(entity);
        //    var result = myContext.SaveChanges();
        //    return result;
        //}

        public int Delete(Key key)
        {
            var entity = entities.Find(key);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            var respond = myContext.SaveChanges();
            return respond;
        }

        public virtual IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public virtual Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public virtual int Insert(Entity entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");
            entities.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public virtual int Update(Entity entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
