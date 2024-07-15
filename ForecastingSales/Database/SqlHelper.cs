using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ForecastingSales.Database
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query)
        {
            var result = new List<Dictionary<string, object>>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var command = new SqlCommand(query, conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }
                        result.Add(row);
                    }
                }
            }

            return result;
        }
    }
}