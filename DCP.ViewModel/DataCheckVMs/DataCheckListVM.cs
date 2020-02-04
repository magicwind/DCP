using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DCP.Model;


namespace DCP.ViewModel.DataCheckVMs
{
    public partial class DataCheckListVM : BasePagedListVM<DataCheck_View, DataCheckSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardAction("DataCheck", GridActionStandardTypesEnum.ExportExcel, "导出",""),
            };
        }

        protected override IEnumerable<IGridColumn<DataCheck_View>> InitGridHeader()
        {
            return new List<GridColumn<DataCheck_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.LeftTable),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.RightTable),
                this.MakeGridHeader(x => x.RowChange),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DataCheck_View> GetSearchQuery()
        {
            var query = DC.Set<DataCheck>()
                .CheckEqual(Searcher.LeftConnectionID, x=>x.LeftConnectionID)
                .CheckContain(Searcher.LeftTable, x=>x.LeftTable)
                .CheckEqual(Searcher.RightConnectionID, x=>x.RightConnectionID)
                .CheckContain(Searcher.RightTable, x=>x.RightTable)
                .CheckEqual(Searcher.RowChange, x=>x.RowChange)
                .Select(x => new DataCheck_View
                {
                    ID = x.ID,
                    Name_view = x.LeftConnection.Name,
                    LeftTable = x.LeftTable,
                    Name_view2 = x.RightConnection.Name,
                    RightTable = x.RightTable,
                    RowChange = x.RowChange,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DataCheck_View : DataCheck{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "名称")]
        public String Name_view2 { get; set; }

    }
}
