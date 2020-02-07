using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace DCP.Model
{
    public enum RowChangeMode
    {
        NoDelete,
        HasDelete
    }

    public class DataCheck : BasePocoWithIntKey
    {
        [Display(Name = "名称")]
        [StringLength(64)]
        public string Name { get; set; }

        [Display(Name = "左表")]
        [Required(ErrorMessage = "{0}是必填项")]
        [ForeignKey("LeftTable")]
        public int? LeftTableID { get; set; }

        [Display(Name = "左表")]
        public Table LeftTable { get; set; }

        [Display(Name = "右表")]
        [Required(ErrorMessage = "{0}是必填项")]
        [ForeignKey("RightTable")]
        public int? RightTableID { get; set; }

        [Display(Name = "右表")]
        public Table RightTable { get; set; }

        [Display(Name = "分组检查类型")]
        [Required(ErrorMessage = "{0}是必填项")]
        public TableCheckGroupType CheckGroupType { get; set; }

        [Display(Name = "行变化类型")]
        [Required(ErrorMessage = "{0}是必填项")]
        public RowChangeMode? RowChange { get; set; }
    }
}
