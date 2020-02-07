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
        public List<ComboSelectListItem> AllLeftTables { get; set; }
        [Display(Name = "左表")]
        public int? LeftTableID { get; set; }
        public List<ComboSelectListItem> AllRightTables { get; set; }
        [Display(Name = "右表")]
        public int? RightTableID { get; set; }
        [Display(Name = "行变化类型")]
        public RowChangeMode? RowChange { get; set; }

        protected override void InitVM()
        {
            AllLeftTables = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
            AllRightTables = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
        }

    }
}
