using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Repositorys;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public HomeController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pageRepository.GetAllPages());
        }
    }
}
