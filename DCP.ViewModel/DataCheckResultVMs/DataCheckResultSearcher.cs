using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckResultVMs
{
    public partial class DataCheckResultSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllDataChecks { get; set; }
        [Display(Name = "检查名称")]
        public int? DataCheckID { get; set; }
        public List<ComboSelectListItem> AllDataCheckRuns { get; set; }
        [Display(Name = "运行名称")]
        public int? DataCheckRunID { get; set; }
        [Display(Name = "创建日期")]
        public String CreatedDate { get; set; }

        protected override void InitVM()
        {
            AllDataChecks = DC.Set<DataCheck>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllDataCheckRuns = DC.Set<DataCheckRun>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.RunName);
        }

    }
}
