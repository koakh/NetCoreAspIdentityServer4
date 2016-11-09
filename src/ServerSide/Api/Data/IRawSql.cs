using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public interface IRawSql<T>
    {
        //Advanced Entity Framework Scenarios for an MVC Web Application (10 of 10)
        //http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/advanced-entity-framework-scenarios-for-an-mvc-web-application

        //Use the DbSet.SqlQuery method for queries that return entity types.The returned objects must be of the type expected by the DbSet object, and they are automatically tracked by the database context unless you turn tracking off. (See the following section about the AsNoTracking method.)
        IEnumerable<T> SqlQuery(string query, params object[] parameters);
        Task<IEnumerable<T>> SqlQueryAsync(string query, params object[] parameters);

        //Use the Database.SqlQuery method for queries that return types that aren't entities. The returned data isn't tracked by the database context, even if you use this method to retrieve entity types.
        //var SqlQuery(string query);

        //Use the Database.ExecuteSqlCommand for non-query commands.
        //SqlQuery(string query);
    }
}
