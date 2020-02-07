using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DCP.Model;


namespace DCP.ViewModel.TableVMs
{
    public partial class TableListVM : BasePagedListVM<Table_View, TableSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardAction("Table", GridActionStandardTypesEnum.ExportExcel, "导出",""),
            };
        }

        protected override IEnumerable<IGridColumn<Table_View>> InitGridHeader()
        {
            return new List<GridColumn<Table_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.TableName),
                this.MakeGridHeader(x => x.IsPartitionTable),
                this.MakeGridHeader(x => x.CreateTimeColumnName),
                this.MakeGridHeader(x => x.UpdateTimeColumnName),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Table_View> GetSearchQuery()
        {
            var query = DC.Set<Table>()
                .CheckEqual(Searcher.ConnectionID, x=>x.ConnectionID)
                .CheckContain(Searcher.TableName, x=>x.TableName)
                .CheckContain(Searcher.CreateTimeColumnName, x=>x.CreateTimeColumnName)
                .CheckContain(Searcher.UpdateTimeColumnName, x=>x.UpdateTimeColumnName)
                .Select(x => new Table_View
                {
				    ID = x.ID,
                    Name_view = x.Connection.Name,
                    TableName = x.TableName,
                    IsPartitionTable = x.IsPartitionTable,
                    CreateTimeColumnName = x.CreateTimeColumnName,
                    UpdateTimeColumnName = x.UpdateTimeColumnName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Table_View : Table{
        [Display(Name = "名称")]
        public String Name_view { get; set; }

    }
}
