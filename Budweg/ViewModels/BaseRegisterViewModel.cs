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
        protected TEntity entity = new();
        protected BaseRepo<TEntity> entityRepo;

        private bool _isAdding = false;

        protected BaseRegisterViewModel(BaseRepo<TEntity> repo)
        {
            entityRepo = repo;
            CurrentEntity = entity;
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


        public void AddToRepo()
        {
            if (_isAdding) return;  // Ignore the second click

            _isAdding = true;
            try
            {
                if (CheckEntity())
                {
                    entityRepo.Add(entity);
                }
            }
            finally
            {
                _isAdding = false;
            }
        }
    }
}
