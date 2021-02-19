namespace DataAccessLibrary.DataAccess
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    /// <summary>
    /// Defines the <see cref="SqlDataAccess" />.
    /// </summary>
    public static class SqlDataAccess
    {
        /// <summary>
        /// The GetConnection.
        /// </summary>
        /// <param name="connectionName">The connectionName<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetConnection(string connectionName = "UserDetailsCaptureDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        /// <summary>
        /// The LoadData.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="sql">The sql<see cref="string"/>.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection connection = new SqlConnection(GetConnection()))
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        public static IEnumerable<T> CheckEmail<T>(string sql)
        {
            using (IDbConnection connection = new SqlConnection(GetConnection()))
            {
                return connection.Query<T>(sql);
            }
        }

        /// <summary>
        /// The SaveData.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="sql">The sql<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="T"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static Task<int> SaveDataAsync<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnection()))
            {
                return Task.FromResult(connection.ExecuteScalar<int>(sql, data));
            }
        }

        public static int UpdateData<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnection()))
            {
                return connection.Execute(sql, data);
            }
        }
    }
}