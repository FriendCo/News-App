using DomainClass;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages;

namespace Services.Repositorys
{
    public interface IGroupRepository
    {
        Task<IEnumerable<PageGroups>> GetAllGroups();
        IEnumerable<PageGroups> GetAllgroups();
        Task<PageGroups> GetGroupID(int GroupID);
        IEnumerable<GroupVM> GetGroupVM();
        bool InsertGroup(PageGroups groups);
        bool UpdateGroup(PageGroups groups);
        bool DeleteGroup(Task<PageGroups> groups);
        bool DeleteGroup(PageGroups groups);
        bool DeleteGroup(int GroupID);

        bool IsExist(int GroupID);
        Task<bool> Save();
    }
}
