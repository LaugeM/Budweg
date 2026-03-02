using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public abstract class RepoBase<TEntity>
    where TEntity : class, new()
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

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
