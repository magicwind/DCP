using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckRunVMs
{
    public partial class DataCheckRunSearcher : BaseSearcher
    {
        [Display(Name = "运行名称")]
        public String RunName { get; set; }
        [Display(Name = "开始时间")]
        public DateTime? StartedAt { get; set; }
        [Display(Name = "结束时间")]
        public DateTime? EndedAt { get; set; }
        [Display(Name = "状态")]
        public RunStatus? Status { get; set; }

        protected override void InitVM()
        {
        }

    }
}
