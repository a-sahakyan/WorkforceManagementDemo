using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkforceManagement.Domain.Concrete;
using WorkforceManagement.Domain.Abstract;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.WebUI.Controllers
{
   [AllowAnonymous]
    public class HomeController : Controller
    {
        IRepository<Employee> _rep;
        private readonly EFDbContext _context;

        public HomeController(EFDbContext context)
        {
            _context = context;
            _rep = new EFModelContext<Employee>(_context);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_rep.Model.ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthPage()
        {
            return View();
        }
    }
}