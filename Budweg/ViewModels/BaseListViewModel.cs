using Budweg.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Budweg.ViewModels
{
    public abstract class BaseListViewModel<TEntity> : SuperClassViewModel
    where TEntity : class, new()
    {
        public ObservableCollection<BaseViewModel<TEntity>> EntityVMOC { get; } = new ObservableCollection<BaseViewModel<TEntity>>();

        protected BaseRepo<TEntity> entityRepo;


        private protected TEntity _selectedEntity;
        public TEntity SelectedEntity
        {
            get
            {
                return _selectedEntity;
            }
            set
            {
                if (_selectedEntity == value) return;
                _selectedEntity = value;
                OnPropertyChanged();
            }
        }

        private BaseViewModel<TEntity>? _selectedEntityVM;
        public BaseViewModel<TEntity>? SelectedEntityVM
        {
            get => _selectedEntityVM;
            set
            {
                if (_selectedEntityVM == value) return;
                _selectedEntityVM = value;
                OnPropertyChanged();
            }
        }

        public BaseListViewModel(BaseRepo<TEntity> repo)
        {
            entityRepo = repo;
        }

        abstract protected void InitializeEntityOC();
    }
}
