﻿namespace DataAccessLibrary.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

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

        /// <summary>
        /// The SaveData.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="sql">The sql<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="T"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnection()))
            {
                return connection.Execute(sql, data);
            }
        }

        /// <summary>
        /// The GetID.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="sql">The sql<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="T"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetID<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnection()))
            {
                return connection.Execute(sql);
            }
        }

        internal static int GetID(int id, string sql)
        {
            using (IDbConnection connection = new SqlConnection(GetConnection()))
            {
                return connection.Execute(sql);
            }
        }
    }
}