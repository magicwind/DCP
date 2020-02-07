using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCP.Model
{
    public class Table : BasePocoWithIntKey
    {
        [Display(Name = "连接")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int? ConnectionID { get; set; }

        [Display(Name = "连接")]
        //[Required(ErrorMessage = "{0}是必填项")]
        //[InverseProperty("LeftConnsInDataCheck")]
        public Connection Connection { get; set; }

        [Display(Name = "表名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(128)]
        public string TableName { get; set; }

        [Display(Name = "是否是分区表")]
        [Required(ErrorMessage = "{0}是必填项")]
        public bool IsPartitionTable { get; set; }

        [Display(Name = "创建时间列名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(64)]
        public string CreateTimeColumnName { get; set; }

        [Display(Name = "更新时间列名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(64)]
        public string UpdateTimeColumnName { get; set; }
    }
}
