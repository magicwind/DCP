using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckVMs
{
    public partial class DataCheckVM : BaseCRUDVM<DataCheck>
    {
        public List<ComboSelectListItem> AllLeftTables { get; set; }
        public List<ComboSelectListItem> AllRightTables { get; set; }

        public DataCheckVM()
        {
            SetInclude(x => x.LeftTable);
            SetInclude(x => x.RightTable);
        }

        protected override void InitVM()
        {
            AllLeftTables = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
            AllRightTables = DC.Set<Table>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
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
