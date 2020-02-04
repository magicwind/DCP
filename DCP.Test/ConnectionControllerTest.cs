using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using DCP.Controllers;
using DCP.ViewModel.ConnectionVMs;
using DCP.Model;
using DCP.DataAccess;

namespace DCP.Test
{
    [TestClass]
    public class ConnectionControllerTest
    {
        private ConnectionController _controller;
        private string _seed;

        public ConnectionControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<ConnectionController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as ConnectionListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(ConnectionVM));

            ConnectionVM vm = rv.Model as ConnectionVM;
            Connection v = new Connection();
			
            v.Name = "KgH";
            v.Host = "gMTL";
            v.Port = 12;
            v.Database = "tBEa";
            v.Username = "2jZ";
            v.Password = "1O0xDQf";
            v.ID = 10;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Connection>().FirstOrDefault();
				
                Assert.AreEqual(data.Name, "KgH");
                Assert.AreEqual(data.Host, "gMTL");
                Assert.AreEqual(data.Port, 12);
                Assert.AreEqual(data.Database, "tBEa");
                Assert.AreEqual(data.Username, "2jZ");
                Assert.AreEqual(data.Password, "1O0xDQf");
                Assert.AreEqual(data.ID, 10);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "KgH";
                v.Host = "gMTL";
                v.Port = 12;
                v.Database = "tBEa";
                v.Username = "2jZ";
                v.Password = "1O0xDQf";
                v.ID = 10;
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ConnectionVM));

            ConnectionVM vm = rv.Model as ConnectionVM;
            v = new Connection();
            v.ID = vm.Entity.ID;
       		
            v.Name = "us3obVjYg";
            v.Host = "Dv6R8YTw";
            v.Port = 61;
            v.Database = "KGi";
            v.Username = "5SGjrD";
            v.Password = "MH10RD";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Host", "");
            vm.FC.Add("Entity.Port", "");
            vm.FC.Add("Entity.Database", "");
            vm.FC.Add("Entity.Username", "");
            vm.FC.Add("Entity.Password", "");
            vm.FC.Add("Entity.ID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Connection>().FirstOrDefault();
 				
                Assert.AreEqual(data.Name, "us3obVjYg");
                Assert.AreEqual(data.Host, "Dv6R8YTw");
                Assert.AreEqual(data.Port, 61);
                Assert.AreEqual(data.Database, "KGi");
                Assert.AreEqual(data.Username, "5SGjrD");
                Assert.AreEqual(data.Password, "MH10RD");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "KgH";
                v.Host = "gMTL";
                v.Port = 12;
                v.Database = "tBEa";
                v.Username = "2jZ";
                v.Password = "1O0xDQf";
                v.ID = 10;
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ConnectionVM));

            ConnectionVM vm = rv.Model as ConnectionVM;
            v = new Connection();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Connection>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "KgH";
                v.Host = "gMTL";
                v.Port = 12;
                v.Database = "tBEa";
                v.Username = "2jZ";
                v.Password = "1O0xDQf";
                v.ID = 10;
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Connection v1 = new Connection();
            Connection v2 = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "KgH";
                v1.Host = "gMTL";
                v1.Port = 12;
                v1.Database = "tBEa";
                v1.Username = "2jZ";
                v1.Password = "1O0xDQf";
                v1.ID = 10;
                v2.Name = "us3obVjYg";
                v2.Host = "Dv6R8YTw";
                v2.Port = 61;
                v2.Database = "KGi";
                v2.Username = "5SGjrD";
                v2.Password = "MH10RD";
                context.Set<Connection>().Add(v1);
                context.Set<Connection>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(ConnectionBatchVM));

            ConnectionBatchVM vm = rv.Model as ConnectionBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Connection>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as ConnectionListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
