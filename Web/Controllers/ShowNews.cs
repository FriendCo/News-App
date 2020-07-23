using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainClass;
using Microsoft.AspNetCore.Mvc;
using Services.Repositorys;

namespace Web.Controllers
{
    public class ShowNews : Controller
    {
        private readonly IPageRepository _pageRepository;
        public ShowNews(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        [Route("ShowNews/{id}")]
        public async  Task<IActionResult> Index(int id)
        {
            var page = await _pageRepository.GetPageByID(id);
            page.Visist++;
            _pageRepository.UpdatePage(page);
            await _pageRepository.Save();
            return  View(page);
        }

        [Route("Like/{id}")]
        public async Task<IActionResult> Like(int id)
        {
            var page = await _pageRepository.GetPageByID(id);
            page.Like++;
            _pageRepository.UpdatePage(page);
            await _pageRepository.Save();
            return Redirect("/ShowNews/" + id);
        }

        [Route("Archive/{GroupID}")]
        public async Task<IActionResult> ShowGroupsArchive(int GroupID)
        {
            return View(await _pageRepository.GetPagesByGroupID(GroupID));
        }

      //  [Route("Search/{param}")]
        public async Task<IActionResult> Search(string param)
        {
            return View(await _pageRepository.GetPagesByFilter(param));
        }
    }
}
