# IDisposable best practices


### 1. Dispose of IDisposable objects as soon as you can

'''C#

    using (var state = new DatabaseState())
    {
    	state.GetDate().Dump();
    }

	...   

    var state = new DatabaseState();
    try
    {
	    state.GetDate().Dump();
    }
	finally
    {
	    state.Dispose();
    }
'''
   
### 2. If you use IDisposable objects as instance fields, implement IDisposable

'''C#

    public class DatabaseState : IDisposable
    {
	    private SqlConnection _connection;
	    
		public void Dispose()
	    {
		    Dispose(true);
    		GC.SuppressFinalize(this);
    	}

	...

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (_connection != null)
			{
				_connection.Dispose();
				_connection = null;
			}
		}
	}

'''

### 3. Allow Dispose() to be called multiple times and don't throw exceptions

'''C#

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (_connection != null)
			{
				_connection.Dispose();
				_connection = null;
			}
		}
	}

'''

### 4. Implement IDisposable to support disposing resources in a class hierarchy

'''C#

	public class DatabaseState : IDisposable
	{
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
	...

'''
   
### 5. If you use unmanaged resources, declare a finalizer which cleans them up 

'''C#

	~UnmanagedDatabaseState()
	{
		Dispose(false);
	}
	
	protected override void Dispose(bool disposing)
	{
		//managed resources...
		
		if (_unmanagedPointer != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(_unmanagedPointer);
			_unmanagedPointer = IntPtr.Zero;
		}
		base.Dispose(disposing);
	}
'''

### 6. Enable Code Analysis with CA2000 enabled – but don’t rely on it

CA2000: Dispose objects before losing scope
 
### 7. If you implement an interface and use IDisposable fields, extend your interface from IDisposable

'''C#

	public class BookFeedRepository : IBookFeedRepository
	{
		private BookFeedContext _context;
		//...
	}
	public interface IBookFeedRepository : IDisposable
	{
		//...
	}

'''
### 8. If you implement IDisposable, don’t implement it explicitly

'''C#
	
	public class DifficultToDiscover : IDisposable
	{
		void IDisposable.Dispose()
		{
			//...
		}
	}
	
	public class EasyToDiscover : IDisposable
	{
		public void Dispose()
		{
			//...
		}
	}

'''

#### IDisposable Best Practices for C# Developers 

https://www.pluralsight.com/courses/idisposable-best-practices-csharp-developers

