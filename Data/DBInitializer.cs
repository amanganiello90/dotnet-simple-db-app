using System;
using System.Linq;
using dotnet_tutorial_app.Models;

namespace dotnet_tutorial_app.Data {
    public class DBInitializer {
        public static void Initialize (UserManagementContext context) {
            context.Database.EnsureDeleted ();
            context.Database.EnsureCreated ();

            // Look for any students.
            //if (context.Users.Any ()) {
            //    return; // DB has been seeded
            // }

            var users = new UserManagement[] {
                new UserManagement { Username = "User1", Password = "changeme1" },
                new UserManagement { Username = "User2", Password = "changexxx" },
            };
            foreach (UserManagement s in users) {
                context.Users.Add (s);
            }
            context.SaveChanges ();

        }
    }
}