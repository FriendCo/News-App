using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Repositorys;

namespace Web.ViewComponents
{
    public class ShowGroupComponent : ViewComponent
    {
        private readonly IGroupRepository _groupRepository;

        public ShowGroupComponent(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowGroupComponent", _groupRepository.GetGroupVM()));
        }

    }
}
