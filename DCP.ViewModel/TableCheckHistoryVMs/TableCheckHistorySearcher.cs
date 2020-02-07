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
    public partial class TableCheckHistorySearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllTables { get; set; }
        [Display(Name = "表")]
        public int? TableID { get; set; }
        [Display(Name = "分组值")]
        public String GroupValue { get; set; }
        [Display(Name = "检查时间")]
        public DateTime? CheckTime { get; set; }

        protected override void InitVM()
        {
            AllTables = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
        }

    }
}
