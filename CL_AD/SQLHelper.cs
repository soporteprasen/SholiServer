using System.Data;
using Microsoft.Data.SqlClient;

public static class SqlHelper
{
    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parameters)
    {
        using var command = new SqlCommand(commandText, connection)
        {
            CommandType = commandType
        };

        if (parameters != null)
            command.Parameters.AddRange(parameters);

        if (connection.State != ConnectionState.Open)
            connection.Open();

        // Comando ejecutado y retorno del reader
        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parameters)
    {
        using var command = new SqlCommand(commandText, connection)
        {
            CommandType = commandType
        };

        if (parameters != null)
            command.Parameters.AddRange(parameters);

        if (connection.State != ConnectionState.Open)
            connection.Open();

        return command.ExecuteNonQuery();
    }

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parameters)
    {
        using var command = new SqlCommand(commandText, connection)
        {
            CommandType = commandType
        };

        if (parameters != null)
            command.Parameters.AddRange(parameters);

        if (connection.State != ConnectionState.Open)
            connection.Open();

        return command.ExecuteScalar();
    }
}
