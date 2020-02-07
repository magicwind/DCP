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
    public partial class DataCheckRunBatchVM : BaseBatchVM<DataCheckRun, DataCheckRun_BatchEdit>
    {
        public DataCheckRunBatchVM()
        {
            ListVM = new DataCheckRunListVM();
            LinkedVM = new DataCheckRun_BatchEdit();
        }

    }

	/// <summary>
    /// 批量编辑字段类
    /// </summary>
    public class DataCheckRun_BatchEdit : BaseVM
    {
        [Display(Name = "状态")]
        public RunStatus? Status { get; set; }

        protected override void InitVM()
        {
        }

    }

}
