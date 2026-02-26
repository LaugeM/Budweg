using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public abstract class RepoBase<TEntity>
    where TEntity : class
    {
        protected readonly string ConnectionString;
        protected List<TEntity> entities;

        protected RepoBase()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            entities = new List<TEntity>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }
    }
}
