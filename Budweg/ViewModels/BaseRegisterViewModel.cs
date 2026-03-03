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

        private protected TEntity _currentEntity;
        public TEntity CurrentEntity
        {
            get
            {
                return _currentEntity;
            }
            set
            {
                if (_currentEntity == value) return;
                _currentEntity = value;
                OnPropertyChanged();
            }
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
