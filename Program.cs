using System;
using System.Data.SQLite;
using System.Text;

namespace dotnetapp {
    class Program {
        static void Main (string[] args) {
            try {
                Console.ResetColor ();
                SQLiteConnection.CreateFile ("MyDatabase.sqlite");
                Console.Write ("Connecting to SQLite... ");
                using (SQLiteConnection connection = new SQLiteConnection ("Data Source=MyDatabase.sqlite;Version=3;")) {
                    connection.Open ();
                    Console.WriteLine ("Done");
                    String sql;

                    //drop table if exists..not necessary because the db file is rewrited
                    /* Console.Write ("Drop database table Employees if exists...");
                     sql = "DROP TABLE IF EXISTS Employees";
                    using (SQLiteCommand command = new SQLiteCommand (sql, connection)) {
                        command.ExecuteNonQuery ();
                        Console.WriteLine ("Done.");
                    }
                    */

                    // Create a Table 
                    Console.Write ("Creating Employees table... ");
                    StringBuilder sb = new StringBuilder ();
                    sb.Append ("CREATE TABLE Employees ( ");
                    sb.Append (" Id INTEGER PRIMARY KEY AUTOINCREMENT, ");
                    sb.Append (" Name VARCHAR(50), ");
                    sb.Append (" Location VARCHAR(50) ");
                    sb.Append ("); ");
                    sql = sb.ToString ();
                    using (SQLiteCommand command = new SQLiteCommand (sql, connection)) {
                        command.ExecuteNonQuery ();
                        Console.WriteLine ("Done");
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine ("Welcome in the database interactive console menu!");
                    Console.ResetColor ();
                    int action = printMenu ();
                    while (action != 6) {
                        Console.ForegroundColor = ConsoleColor.Green;

                        // INITIAL DATA
                        if (action == 1) {
                            Console.WriteLine ("Inserting initial data into table, Press any key for the result... ");
                            Console.ReadKey (true);
                            sb.Clear ();
                            sb.Append ("INSERT INTO Employees (Name, Location) VALUES ");
                            sb.Append ("('Jared', 'Australia'), ");
                            sb.Append ("('Nikita', 'India'), ");
                            sb.Append ("('Tom', 'Germany'); ");
                            sql = sb.ToString ();
                            using (SQLiteCommand command = new SQLiteCommand (sql, connection)) {
                                command.ExecuteNonQuery ();
                                Console.WriteLine ("Done");
                            }
                        }
                        if (action == 2) {
                            // INSERT employee with prepare statement
                            Console.WriteLine ("Inserting a new row into table, Press any key for the result... ");
                            Console.ReadKey (true);
                            sb.Clear ();
                            sb.Append ("INSERT INTO Employees (Name, Location) ");
                            sb.Append ("VALUES (@name, @location);");
                            sql = sb.ToString ();
                            using (SQLiteCommand command = new SQLiteCommand (sql, connection)) {
                                command.Parameters.AddWithValue ("@name", "Jake");
                                command.Parameters.AddWithValue ("@location", "United States");
                                int rowsAffected = command.ExecuteNonQuery ();
                                Console.WriteLine (rowsAffected + " row(s) inserted");
                            }
                        }
                        if (action == 3) {
                            // READ DATA
                            Console.WriteLine ("Reading data from Employees table, Press any key for the result...... ");
                            Console.ReadKey (true);
                        }

                        if (action == 4) {
                            // UPDATE DATA
                            Console.Write ("Updating data from Employees table, insert the employee Name: ");
                            string name = Console.ReadLine ();
                            Console.Write ("Insert the new location for that employees with name '" + name + "' :");
                            string newLocation = Console.ReadLine ();
                            Console.WriteLine ("Updating data from Employees table, Press any key for the result...... ");
                            Console.ReadKey (true);
                            sb.Clear ();
                            sb.Append ("UPDATE Employees SET Location = @location WHERE Name = @name");
                            sql = sb.ToString ();
                            using (SQLiteCommand command = new SQLiteCommand (sql, connection)) {
                                command.Parameters.AddWithValue ("@name", name);
                                command.Parameters.AddWithValue ("@location", newLocation);
                                int rowsAffected = command.ExecuteNonQuery ();
                                Console.WriteLine (rowsAffected + " row(s) updated");
                            }

                        }

                        if (action == 5) {
                            // DELETE DATA
                            Console.Write ("Deleting data from Employees table, insert the employee Name: ");
                            string name = Console.ReadLine ();
                            Console.WriteLine ("Deleting data from Employees table, Press any key for the result...... ");
                            Console.ReadKey (true);
                            sb.Clear ();
                            sb.Append ("DELETE FROM Employees WHERE Name = @name;");
                            sql = sb.ToString ();
                            using (SQLiteCommand command = new SQLiteCommand (sql, connection)) {
                                command.Parameters.AddWithValue ("@name", name);
                                int rowsAffected = command.ExecuteNonQuery ();
                                Console.WriteLine (rowsAffected + " row(s) deleted");
                            }

                        }
                        printEmployeesTable (connection);
                        action = printMenu ();
                    }
                }
            } catch (SQLiteException e) {
                Console.WriteLine (e.ToString ());
            }

            Console.WriteLine ("All done. Press any key to finish program...");
            Console.ReadKey (true);

        }

        static int printMenu () {
            Console.ResetColor ();
            Console.WriteLine ("\n---------------------------------MENU--------------------------------");
            Console.WriteLine ("1-INSERT three record in Employees table (Jared, Nikita and Tom)");
            Console.WriteLine ("2-INSERT Jake employee with United States location in Employees table");
            Console.WriteLine ("3-SELECT all records in Employees table");
            Console.WriteLine ("4-UPDATE a record in Employees table");
            Console.WriteLine ("5-DELETE a record in Employees table");
            Console.WriteLine ("6-EXIT");
            Console.WriteLine ("---------------------------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write ("Choose the action number that you want to perform:");
            string val = Console.ReadLine ();
            Console.ResetColor ();
            return Convert.ToInt32 (val);
        }

        static void printEmployeesTable (SQLiteConnection connection) {
            string sql = "SELECT Id, Name, Location FROM Employees;";
            using (SQLiteCommand command = new SQLiteCommand (sql, connection)) {

                using (SQLiteDataReader reader = command.ExecuteReader ()) {
                    if (!reader.HasRows) {
                        Console.WriteLine ("The Employees table is empty! ");
                        return;
                    }

                    Console.WriteLine ("--------------------------");
                    Console.WriteLine ("ID |  NAME  | LOCATION   |");
                    while (reader.Read ()) {
                        Console.WriteLine ("{0} {1} {2}", reader.GetInt32 (0), reader.GetString (1), reader.GetString (2));
                    }
                    Console.WriteLine ("--------------------------");

                }
            }

            return;
        }
    }

}