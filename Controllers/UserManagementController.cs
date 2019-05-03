using System.Diagnostics;
using System.Threading.Tasks;
using dotnet_tutorial_app.Data;
using dotnet_tutorial_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace dotnet_tutorial_app.Controllers {
    public class UserManagementController : Controller {
        private readonly UserManagementContext _context;

        public UserManagementController (UserManagementContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index () {

            return View (await _context.Users.ToListAsync ());
        }

        // GET: UserManagement/Create
        public IActionResult Create () {
            return View ();
        }
        // POST: UserManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (
            [Bind ("Username, Password")] UserManagement user) {
            try {
                if (ModelState.IsValid) {
                    _context.Add (user);
                    await _context.SaveChangesAsync ();
                    return RedirectToAction (nameof (Index));
                }
            } catch (DbUpdateException /* ex */ ) {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError ("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View (user);
        }

        // GET: UserManagement/Edit/5
        public async Task<IActionResult> Edit (int? id) {
            if (id == null) {
                return NotFound ();
            }

            var user = await _context.Users.FindAsync (id);
            if (user == null) {
                return NotFound ();
            }
            return View (user);
        }

        // POST: UserManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName ("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerformed (int? id) {
            if (id == null) {
                return NotFound ();
            }
            var userToUpdate = await _context.Users.FirstOrDefaultAsync (u => u.UserManagementID == id);
            if (await TryUpdateModelAsync<UserManagement> (
                    userToUpdate,
                    "",
                    u => u.Username, u => u.Password)) {
                try {
                    await _context.SaveChangesAsync ();
                    return RedirectToAction (nameof (Index));
                } catch (DbUpdateException /* ex */ ) {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError ("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View (userToUpdate);
        }

        // GET: UserManagement/Delete/5
        public async Task<IActionResult> Delete (int? id, bool? saveChangesError = false) {

            if (id == null) {
                return NotFound ();
            }

            var user = await _context.Users
                .AsNoTracking ()
                .FirstOrDefaultAsync (u => u.UserManagementID == id);
            if (user == null) {
                return NotFound ();
            }

            if (saveChangesError.GetValueOrDefault ()) {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View (user);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id) {
            var user = await _context.Users.FindAsync (id);
            if (user == null) {
                return RedirectToAction (nameof (Index));
            }

            try {
                _context.Users.Remove (user);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            } catch (DbUpdateException /* ex */ ) {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction (nameof (Delete), new { id = id, saveChangesError = true });
            }
        }

    }
}