using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DL.Context;
using DomainClass;
using Services.Repositorys;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageGroupsController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public PageGroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        // GET: Admin/PageGroups
        public async Task<IActionResult> Index()
        {
            return View(await _groupRepository.GetAllGroups());
        }

        // GET: Admin/PageGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroups = await _groupRepository.GetGroupID(id.Value);
            if (pageGroups == null)
            {
                return NotFound();
            }

            return View(pageGroups);
        }

        // GET: Admin/PageGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PageGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupID,Title,Name")] PageGroups pageGroups)
        {
            if (ModelState.IsValid)
            {
                _groupRepository.InsertGroup(pageGroups);
                await _groupRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(pageGroups);
        }

        // GET: Admin/PageGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroups = await _groupRepository.GetGroupID(id.Value);
            if (pageGroups == null)
            {
                return NotFound();
            }
            return View(pageGroups);
        }

        // POST: Admin/PageGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupID,Title,Name")] PageGroups pageGroups)
        {
            if (id != pageGroups.GroupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _groupRepository.UpdateGroup(pageGroups);
                    await _groupRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageGroupsExists(pageGroups.GroupID))
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
            return View(pageGroups);
        }

        // GET: Admin/PageGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroups = await _groupRepository.GetGroupID(id.Value);
            if (pageGroups == null)
            {
                return NotFound();
            }

            return View(pageGroups);
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageGroups = await _groupRepository.GetGroupID(id);
            await _groupRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageGroupsExists(int id)
        {
            return _groupRepository.IsExist(id);
        }
    }
}
