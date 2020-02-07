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
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.TableName_view),
                this.MakeGridHeader(x => x.TableName_view2),
                this.MakeGridHeader(x => x.CheckGroupType),
                this.MakeGridHeader(x => x.RowChange),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DataCheck_View> GetSearchQuery()
        {
            var query = DC.Set<DataCheck>()
                .CheckEqual(Searcher.LeftTableID, x=>x.LeftTableID)
                .CheckEqual(Searcher.RightTableID, x=>x.RightTableID)
                .CheckEqual(Searcher.RowChange, x=>x.RowChange)
                .Select(x => new DataCheck_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    TableName_view = x.LeftTable.TableName,
                    TableName_view2 = x.RightTable.TableName,
                    CheckGroupType = x.CheckGroupType,
                    RowChange = x.RowChange,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DataCheck_View : DataCheck{
        [Display(Name = "表名")]
        public String TableName_view { get; set; }
        [Display(Name = "表名")]
        public String TableName_view2 { get; set; }

    }
}
