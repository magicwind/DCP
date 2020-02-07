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
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.Name);
        [Display(Name = "左表")]
        public ExcelPropety LeftTable_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.LeftTableID);
        [Display(Name = "右表")]
        public ExcelPropety RightTable_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.RightTableID);
        [Display(Name = "分组检查类型")]
        public ExcelPropety CheckGroupType_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.CheckGroupType);
        [Display(Name = "行变化类型")]
        public ExcelPropety RowChange_Excel = ExcelPropety.CreateProperty<DataCheck>(x => x.RowChange);

	    protected override void InitVM()
        {
            LeftTable_Excel.DataType = ColumnDataType.ComboBox;
            LeftTable_Excel.ListItems = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
            RightTable_Excel.DataType = ColumnDataType.ComboBox;
            RightTable_Excel.ListItems = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
        }

    }

    public class DataCheckImportVM : BaseImportVM<DataCheckTemplateVM, DataCheck>
    {

    }

}
