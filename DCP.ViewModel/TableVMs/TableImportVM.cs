using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.TableVMs
{
    public partial class TableTemplateVM : BaseTemplateVM
    {
        [Display(Name = "连接")]
        public ExcelPropety Connection_Excel = ExcelPropety.CreateProperty<Table>(x => x.ConnectionID);
        [Display(Name = "表名")]
        public ExcelPropety TableName_Excel = ExcelPropety.CreateProperty<Table>(x => x.TableName);
        [Display(Name = "是否是分区表")]
        public ExcelPropety IsPartitionTable_Excel = ExcelPropety.CreateProperty<Table>(x => x.IsPartitionTable);
        [Display(Name = "创建时间列名")]
        public ExcelPropety CreateTimeColumnName_Excel = ExcelPropety.CreateProperty<Table>(x => x.CreateTimeColumnName);
        [Display(Name = "更新时间列名")]
        public ExcelPropety UpdateTimeColumnName_Excel = ExcelPropety.CreateProperty<Table>(x => x.UpdateTimeColumnName);

	    protected override void InitVM()
        {
            Connection_Excel.DataType = ColumnDataType.ComboBox;
            Connection_Excel.ListItems = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

    public class TableImportVM : BaseImportVM<TableTemplateVM, Table>
    {

    }

}
