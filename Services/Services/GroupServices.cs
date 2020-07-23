using DL.Context;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Services.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages;

namespace Services.Services
{
    public class GroupServices : IGroupRepository
    {
        NewsContext _db;
        public GroupServices(NewsContext context)
        {
            _db = context;
        }
        public bool DeleteGroup(Task<PageGroups> groups)
        {
            try
            {
                _db.Remove(groups);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool DeleteGroup(PageGroups groups)
        {
            try
            {
                _db.Remove(groups);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteGroup(int GroupID)
        {
            try
            {
                var group = GetGroupID(GroupID);
                DeleteGroup(group);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<PageGroups>> GetAllGroups()
        {
            return await _db.PageGroups.ToListAsync();
        }

        public IEnumerable<PageGroups> GetAllgroups()
        {
            return _db.PageGroups.ToList();
        }

        public async Task<PageGroups> GetGroupID(int GroupID)
        {
            return await _db.PageGroups.FindAsync(GroupID);
        }

        public IEnumerable<GroupVM> GetGroupVM()
        {
            return _db.PageGroups.Select(g => new GroupVM
            {
                GroupId = g.GroupID,
                Count = g.Pages.Count,
                Title = g.Title
            }).ToList();
        }

        public bool InsertGroup(PageGroups groups)
        {
            try
            {
                _db.Add(groups);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsExist(int GroupID)
        {
            return (_db.PageGroups.Any(g => g.GroupID == GroupID)) ? true : false;
        }

        public async Task<bool> Save()
        {
            await _db.SaveChangesAsync();
            return true;
        }

        public bool UpdateGroup(PageGroups groups)
        {
            try
            {
                _db.Update(groups);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
