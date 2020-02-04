﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.ConnectionVMs
{
    public partial class ConnectionSearcher : BaseSearcher
    {
        [Display(Name = "名称")]
        public String Name { get; set; }
        [Display(Name = "数据库类型")]
        public DatabaseType? Type { get; set; }
        [Display(Name = "主机")]
        public String Host { get; set; }
        [Display(Name = "库名")]
        public String Database { get; set; }

        protected override void InitVM()
        {
        }

    }
}
