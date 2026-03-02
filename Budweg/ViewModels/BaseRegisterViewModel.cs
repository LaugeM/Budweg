using Budweg.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.ViewModels
{
    public abstract class BaseRegisterViewModel<TEntity> : SuperClassViewModel
    where TEntity : class, new()
    {
        private TEntity entity = new();
        protected BaseRepo<TEntity> entityRepo;

        protected BaseRegisterViewModel(BaseRepo<TEntity> repo)
        {
            entityRepo = repo;
        }

        public abstract bool CheckEntity();


        protected void AddToRepo()
        {
            if (CheckEntity())
            {
                entityRepo.Add(entity);
            }
        }
    }
}
