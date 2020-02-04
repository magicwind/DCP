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
        public List<ComboSelectListItem> AllLeftConnections { get; set; }
        public List<ComboSelectListItem> AllRightConnections { get; set; }

        public DataCheckVM()
        {
            SetInclude(x => x.LeftConnection);
            SetInclude(x => x.RightConnection);
        }

        protected override void InitVM()
        {
            AllLeftConnections = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllRightConnections = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
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
