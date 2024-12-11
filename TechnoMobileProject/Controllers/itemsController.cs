using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using TechnoMobileProject.Data;
using TechnoMobileProject.Models;

namespace TechnoMobileProject.Controllers
{
    public class itemsController : Controller
    {
        private readonly TechnoMobileProjectContext _context;

        public itemsController(TechnoMobileProjectContext context)
        {
            _context = context;
        }
        // GET: items1
        public async Task<IActionResult> Index()
        {
            string roles = HttpContext.Session.GetString("Role");
            if (roles != "admin")
            {
                return RedirectToAction("login","usersaccounts");
                }else
                return View(await _context.items.ToListAsync());
        }
        public async Task<IActionResult> List()
        {
            string roles = HttpContext.Session.GetString("Role");
            if (roles == "admin")
            {
                return RedirectToAction("login", "usersaccounts");
            }
            else
            {
                var orderedItems = await _context.items
                                                 .OrderBy(item => item.category) 
                                                 .ToListAsync();
                return View(orderedItems);
            }
        }

        // GET: items1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }
        public async Task<IActionResult> dash()
        {
            string roles = HttpContext.Session.GetString("Role");
            if (roles != "admin")
            {
                return RedirectToAction("login", "usersaccounts");
            }
            else { 
            {
                string sql = "";

                var builder = WebApplication.CreateBuilder();
                string conStr = builder.Configuration.GetConnectionString("TechnoDb");
                SqlConnection conn = new SqlConnection(conStr);

                SqlCommand comm;
                conn.Open();
                for (int i = 1; i <= 16; i++)
                {
                    sql = "SELECT COUNT(Id) FROM items WHERE category = " + i;
                    comm = new SqlCommand(sql, conn);
                    ViewData["d" + i] = (int)comm.ExecuteScalar();
                }
                return View();
            }
            }
        }

        public async Task<IActionResult> DetailsItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: items1/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,title,info,price,discount,pubdate,category,bookquantity")] items items)
        {
            {
                if (file != null)
                {
                    string filename = file.FileName;
                    //  string  ext = Path.GetExtension(file.FileName);
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    { await file.CopyToAsync(filestream); }

                    items.imgfile = filename;
                }

                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("items/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        [HttpPost("items/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("Id,title,info,price,discount,pubdate,category,bookquantity,imgfile")] items items)
        {
            if (id != items.Id)
            {
                return NotFound();
            }

            if (file != null)
            {
                string filename = file.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                items.imgfile = filename;
            }

            _context.Update(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(items);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: items1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var items = await _context.items.FindAsync(id);
            if (items != null)
            {
                _context.items.Remove(items);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool itemsExists(int id)
        {
            return _context.items.Any(e => e.Id == id);
        }
    }
}
