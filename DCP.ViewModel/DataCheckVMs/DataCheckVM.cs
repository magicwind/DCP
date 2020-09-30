using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using DCP.Model;


namespace DCP.ViewModel.DataCheckVMs
{
    public partial class DataCheckVM : BaseCRUDVM<DataCheck>
    {
        public List<ComboSelectListItem> AllLeftTables { get; set; }
        public List<ComboSelectListItem> AllRightTables { get; set; }
        public int? LeftConnectionId { get; set; }
        public int? RightConnectionId { get; set; }
        public string LeftConnectionName { get; set; }
        public string RightConnectionName { get; set; }
        public List<ComboSelectListItem> AllDatabases { get; set; }
        public List<ComboSelectListItem> RedshiftOnlyDatabases { get; set; }


        public DataCheckVM()
        {
            SetInclude(x => x.LeftTable);
            SetInclude(x => x.RightTable);
        }

        protected override void InitVM()
        {
            AllDatabases = DC.Set<Connection>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            RedshiftOnlyDatabases = DC.Set<Connection>().Where(d => d.Type == DatabaseType.Redshift).GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);

            if (Entity.LeftTableID != null)
            {
                LeftConnectionId = DC.Set<Table>().Where(x => x.ID == Entity.LeftTableID).Select(c => c.ConnectionID).SingleOrDefault();
                AllLeftTables = DC.Set<Table>().Where(x => x.ConnectionID == LeftConnectionId).GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
                LeftConnectionName = DC.Set<Connection>().Where(c => c.ID == LeftConnectionId).Select(c => c.Name).SingleOrDefault();
            }

            if (Entity.RightTableID != null)
            {
                RightConnectionId = DC.Set<Table>().Where(x => x.ID == Entity.RightTableID).Select(c => c.ConnectionID).SingleOrDefault();
                AllRightTables = DC.Set<Table>().Where(x => x.ConnectionID == RightConnectionId).GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.TableName);
                RightConnectionName = DC.Set<Connection>().Where(c => c.ID == RightConnectionId).Select(c => c.Name).SingleOrDefault();
            }
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
