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
    public partial class DataCheckSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllLeftConnections { get; set; }
        [Display(Name = "左连接")]
        public int? LeftConnectionID { get; set; }
        [Display(Name = "左表名称")]
        public String LeftTable { get; set; }
        public List<ComboSelectListItem> AllRightConnections { get; set; }
        [Display(Name = "右连接")]
        public int? RightConnectionID { get; set; }
        [Display(Name = "右表名称")]
        public String RightTable { get; set; }
        [Display(Name = "行变化类型")]
        public RowChangeMode? RowChange { get; set; }
        protected override void InitVM()
        {
            AllLeftConnections = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllRightConnections = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }
}
