using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Bogus;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Shopper.FakeRepositories
{
    public class FakeEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly ICollection<TEntity> entities;

        public FakeEntityRepository(Faker<TEntity> faker)
        {
            entities = faker.Generate(100);
        }

        public virtual void Add(TEntity entity)
        {
            int lastId = entities.Max(c => c.Id);

            entity.Id = ++lastId;

            entities.Add(entity);
        }

        public virtual bool Exists(int id)
        {
            return entities.Any(e => e.Id == id);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return entities;
        }

        public virtual TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public virtual void Remove(int id)
        {
            entities.Remove(Get(id));
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(int id, JsonPatchDocument<TEntity> patchEntity)
        {
            TEntity entity = Get(id);

            patchEntity.ApplyTo(entity);
        }
    }
}
