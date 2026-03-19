using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.ViewModels
{
    public abstract class BaseViewModel<TEntity> : SuperClassViewModel
    {
        public TEntity Entity { get; }

        public BaseViewModel(TEntity entity)
        {
            Entity = entity;
        }
    }
}
