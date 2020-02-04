using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.ConnectionVMs
{
    public partial class ConnectionTemplateVM : BaseTemplateVM
    {
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<Connection>(x => x.Name);
        [Display(Name = "数据库类型")]
        public ExcelPropety Type_Excel = ExcelPropety.CreateProperty<Connection>(x => x.Type);
        [Display(Name = "主机")]
        public ExcelPropety Host_Excel = ExcelPropety.CreateProperty<Connection>(x => x.Host);
        [Display(Name = "端口")]
        public ExcelPropety Port_Excel = ExcelPropety.CreateProperty<Connection>(x => x.Port);
        [Display(Name = "库名")]
        public ExcelPropety Database_Excel = ExcelPropety.CreateProperty<Connection>(x => x.Database);
        [Display(Name = "用户名")]
        public ExcelPropety Username_Excel = ExcelPropety.CreateProperty<Connection>(x => x.Username);
        [Display(Name = "密码")]
        public ExcelPropety Password_Excel = ExcelPropety.CreateProperty<Connection>(x => x.Password);

	    protected override void InitVM()
        {
        }

    }

    public class ConnectionImportVM : BaseImportVM<ConnectionTemplateVM, Connection>
    {

    }

}
