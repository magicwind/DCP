using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DCP.Model;


namespace DCP.ViewModel.TableCheckHistoryVMs
{
    public partial class TableCheckHistoryListVM : BasePagedListVM<TableCheckHistory_View, TableCheckHistorySearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardAction("TableCheckHistory", GridActionStandardTypesEnum.ExportExcel, "导出",""),
            };
        }

        protected override IEnumerable<IGridColumn<TableCheckHistory_View>> InitGridHeader()
        {
            return new List<GridColumn<TableCheckHistory_View>>{
                this.MakeGridHeader(x => x.TableName_view),
                this.MakeGridHeader(x => x.GroupType),
                this.MakeGridHeader(x => x.GroupValue),
                this.MakeGridHeader(x => x.GroupCount),
                this.MakeGridHeader(x => x.CheckTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<TableCheckHistory_View> GetSearchQuery()
        {
            var query = DC.Set<TableCheckHistory>()
                .CheckEqual(Searcher.TableID, x=>x.TableID)
                .CheckContain(Searcher.GroupValue, x=>x.GroupValue)
                .CheckEqual(Searcher.CheckTime, x=>x.CheckTime)
                .Select(x => new TableCheckHistory_View
                {
				    ID = x.ID,
                    TableName_view = x.Table.TableName,
                    GroupType = x.GroupType,
                    GroupValue = x.GroupValue,
                    GroupCount = x.GroupCount,
                    CheckTime = x.CheckTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class TableCheckHistory_View : TableCheckHistory{
        [Display(Name = "表名")]
        public String TableName_view { get; set; }

    }
}
