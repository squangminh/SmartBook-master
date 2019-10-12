using AdwardSoft.Provider.API;
using AdwardSoft.Web.Inside.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.ViewComponents.User
{
    public class UserListViewComponent : ViewComponent
    {
        private IAPIFactory _apiFactory;
        public UserListViewComponent(IAPIFactory apiFactory)
        {
            _apiFactory = apiFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(
        int maxPriority, bool isDone)
        {
            var items = GetItemsAsync(maxPriority, isDone);
            return View(items);
        }
        private List<UserInfoViewModel> GetItemsAsync(int maxPriority, bool isDone)
        {
            return new List<UserInfoViewModel>();
        }
    }
}
