using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnoMobileProject.Data;
using TechnoMobileProject.Models;

namespace TechnoMobileProject.Controllers
{
    public class usersaccountsController : Controller
    {
        private readonly TechnoMobileProjectContext _context;

        public usersaccountsController(TechnoMobileProjectContext context)
        {
            _context = context;
        }
        public IActionResult login()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Name"))
                return View();
            else
            {
                string na = HttpContext.Request.Cookies["Name"].ToString();
                string ro = HttpContext.Request.Cookies["Role"].ToString();
                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", ro);

                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost, ActionName("login")]

        public async Task<IActionResult> login(string na, string pa, string auto)
        {
            var ur = await _context.usersaccounts.FromSqlRaw("SELECT * FROM usersaccounts where name ='" + na + "' and  pass ='" + pa + "' ").FirstOrDefaultAsync();

            if (ur != null)
            {
                int id = ur.Id;
                string na1 = ur.name;
                string ro = ur.role;
                string pass = ur.pass;

                HttpContext.Session.SetString("userid", id.ToString());
                HttpContext.Session.SetString("Name", na1);
                HttpContext.Session.SetString("Role", ro);
                HttpContext.Session.SetString("password", pass);

                if (auto == "on")
                {
                    HttpContext.Response.Cookies.Append("Name", na1);
                    HttpContext.Response.Cookies.Append("Role", ro);
                }

                if (pa == pass)
                {
                    TempData["Message"] = null; // Clear the message
                    if (ro == "customer")
                        return RedirectToAction("Index");
                    else if (ro == "admin")
                        return RedirectToAction("usersaccounts", "Login");
                    else
                        return View();
                }
                else
                {
                    TempData["Message"] = "Incorrect password.";
                    return View();
                }
            }
            else
            {
                TempData["Message"] = "Incorrect username or not found.";
                return View();
            }
        }
        public IActionResult Registration()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Registration(usersaccounts cli, string matchingpassword, int age)
        {
            if (age < 12 || age > 90)
            {
                ViewData["Message"] = "Age must be between 12 and 90.";
                return View();
            }
            if (cli.pass != matchingpassword)
            {
                ViewData["Message"] = "passwords do not match";
                return View();
            }

            var isExisting = _context.usersaccounts.FirstOrDefault(u => u.name == cli.name);
            if (isExisting != null)
            {
                ViewData["Message"] = "username is already taken try another username";
            }

            else
            {

                cli.role = "customer";
                _context.usersaccounts.Add(cli);
                _context.SaveChanges();
                ViewData["Message"] = "Registration is successful";
                int id = cli.Id;
                string na1 = cli.name;
                string ro = cli.role;
                HttpContext.Session.SetString("Userid", Convert.ToString(id));
                HttpContext.Session.SetString("Name", na1);
                HttpContext.Session.SetString("Role", ro);

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.Session.GetString("Name");


            ViewData["message"] = "Welcome Admin : " + userName;

            // Return the view with the list of users
            return View(await _context.usersaccounts.ToListAsync());
        }
        public IActionResult Addadmin(usersaccounts cli)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Addadmin(usersaccounts cle, string matchingpassword)
        {

            if (cle.pass != matchingpassword)
            {
                ViewData["Message"] = "Passwords do not match.";
                return View();
            }

            // Check if username already exists
            var isExisting = _context.usersaccounts.FirstOrDefault(u => u.name == cle.name);
            if (isExisting != null)
            {
                ViewData["Message"] = "Username is already taken, try another username.";
                return View();
            }

            cle.role = "admin";
            _context.usersaccounts.Add(cle);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Admin added successfully.";

            return RedirectToAction(nameof(Index)); // Redirect after successful addition
        }
        public IActionResult Users_search()
        {
            HttpContext.Session.LoadAsync();
            string role = HttpContext.Session.GetString("Role");
            if (role != "admin")
            {
                return RedirectToAction("login", "usersaccounts");
            }
            usersaccounts users = new usersaccounts();
            return View(users);
            }
        [HttpPost]
        public async Task<IActionResult> Users_Search(string name) {
            HttpContext.Session.LoadAsync();
            string role = HttpContext.Session.GetString("Role");
            if (role != "admin")
            {
                return RedirectToAction("login", "usersaccounts");
            }
            var getnames = await _context.usersaccounts.FromSqlRaw("select * from where name='" + name + "'").FirstOrDefaultAsync();
            return View(getnames);

        }


        // GET: usersaccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersaccounts == null)
            {
                return NotFound();
            }

            return View(usersaccounts);
        }

        // GET: usersaccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: usersaccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,pass,role")] usersaccounts usersaccounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersaccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersaccounts);
        }

        // GET: usersaccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts.FindAsync(id);
            if (usersaccounts == null)
            {
                return NotFound();
            }
            return View(usersaccounts);
        }

        // POST: usersaccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role")] usersaccounts usersaccounts)
        {
            if (id != usersaccounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersaccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usersaccountsExists(usersaccounts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usersaccounts);
        }

        // GET: usersaccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersaccounts == null)
            {
                return NotFound();
            }

            return View(usersaccounts);
        }

        // POST: usersaccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersaccounts = await _context.usersaccounts.FindAsync(id);
            if (usersaccounts != null)
            {
                _context.usersaccounts.Remove(usersaccounts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usersaccountsExists(int id)
        {
            return _context.usersaccounts.Any(e => e.Id == id);
        }
    }
}
