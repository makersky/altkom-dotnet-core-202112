using Altkom.Shopper.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace Altkom.Shopper.IRepositories
{
    // Interfejs generyczny (szablon)
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Update(int id, JsonPatchDocument<TEntity> patchCustomer);
        void Remove(int id);
        bool Exists(int id);
    }
}
