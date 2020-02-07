using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCP.Model
{
    public class TableCheckHistory : BasePocoWithIntKey
    {
        [Display(Name = "表")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int? TableID { get; set; }

        [Display(Name = "表")]
        public Table Table { get; set; }

        [Display(Name = "分组类型")]
        [Required(ErrorMessage = "{0}是必填项")]
        public TableCheckGroupType GroupType { get; set; }

        [Display(Name = "分组值")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(32)]
        public string GroupValue { get; set; }

        [Display(Name = "分组数量")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int GroupCount { get; set; }

        [Display(Name = "检查时间")]
        [Required(ErrorMessage = "{0}是必填项")]
        public DateTime CheckTime { get; set; }
    }
}
