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
        [Display(Name = "左连接")]
        [Required(ErrorMessage = "{0}是必填项")]
        [ForeignKey("LeftConnection")]
        public int? LeftConnectionID { get; set; }

        [Display(Name = "左连接")]
        //[Required(ErrorMessage = "{0}是必填项")]
        //[InverseProperty("LeftConnsInDataCheck")]
        public Connection LeftConnection { get; set; }

        [Display(Name = "左表名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(128)]
        public string LeftTable { get; set; }

        [Display(Name = "右连接")]
        [Required(ErrorMessage = "{0}是必填项")]
        [ForeignKey("RightConnection")]
        public int? RightConnectionID { get; set; }

        [Display(Name = "右连接")]
        //[Required(ErrorMessage = "{0}是必填项")]
        //[InverseProperty("RightConnsInDataCheck")]
        public Connection RightConnection { get; set; }

        [Display(Name = "右表名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(128)]
        public string RightTable { get; set; }

        [Display(Name = "行变化类型")]
        [Required(ErrorMessage = "{0}是必填项")]
        public RowChangeMode? RowChange { get; set; }
    }
}
