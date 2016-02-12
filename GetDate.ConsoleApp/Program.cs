using System;

namespace GetDate.ConsoleApp
{
    class Program
    {
        //private static DatabaseState _DatabaseState;

        static void Main(string[] args)
        {
            Console.WriteLine("'g' to Get Date; 'gc' to Garbage Collect; 'x' to Exit");
            var command = "";
            while (command != "x")
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "g":
                        GetDate();
                        break;

                    case "gc":
                        GC.Collect();
                        break;
                }
            }
        }

        private static void GetDate()
        {
            using (var databaseState = new UnmanagedDatabaseState())
            //var databaseState = new UnmanagedDatabaseState();
            {
                Console.WriteLine(databaseState.GetDate());
            }
        }
    }
}
