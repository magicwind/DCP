using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckVMs
{
    public partial class DataCheckTemplateVM : BaseTemplateVM
    {
        [Display(Name = "左连接")]
        public ExcelPropety LeftConnection_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.LeftConnectionID);
        [Display(Name = "左表名称")]
        public ExcelPropety LeftTable_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.LeftTable);
        [Display(Name = "右连接")]
        public ExcelPropety RightConnection_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.RightConnectionID);
        [Display(Name = "右表名称")]
        public ExcelPropety RightTable_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.RightTable);
        [Display(Name = "行变化类型")]
        public ExcelPropety RowChange_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.RowChange);

	    protected override void InitVM()
        {
            LeftConnection_Excel.DataType = ColumnDataType.ComboBox;
            LeftConnection_Excel.ListItems = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            RightConnection_Excel.DataType = ColumnDataType.ComboBox;
            RightConnection_Excel.ListItems = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

    public class DataCheckImportVM : BaseImportVM<DataCheckTemplateVM, DataCheck>
    {

    }

}
