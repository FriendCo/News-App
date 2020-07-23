using DomainClass;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositorys
{
    public interface IPageRepository
    {
        Task<IEnumerable<PagesTB>> GetAllPages();
        Task<IEnumerable<PagesTB>> GetPagesByFilter(string q);
        Task<IEnumerable<PagesTB>> GetPagesByGroupID(int GroupId); 
        Task<PagesTB> GetPageByID(int PageID);
        Task<IEnumerable<PagesTB>> GetTopPages(int take=5);
        Task<IEnumerable<PagesTB>> GetTopLikePages(int take = 5);
        bool InsertPage(PagesTB page);
        bool DeletePage(Task<PagesTB> pages);
        bool DeletePage(int PageID);
        bool DeletePage(PagesTB pages);
        bool UpdatePage(PagesTB pages);
        bool IsExist(int PageID);
        Task<bool> Save();

    }
}
