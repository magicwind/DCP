using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DCP.Model;


namespace DCP.ViewModel.ConnectionVMs
{
    public partial class ConnectionListVM : BasePagedListVM<Connection_View, ConnectionSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardAction("Connection", GridActionStandardTypesEnum.ExportExcel, "导出",""),
            };
        }

        protected override IEnumerable<IGridColumn<Connection_View>> InitGridHeader()
        {
            return new List<GridColumn<Connection_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Type),
                this.MakeGridHeader(x => x.Host),
                this.MakeGridHeader(x => x.Port),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Connection_View> GetSearchQuery()
        {
            var query = DC.Set<Connection>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.Host, x=>x.Host)
                .Select(x => new Connection_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Type = x.Type,
                    Host = x.Host,
                    Port = x.Port,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Connection_View : Connection{

    }
}
