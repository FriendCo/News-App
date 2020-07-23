using DL.Context;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Services.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PageServices : IPageRepository
    {
        NewsContext _db;

        public PageServices(NewsContext context)
        {
            _db = context;
        }

        public bool DeletePage(Task<PagesTB> pages)
        {
            try
            {
                _db.Remove(pages);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePage(int PageID)
        {
            try
            {
                var page = GetPageByID(PageID);
                DeletePage(page);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePage(PagesTB pages)
        {
            try
            {
                _db.Remove(pages);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<PagesTB>> GetAllPages()
        {
            return await _db.Pages.ToListAsync();
        }

        public async Task<PagesTB> GetPageByID(int PageID)
        {
            return await _db.Pages.FindAsync(PageID);
        }

        public async Task<IEnumerable<PagesTB>> GetPagesByFilter(string q)
        {
            return await _db.Pages.Where(p => p.Title.Contains(q) || p.Text.Contains(q) || p.ShortDescription.Contains(q)).ToListAsync();
        }

        public async Task<IEnumerable<PagesTB>> GetPagesByGroupID(int GroupId)
        {
            return await _db.Pages.Where(p => p.GroupID == GroupId).ToListAsync();
        }

        public async Task<IEnumerable<PagesTB>> GetTopLikePages(int take = 5)
        {
            return await _db.Pages.OrderByDescending(p => p.Like).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<PagesTB>> GetTopPages(int take = 5)
        {
            return await _db.Pages.OrderByDescending(p => p.Visist).Take(take).ToListAsync();
        }

        public bool InsertPage(PagesTB page)
        {
            try
            {
                _db.Add(page);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool IsExist(int PageID)
        {
            return (_db.Pages.Any(p => p.PageID == PageID)) ? true : false;
        }

        public async Task<bool> Save()
        {
          await _db.SaveChangesAsync();
           return true;
        }

        public bool UpdatePage(PagesTB pages)
        {
            try
            {
                _db.Update(pages);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
