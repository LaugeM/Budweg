using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.ViewModels
{
    public abstract class BaseRegisterViewModel<TEntity>
    where TEntity : class, new()
    {

        public virtual TEntity CreateEntity()
        {
            return new TEntity();
        }
    }
}
