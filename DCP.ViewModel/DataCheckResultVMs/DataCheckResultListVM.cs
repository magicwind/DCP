using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DCP.Model;


namespace DCP.ViewModel.DataCheckResultVMs
{
    public partial class DataCheckResultListVM : BasePagedListVM<DataCheckResult_View, DataCheckResultSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardAction("DataCheckResult", GridActionStandardTypesEnum.ExportExcel, "导出",""),
            };
        }

        protected override IEnumerable<IGridColumn<DataCheckResult_View>> InitGridHeader()
        {
            return new List<GridColumn<DataCheckResult_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.RunName_view),
                this.MakeGridHeader(x => x.CreatedDate),
                this.MakeGridHeader(x => x.LeftCount),
                this.MakeGridHeader(x => x.RightCount),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DataCheckResult_View> GetSearchQuery()
        {
            var query = DC.Set<DataCheckResult>()
                .CheckEqual(Searcher.DataCheckID, x=>x.DataCheckID)
                .CheckEqual(Searcher.DataCheckRunID, x=>x.DataCheckRunID)
                .CheckContain(Searcher.CreatedDate, x=>x.CreatedDate)
                .Select(x => new DataCheckResult_View
                {
				    ID = x.ID,
                    Name_view = x.DataCheck.Name,
                    RunName_view = x.DataCheckRun.RunName,
                    CreatedDate = x.CreatedDate,
                    LeftCount = x.LeftCount,
                    RightCount = x.RightCount,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DataCheckResult_View : DataCheckResult{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "运行名称")]
        public String RunName_view { get; set; }

    }
}
