using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckResultVMs
{
    public partial class DataCheckResultVM : BaseCRUDVM<DataCheckResult>
    {
        public List<ComboSelectListItem> AllDataChecks { get; set; }
        public List<ComboSelectListItem> AllDataCheckRuns { get; set; }

        public DataCheckResultVM()
        {
            SetInclude(x => x.DataCheck);
            SetInclude(x => x.DataCheckRun);
        }

        protected override void InitVM()
        {
            AllDataChecks = DC.Set<DataCheck>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllDataCheckRuns = DC.Set<DataCheckRun>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.RunName);
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
