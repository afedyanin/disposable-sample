using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace GetDate.ConsoleApp
{
    public class UnmanagedDatabaseState : DatabaseState
    {
        private SqlCommand _command;
        private IntPtr _unmanagedPointer;

        public override string GetDate()
        {
            var sqlDate = base.GetDate();
            if (_command == null)
            {
                _command = _connection.CreateCommand();
            }
            if (_unmanagedPointer == IntPtr.Zero)
            {
                _unmanagedPointer = Marshal.AllocHGlobal(100 * 1024 * 1024);
            }
            return sqlDate;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_command != null)
                {
                    _command.Dispose();
                    _command = null;
                }
            }
            if (_unmanagedPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_unmanagedPointer);
                _unmanagedPointer = IntPtr.Zero;
            }
            base.Dispose(disposing);
        }

        ~UnmanagedDatabaseState()
        {
            Dispose(false);
        }
    }
}