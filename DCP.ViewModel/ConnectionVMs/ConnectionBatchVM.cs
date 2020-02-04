using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.ConnectionVMs
{
    public partial class ConnectionBatchVM : BaseBatchVM<Connection, Connection_BatchEdit>
    {
        public ConnectionBatchVM()
        {
            ListVM = new ConnectionListVM();
            LinkedVM = new Connection_BatchEdit();
        }

    }

	/// <summary>
    /// 批量编辑字段类
    /// </summary>
    public class Connection_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
