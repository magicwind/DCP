using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using DCP.Controllers;
using DCP.ViewModel.TableVMs;
using DCP.Model;
using DCP.DataAccess;

namespace DCP.Test
{
    [TestClass]
    public class TableControllerTest
    {
        private TableController _controller;
        private string _seed;

        public TableControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<TableController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as TableListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(TableVM));

            TableVM vm = rv.Model as TableVM;
            Table v = new Table();
			
            v.ConnectionID = AddConnection();
            v.TableName = "1qMRlx4";
            v.CreateTimeColumnName = "37q4dn";
            v.UpdateTimeColumnName = "RuvmER";
            v.ID = 84;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Table>().FirstOrDefault();
				
                Assert.AreEqual(data.TableName, "1qMRlx4");
                Assert.AreEqual(data.CreateTimeColumnName, "37q4dn");
                Assert.AreEqual(data.UpdateTimeColumnName, "RuvmER");
                Assert.AreEqual(data.ID, 84);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Table v = new Table();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ConnectionID = AddConnection();
                v.TableName = "1qMRlx4";
                v.CreateTimeColumnName = "37q4dn";
                v.UpdateTimeColumnName = "RuvmER";
                v.ID = 84;
                context.Set<Table>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TableVM));

            TableVM vm = rv.Model as TableVM;
            v = new Table();
            v.ID = vm.Entity.ID;
       		
            v.TableName = "y2i24";
            v.CreateTimeColumnName = "eMO";
            v.UpdateTimeColumnName = "TUSfWk";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ConnectionID", "");
            vm.FC.Add("Entity.TableName", "");
            vm.FC.Add("Entity.CreateTimeColumnName", "");
            vm.FC.Add("Entity.UpdateTimeColumnName", "");
            vm.FC.Add("Entity.ID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Table>().FirstOrDefault();
 				
                Assert.AreEqual(data.TableName, "y2i24");
                Assert.AreEqual(data.CreateTimeColumnName, "eMO");
                Assert.AreEqual(data.UpdateTimeColumnName, "TUSfWk");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Table v = new Table();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ConnectionID = AddConnection();
                v.TableName = "1qMRlx4";
                v.CreateTimeColumnName = "37q4dn";
                v.UpdateTimeColumnName = "RuvmER";
                v.ID = 84;
                context.Set<Table>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TableVM));

            TableVM vm = rv.Model as TableVM;
            v = new Table();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Table>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Table v = new Table();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ConnectionID = AddConnection();
                v.TableName = "1qMRlx4";
                v.CreateTimeColumnName = "37q4dn";
                v.UpdateTimeColumnName = "RuvmER";
                v.ID = 84;
                context.Set<Table>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Table v1 = new Table();
            Table v2 = new Table();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ConnectionID = AddConnection();
                v1.TableName = "1qMRlx4";
                v1.CreateTimeColumnName = "37q4dn";
                v1.UpdateTimeColumnName = "RuvmER";
                v1.ID = 84;
                v2.ConnectionID = v1.ConnectionID; 
                v2.TableName = "y2i24";
                v2.CreateTimeColumnName = "eMO";
                v2.UpdateTimeColumnName = "TUSfWk";
                context.Set<Table>().Add(v1);
                context.Set<Table>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TableBatchVM));

            TableBatchVM vm = rv.Model as TableBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Table>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as TableListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Int32 AddConnection()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "QwH";
                v.Host = "ZUKW5Zt";
                v.Port = 80;
                v.Database = "y9Mg";
                v.Username = "xQZtlr";
                v.Password = "aubMKgy";
                v.ID = 39;
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
