﻿using System.Data.SqlClient;

namespace MealsApi.Utility
{
    public static class SqlHelper
    {
        public static void ExecuteProcedure(string connString, string procName,
            params SqlParameter[] paramters)
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (paramters != null)
                    {
                        command.Parameters.AddRange(paramters);
                    }
                    sqlConnection.Open();
                    command.ExecuteScalar();
                }
            }
        }

        public static TData ExtecuteProcedureOrQueryReturnData<TData>(string connString, string procNameOrSqlQuery,
            Func<SqlDataReader, TData> translator, bool isProc = true,
            params SqlParameter[] parameters)
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    if(isProc)
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = procNameOrSqlQuery;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        TData elements;
                        try
                        {
                            elements = translator(reader);
                        }
                        finally
                        {
                            while (reader.NextResult())
                            { }
                        }
                        return elements;
                    }
                }
            }
        }

        #region Get Values from Sql Data Reader
        public static string GetNullableString(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? null : Convert.ToString(reader[colName]);
        }

        public static int GetNullableInt32(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToInt32(reader[colName]);
        }

        public static float GetNullableFloat(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToSingle(reader[colName].ToString()); 
        }

        public static bool GetBoolean(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? default(bool) : Convert.ToBoolean(reader[colName]);
        }

        public static bool IsColumnExists(this System.Data.IDataRecord dr, string colName)
        {
            try
            {
                return (dr.GetOrdinal(colName) >= 0);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
