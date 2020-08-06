using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectJavelin.Models;

namespace ProjectJavelin.Controllers
{
    public class StatsController : Controller
    {
        private readonly xikfmwkwContext _context;

        public StatsController(xikfmwkwContext context)
        {
            _context = context;
        }

        // GET: Stats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stats.ToListAsync());
        }

        
    }
}
