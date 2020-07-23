using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Repositorys;

namespace Web.ViewComponents
{
    public class ShowTopNewsComponent : ViewComponent
    {
        private readonly IPageRepository _pageRepository;

        public ShowTopNewsComponent(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowTopNewsComponent", await _pageRepository.GetTopPages()));
        }

    }
}
