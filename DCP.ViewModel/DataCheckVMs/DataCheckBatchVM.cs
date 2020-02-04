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
    public partial class DataCheckBatchVM : BaseBatchVM<DataCheck, DataCheck_BatchEdit>
    {
        public DataCheckBatchVM()
        {
            ListVM = new DataCheckListVM();
            LinkedVM = new DataCheck_BatchEdit();
        }

    }

	/// <summary>
    /// 批量编辑字段类
    /// </summary>
    public class DataCheck_BatchEdit : BaseVM
    {
        public List<ComboSelectListItem> AllLeftConnections { get; set; }
        [Display(Name = "左连接")]
        public Guid? LeftConnectionID { get; set; }
        public List<ComboSelectListItem> AllRightConnections { get; set; }
        [Display(Name = "右连接")]
        public Guid? RightConnectionID { get; set; }
        [Display(Name = "行变化类型")]
        public RowChangeMode? RowChange { get; set; }

        protected override void InitVM()
        {
            AllLeftConnections = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllRightConnections = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

}
