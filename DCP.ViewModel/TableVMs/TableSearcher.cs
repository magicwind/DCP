using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.TableVMs
{
    public partial class TableSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllConnections { get; set; }
        [Display(Name = "连接")]
        public int? ConnectionID { get; set; }
        [Display(Name = "表名")]
        public String TableName { get; set; }
        [Display(Name = "创建时间列名")]
        public String CreateTimeColumnName { get; set; }
        [Display(Name = "更新时间列名")]
        public String UpdateTimeColumnName { get; set; }

        protected override void InitVM()
        {
            AllConnections = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }
}
