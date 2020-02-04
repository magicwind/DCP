using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using DCP.Controllers;
using DCP.ViewModel.DataCheckVMs;
using DCP.Model;
using DCP.DataAccess;

namespace DCP.Test
{
    [TestClass]
    public class DataCheckControllerTest
    {
        private DataCheckController _controller;
        private string _seed;

        public DataCheckControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DataCheckController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as DataCheckListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckVM));

            DataCheckVM vm = rv.Model as DataCheckVM;
            DataCheck v = new DataCheck();
			
            v.LeftConnectionID = AddLeftConnection();
            v.LeftTable = "mtvrE4uJ";
            v.RightConnectionID = AddRightConnection();
            v.RightTable = "qTIAgyH";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheck>().FirstOrDefault();
				
                Assert.AreEqual(data.LeftTable, "mtvrE4uJ");
                Assert.AreEqual(data.RightTable, "qTIAgyH");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DataCheck v = new DataCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LeftConnectionID = AddLeftConnection();
                v.LeftTable = "mtvrE4uJ";
                v.RightConnectionID = AddRightConnection();
                v.RightTable = "qTIAgyH";
                context.Set<DataCheck>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckVM));

            DataCheckVM vm = rv.Model as DataCheckVM;
            v = new DataCheck();
            v.ID = vm.Entity.ID;
       		
            v.LeftTable = "2nft";
            v.RightTable = "UPI7iIpR";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LeftConnectionID", "");
            vm.FC.Add("Entity.LeftTable", "");
            vm.FC.Add("Entity.RightConnectionID", "");
            vm.FC.Add("Entity.RightTable", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheck>().FirstOrDefault();
 				
                Assert.AreEqual(data.LeftTable, "2nft");
                Assert.AreEqual(data.RightTable, "UPI7iIpR");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DataCheck v = new DataCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LeftConnectionID = AddLeftConnection();
                v.LeftTable = "mtvrE4uJ";
                v.RightConnectionID = AddRightConnection();
                v.RightTable = "qTIAgyH";
                context.Set<DataCheck>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckVM));

            DataCheckVM vm = rv.Model as DataCheckVM;
            v = new DataCheck();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<DataCheck>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DataCheck v = new DataCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.LeftConnectionID = AddLeftConnection();
                v.LeftTable = "mtvrE4uJ";
                v.RightConnectionID = AddRightConnection();
                v.RightTable = "qTIAgyH";
                context.Set<DataCheck>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            DataCheck v1 = new DataCheck();
            DataCheck v2 = new DataCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LeftConnectionID = AddLeftConnection();
                v1.LeftTable = "mtvrE4uJ";
                v1.RightConnectionID = AddRightConnection();
                v1.RightTable = "qTIAgyH";
                v2.LeftConnectionID = v1.LeftConnectionID; 
                v2.LeftTable = "2nft";
                v2.RightConnectionID = v1.RightConnectionID; 
                v2.RightTable = "UPI7iIpR";
                context.Set<DataCheck>().Add(v1);
                context.Set<DataCheck>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckBatchVM));

            DataCheckBatchVM vm = rv.Model as DataCheckBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<DataCheck>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DataCheckListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddLeftConnection()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "6bhzzxKx";
                v.Host = "zrlnU";
                v.Port = 11;
                v.Username = "wKlj9";
                v.Password = "sN2CFJ";
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddRightConnection()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "aIeXnwfr";
                v.Host = "tObTvIkFP";
                v.Port = 33;
                v.Username = "7xkH3";
                v.Password = "DDg7di6";
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
