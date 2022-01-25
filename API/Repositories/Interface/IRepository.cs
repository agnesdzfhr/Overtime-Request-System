using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    //Entity mewakili nama model, kita ganti Entitynya adalah class
    public interface IRepository<Entity, Key> where Entity : class
    {
        IEnumerable<Entity> Get();
        Entity Get(Key key); //Key ini nanti akan mewakili tipe data key dari setiap class
        int Insert(Entity entity);
        int Update(Entity entity);
        //int Delete(Entity entity);
        int Delete(Key key);
    }
}
