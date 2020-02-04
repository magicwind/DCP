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
			
            v.Name = "cwy";
            v.Host = "TnzYKkEYk";
            v.Port = 13;
            v.Username = "LKUv8L01";
            v.Password = "ihK";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Connection>().FirstOrDefault();
				
                Assert.AreEqual(data.Name, "cwy");
                Assert.AreEqual(data.Host, "TnzYKkEYk");
                Assert.AreEqual(data.Port, 13);
                Assert.AreEqual(data.Username, "LKUv8L01");
                Assert.AreEqual(data.Password, "ihK");
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
       			
                v.Name = "cwy";
                v.Host = "TnzYKkEYk";
                v.Port = 13;
                v.Username = "LKUv8L01";
                v.Password = "ihK";
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ConnectionVM));

            ConnectionVM vm = rv.Model as ConnectionVM;
            v = new Connection();
            v.ID = vm.Entity.ID;
       		
            v.Name = "TLJt1";
            v.Host = "qJCqDa8";
            v.Port = 11;
            v.Username = "amfm7d";
            v.Password = "pNhM";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Host", "");
            vm.FC.Add("Entity.Port", "");
            vm.FC.Add("Entity.Username", "");
            vm.FC.Add("Entity.Password", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Connection>().FirstOrDefault();
 				
                Assert.AreEqual(data.Name, "TLJt1");
                Assert.AreEqual(data.Host, "qJCqDa8");
                Assert.AreEqual(data.Port, 11);
                Assert.AreEqual(data.Username, "amfm7d");
                Assert.AreEqual(data.Password, "pNhM");
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
        		
                v.Name = "cwy";
                v.Host = "TnzYKkEYk";
                v.Port = 13;
                v.Username = "LKUv8L01";
                v.Password = "ihK";
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
				
                v.Name = "cwy";
                v.Host = "TnzYKkEYk";
                v.Port = 13;
                v.Username = "LKUv8L01";
                v.Password = "ihK";
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
				
                v1.Name = "cwy";
                v1.Host = "TnzYKkEYk";
                v1.Port = 13;
                v1.Username = "LKUv8L01";
                v1.Password = "ihK";
                v2.Name = "TLJt1";
                v2.Host = "qJCqDa8";
                v2.Port = 11;
                v2.Username = "amfm7d";
                v2.Password = "pNhM";
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
