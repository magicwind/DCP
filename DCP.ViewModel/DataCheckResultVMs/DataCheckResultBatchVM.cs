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
    public partial class DataCheckResultBatchVM : BaseBatchVM<DataCheckResult, DataCheckResult_BatchEdit>
    {
        public DataCheckResultBatchVM()
        {
            ListVM = new DataCheckResultListVM();
            LinkedVM = new DataCheckResult_BatchEdit();
        }

    }

	/// <summary>
    /// 批量编辑字段类
    /// </summary>
    public class DataCheckResult_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
