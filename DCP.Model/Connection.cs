using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace DCP.Model
{
    public enum DatabaseType
    {
        //[Display(Name = "数仓")]
        Redshift,
        MariaDB 
    }

    public class Connection : BasePoco
    {
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(32)]
        public string Name { get; set; }

        [Display(Name = "数据库类型")]
        [Required(ErrorMessage = "{0}是必填项")]
        public DatabaseType? Type { get; set; }

        [Display(Name = "主机")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(128, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Host { get; set; }

        [Display(Name = "端口")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int Port { get; set; }

        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(64)]
        public string Username { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(64, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //public ICollection<DataCheck> LeftConnsInDataCheck { get; set; }

        //public ICollection<DataCheck> RightConnsInDataCheck { get; set; }
    }
}
