using System;
using System.Text;
using System.Data.SQLite;

namespace dotnetapp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
             SQLiteConnection.CreateFile("MyDatabase.sqlite");
             Console.WriteLine("Connecting to SQLite ... ");
             using (SQLiteConnection connection =new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
             {
                connection.Open();
                Console.WriteLine("Done.");
                 //drop table if exists
                Console.Write("drop table database if exists for resetting...");
                String sql="DROP TABLE IF EXISTS Employees";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                     command.ExecuteNonQuery();
                     Console.WriteLine("Done.");
                }

                // Create a Table 
                 Console.Write("Creating Employees table, press any key to continue...");
                 Console.ReadKey(true);
                 StringBuilder sb = new StringBuilder();
                 sb.Append("CREATE TABLE Employees ( ");
                 sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                 sb.Append(" Name VARCHAR(50), ");
                 sb.Append(" Location VARCHAR(50) ");
                 sb.Append("); ");
                 sql = sb.ToString();
                 using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        //command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }
                // INITIAL DATA
                Console.Write("Inserting initial data into table, press any key to continue...");
                sb.Clear();
                sb.Append("INSERT INTO Employees (Name, Location) VALUES ");
                sb.Append("(N'Jared', N'Australia'), ");
                sb.Append("(N'Nikita', N'India'), ");
                sb.Append("(N'Tom', N'Germany'); ");
                sql = sb.ToString();
                 using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        //command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }

                    // READ DATA
                    Console.WriteLine("Reading data from table, press any key to continue...");
                    Console.ReadKey(true);
                    sql = "SELECT Id, Name, Location FROM Employees;";
                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }

                  
             }
            }
             catch (SQLiteException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);        
            
        
        }
    }
}
