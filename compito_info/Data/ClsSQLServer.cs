using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Data.SqlClient;


    public class ClsSQLServer
     {
    private string _connectionString;
    private SqlConnection _connection;


        public ClsSQLServer()
        {
            _connectionString = DbConfig.ConnectionString;
            _connection = new SqlConnection(_connectionString);
        }
   

    public ClsSQLServer(string server, string database, string username, string password)
    {
        _connectionString = $@"Data Source={server};Initial Catalog={database};User ID={username};Password={password};Connect Timeout=30";
        _connection = new SqlConnection(_connectionString);
    }

    public ClsSQLServer(string connString)
    {
        _connectionString = connString;
        _connection = new SqlConnection(_connectionString);
    }

    private void OpenConnection() 
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();
    }
    private void CloseConnection() 
    {
        if (_connection.State != ConnectionState.Closed)
            _connection.Close();
    }

    public DataTable ExecuteQuery(string query) 
    { 
        DataTable dt = new DataTable();

        try
        {
            OpenConnection();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, _connection))
                adapter.Fill(dt);
        }
        finally 
        {
            CloseConnection();
        }
        return dt; 
    }
    public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters) 
    {
        DataTable dt = new DataTable();
        try
        {
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                AddParameters(cmd, parameters);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    adapter.Fill(dt);
            }
        }
        finally { CloseConnection(); }
        return dt;
    }

    public int ExecuteNonQuery(string query)
    {
        int rows = 0;
        try
        {
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(query, _connection))
                rows = cmd.ExecuteNonQuery();
        }
        finally
        {
            CloseConnection();
        }
        return rows; 
    }
    public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
    {
        int rows = 0;
        try
        {
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                AddParameters(cmd, parameters);
                rows = cmd.ExecuteNonQuery();
            }
        }
        finally { CloseConnection(); }
        return rows;
    }

    public object ExecuteScalar(string query)
    {
        object res = null;

        try
        {
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(query, _connection))
                res = cmd.ExecuteScalar();
        }
        finally
        {
            CloseConnection();
        }
        return res; 
    }
    public object ExecuteScalar(string query, Dictionary<string, object> parameters)
    {
        object res = null;

        try
        {
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                AddParameters(cmd, parameters);
                res = cmd.ExecuteScalar();
            }
        }
        finally
        {
            CloseConnection();
        }
        return res;
    }
    public bool TestConnection()
    {
        try
        {
            OpenConnection();
            CloseConnection();
            return true;
        }
        catch { return false; }
    }
    private void AddParameters(SqlCommand cmd, Dictionary<string, object> parameters)
    {
        if (parameters == null)
            return;
        foreach (var p in parameters)
            cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
    }
}

