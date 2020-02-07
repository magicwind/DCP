using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCP.Model
{
    public enum RunStatus
    {
        Success,
        Failed,
        Runing
    }

    public class DataCheckRun : BasePocoWithIntKey
    {
        [Display(Name = "运行名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(64)]
        public string RunName { get; set; }

        [Display(Name = "开始时间")]
        [Required(ErrorMessage = "{0}是必填项")]
        public DateTime StartedAt { get; set; }

        [Display(Name = "结束时间")]
        //[Required(ErrorMessage = "{0}是必填项")]
        public DateTime? EndedAt { get; set; }

        [Display(Name = "成功数量")]
        public int? PassedCaseCount { get; set; }

        [Display(Name = "失败数量")]
        public int? FailedCaseCount { get; set; }

        [Display(Name = "状态")]
        [Required(ErrorMessage = "{0}是必填项")]
        public RunStatus Status { get; set; }
    }
}
