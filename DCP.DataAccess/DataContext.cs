using DCP.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace DCP.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<Connection> Connections { get; set; }
        public DbSet<DataCheck> DataChecks { get; set; }

        public DataContext(string cs, DBTypeEnum dbtype)
             : base(cs, dbtype)
        {
        }

    }

    /// <summary>
    /// 为EF的Migration准备的辅助类，填写完整连接字符串和数据库类型
    /// 就可以使用Add-Migration和Update-Database了
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("Server=localhost; Database=WTMDb; User=root; Password=root;", DBTypeEnum.MySql);
        }
    }

}
