using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.TableCheckHistoryVMs
{
    public partial class TableCheckHistoryTemplateVM : BaseTemplateVM
    {
        [Display(Name = "表")]
        public ExcelPropety Table_Excel = ExcelPropety.CreateProperty<TableCheckHistory>(x => x.TableID);
        [Display(Name = "分组类型")]
        public ExcelPropety GroupType_Excel = ExcelPropety.CreateProperty<TableCheckHistory>(x => x.GroupType);
        [Display(Name = "分组值")]
        public ExcelPropety GroupValue_Excel = ExcelPropety.CreateProperty<TableCheckHistory>(x => x.GroupValue);
        [Display(Name = "分组数量")]
        public ExcelPropety GroupCount_Excel = ExcelPropety.CreateProperty<TableCheckHistory>(x => x.GroupCount);
        [Display(Name = "检查时间")]
        public ExcelPropety CheckTime_Excel = ExcelPropety.CreateProperty<TableCheckHistory>(x => x.CheckTime);

	    protected override void InitVM()
        {
            Table_Excel.DataType = ColumnDataType.ComboBox;
            Table_Excel.ListItems = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
        }

    }

    public class TableCheckHistoryImportVM : BaseImportVM<TableCheckHistoryTemplateVM, TableCheckHistory>
    {

    }

}
