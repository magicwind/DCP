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
    public partial class TableCheckHistoryBatchVM : BaseBatchVM<TableCheckHistory, TableCheckHistory_BatchEdit>
    {
        public TableCheckHistoryBatchVM()
        {
            ListVM = new TableCheckHistoryListVM();
            LinkedVM = new TableCheckHistory_BatchEdit();
        }

    }

	/// <summary>
    /// 批量编辑字段类
    /// </summary>
    public class TableCheckHistory_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
