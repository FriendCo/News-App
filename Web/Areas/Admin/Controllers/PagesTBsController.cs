using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomainClass;
using Services.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PagesTBsController : Controller
    {
        private readonly IPageRepository _pageRepository;
        private readonly IGroupRepository _groupRepository;

       public PagesTBsController(IPageRepository pageRepository,IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _pageRepository = pageRepository;
        }

        // GET: Admin/PagesTBs
        public async Task<IActionResult> Index()
        {
            return View(await _pageRepository.GetAllPages());
        }

        // GET: Admin/PagesTBs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagesTB = await _pageRepository.GetPageByID(id.Value);
            if (pagesTB == null)
            {
                return NotFound();
            }

            return View(pagesTB);
        }

        // GET: Admin/PagesTBs/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Groups = new SelectList(await _groupRepository.GetAllGroups(), "GroupID", "Title");
            return View();
        }

        // POST: Admin/PagesTBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageID,Title,ShortDescription,Text,ImageName,CreateDate,Visist,Like,GroupID")] PagesTB pagesTB,IFormFile imgPage)
        {
            if (ModelState.IsValid)
            {
                pagesTB.Like = 0;
                pagesTB.Visist = 0;
                pagesTB.CreateDate = DateTime.Now;
                if (imgPage != null)
                {
                    pagesTB.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgPage.FileName);
                    string savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", pagesTB.ImageName);
                    using(var st = new FileStream(savepath, FileMode.Create))
                    {
                        imgPage.CopyTo(st);
                    }
                }
                _pageRepository.InsertPage(pagesTB);
                await _pageRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Groups = new SelectList(await _groupRepository.GetAllGroups(), "GroupID", "Title");
            return View(pagesTB);
        }

        // GET: Admin/PagesTBs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagesTB = await _pageRepository.GetPageByID(id.Value);
            if (pagesTB == null)
            {
                return NotFound();
            }
            ViewBag.Groups = new SelectList(await _groupRepository.GetAllGroups(), "GroupID", "Title");
            return View(pagesTB);
        }

        // POST: Admin/PagesTBs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageID,Title,ShortDescription,Text,ImageName,CreateDate,Visist,Like,GroupID")] PagesTB pagesTB,IFormFile imgPage)
        {
            if (id != pagesTB.PageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(imgPage != null)
                    {
                        pagesTB.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgPage.FileName);
                        string savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", pagesTB.ImageName);
                        System.IO.File.Delete(savepath);
                        using (var stream = new FileStream(savepath,FileMode.Create))
                        {
                            imgPage.CopyTo(stream);
                        }
                    }
                    _pageRepository.UpdatePage(pagesTB);
                    await _pageRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagesTBExists(pagesTB.PageID))
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
            ViewBag.Groups = new SelectList(await _groupRepository.GetAllGroups(), "GroupID", "Title");
            return View(pagesTB);
        }

        // GET: Admin/PagesTBs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagesTB = await _pageRepository.GetPageByID(id.Value);
            if (pagesTB == null)
            {
                return NotFound();
            }

            return View(pagesTB);
        }

        // POST: Admin/PagesTBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             var pagesTB = await _pageRepository.GetPageByID(id);
            if(pagesTB.ImageName != null)
            {
                string DeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", pagesTB.ImageName);
                System.IO.File.Delete(DeletePath);
            }
            _pageRepository.DeletePage(pagesTB);
            await _pageRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PagesTBExists(int id)
        {
            return _pageRepository.IsExist(id);
        }
    }
}
