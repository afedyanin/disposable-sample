using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GetDate.ConsoleApp
{
    public class DatabaseState : IDisposable
    {
        protected SqlConnection _connection;

        public virtual string GetDate()
        {
            if (_disposed)
                throw new ObjectDisposedException("DatabaseState");

            if (_connection == null)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["master"];
                _connection = new SqlConnection(connectionString.ConnectionString);
                _connection.Open();
            }
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT getdate()";
                return command.ExecuteScalar().ToString();
            }
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
                _disposed = true;
            }
        }
    }
}
