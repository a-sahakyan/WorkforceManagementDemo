using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkforceManagement.Domain.Concrete;

namespace WorkforceManagement.WebUI.Controllers
{
   [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly EFDbContext _context;

        public HomeController(EFDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}