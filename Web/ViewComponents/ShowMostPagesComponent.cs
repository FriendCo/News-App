using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Repositorys;

namespace Web.ViewComponents
{
    public class ShowMostPagesComponent : ViewComponent
    {
        private readonly IPageRepository _pageRepository;

        public ShowMostPagesComponent(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowMostPagesComponent",await _pageRepository.GetTopLikePages()));
        }
    }
}
