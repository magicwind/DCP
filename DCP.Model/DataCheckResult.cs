using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCP.Model
{
    public class DataCheckResult : BasePocoWithIntKey
    {
        [Display(Name = "检查名称")]
        [Required]
        public int? DataCheckID { get; set; }

        [Display(Name = "检查名称")]
        public DataCheck DataCheck { get; set; }

        [Display(Name = "运行名称")]
        [Required]
        public int? DataCheckRunID { get; set; }

        [Display(Name = "运行名称")]
        public DataCheckRun DataCheckRun { get; set; }

        [Display(Name = "创建日期")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(32)]
        public string CreatedDate { get; set; }

        [Display(Name = "左表数量")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int LeftCount { get; set; }

        [Display(Name = "右表数量")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int RightCount { get; set; }
    }
}
