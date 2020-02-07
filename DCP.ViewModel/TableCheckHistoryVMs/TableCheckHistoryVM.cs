using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.TableCheckHistoryVMs
{
    public partial class TableCheckHistoryVM : BaseCRUDVM<TableCheckHistory>
    {
        public List<ComboSelectListItem> AllTables { get; set; }

        public TableCheckHistoryVM()
        {
            SetInclude(x => x.Table);
        }

        protected override void InitVM()
        {
            AllTables = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
