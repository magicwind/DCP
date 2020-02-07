using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckResultVMs
{
    public partial class DataCheckResultTemplateVM : BaseTemplateVM
    {
        [Display(Name = "检查名称")]
        public ExcelPropety DataCheck_Excel = ExcelPropety.CreateProperty<DataCheckResult>(x => x.DataCheckID);
        [Display(Name = "运行名称")]
        public ExcelPropety DataCheckRun_Excel = ExcelPropety.CreateProperty<DataCheckResult>(x => x.DataCheckRunID);
        [Display(Name = "创建日期")]
        public ExcelPropety CreatedDate_Excel = ExcelPropety.CreateProperty<DataCheckResult>(x => x.CreatedDate);
        [Display(Name = "左表数量")]
        public ExcelPropety LeftCount_Excel = ExcelPropety.CreateProperty<DataCheckResult>(x => x.LeftCount);
        [Display(Name = "右表数量")]
        public ExcelPropety RightCount_Excel = ExcelPropety.CreateProperty<DataCheckResult>(x => x.RightCount);

	    protected override void InitVM()
        {
            DataCheck_Excel.DataType = ColumnDataType.ComboBox;
            DataCheck_Excel.ListItems = DC.Set<DataCheck>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            DataCheckRun_Excel.DataType = ColumnDataType.ComboBox;
            DataCheckRun_Excel.ListItems = DC.Set<DataCheckRun>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.RunName);
        }

    }

    public class DataCheckResultImportVM : BaseImportVM<DataCheckResultTemplateVM, DataCheckResult>
    {

    }

}
