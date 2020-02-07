using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DCP.Model;


namespace DCP.ViewModel.DataCheckRunVMs
{
    public partial class DataCheckRunListVM : BasePagedListVM<DataCheckRun_View, DataCheckRunSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckRun", GridActionStandardTypesEnum.ExportExcel, "导出",""),
            };
        }

        protected override IEnumerable<IGridColumn<DataCheckRun_View>> InitGridHeader()
        {
            return new List<GridColumn<DataCheckRun_View>>{
                this.MakeGridHeader(x => x.RunName),
                this.MakeGridHeader(x => x.StartedAt),
                this.MakeGridHeader(x => x.EndedAt),
                this.MakeGridHeader(x => x.PassedCaseCount),
                this.MakeGridHeader(x => x.FailedCaseCount),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DataCheckRun_View> GetSearchQuery()
        {
            var query = DC.Set<DataCheckRun>()
                .CheckContain(Searcher.RunName, x=>x.RunName)
                .CheckEqual(Searcher.StartedAt, x=>x.StartedAt)
                .CheckEqual(Searcher.EndedAt, x=>x.EndedAt)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .Select(x => new DataCheckRun_View
                {
				    ID = x.ID,
                    RunName = x.RunName,
                    StartedAt = x.StartedAt,
                    EndedAt = x.EndedAt,
                    PassedCaseCount = x.PassedCaseCount,
                    FailedCaseCount = x.FailedCaseCount,
                    Status = x.Status,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DataCheckRun_View : DataCheckRun{

    }
}
