using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFCoreDBContext _context;

        
        public HomeController(EFCoreDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = _context.Employees.Include(e => e.Department);
            return View(await result.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) { 
                return NotFound();
            }
            
            var res = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // populate departments for the dropdown
            ViewBag.DepartmentId = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "Name");
            return View();    
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Employee created successfully!";
                return RedirectToAction(nameof(Index));
            }

            // if we get here something failed validation; repopulate the department select list
            ViewBag.DepartmentId = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
