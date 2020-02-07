using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckRunVMs
{
    public partial class DataCheckRunTemplateVM : BaseTemplateVM
    {
        [Display(Name = "运行名称")]
        public ExcelPropety RunName_Excel = ExcelPropety.CreateProperty<DataCheckRun>(x => x.RunName);
        [Display(Name = "开始时间")]
        public ExcelPropety StartedAt_Excel = ExcelPropety.CreateProperty<DataCheckRun>(x => x.StartedAt);
        [Display(Name = "结束时间")]
        public ExcelPropety EndedAt_Excel = ExcelPropety.CreateProperty<DataCheckRun>(x => x.EndedAt);
        [Display(Name = "成功数量")]
        public ExcelPropety PassedCaseCount_Excel = ExcelPropety.CreateProperty<DataCheckRun>(x => x.PassedCaseCount);
        [Display(Name = "失败数量")]
        public ExcelPropety FailedCaseCount_Excel = ExcelPropety.CreateProperty<DataCheckRun>(x => x.FailedCaseCount);
        [Display(Name = "状态")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<DataCheckRun>(x => x.Status);

	    protected override void InitVM()
        {
        }

    }

    public class DataCheckRunImportVM : BaseImportVM<DataCheckRunTemplateVM, DataCheckRun>
    {

    }

}
