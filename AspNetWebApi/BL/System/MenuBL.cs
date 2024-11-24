using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.ViewModels.Common;
using AspNetWebApi.DA.ViewModels.HR;
using AspNetWebApi.DA.ViewModels.System;

namespace AspNetWebApi.BL.System
{
    public class MenuBL
    {
        ModelVssDb _vssDb = new ModelVssDb();
        public IEnumerable<MenuVM> Get(string phone, int pageIndex, int pageSize)
        {
            int nRow = _vssDb.Menus.Count();
            var listMenu = _vssDb.Menus
                .Select(x => new MenuVM
                {
                    MenuId = x.MenuId,
                    MenuName = x.MenuName,
                    MenuPath = x.MenuPath,

                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    RowCount = nRow
                })
                .OrderBy(s => s.MenuId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return listMenu;
        }
    }
}