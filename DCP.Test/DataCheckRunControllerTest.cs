using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using DCP.Controllers;
using DCP.ViewModel.DataCheckRunVMs;
using DCP.Model;
using DCP.DataAccess;

namespace DCP.Test
{
    [TestClass]
    public class DataCheckRunControllerTest
    {
        private DataCheckRunController _controller;
        private string _seed;

        public DataCheckRunControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DataCheckRunController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as DataCheckRunListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckRunVM));

            DataCheckRunVM vm = rv.Model as DataCheckRunVM;
            DataCheckRun v = new DataCheckRun();
			
            v.RunName = "eq4jAhl2";
            v.ID = 55;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheckRun>().FirstOrDefault();
				
                Assert.AreEqual(data.RunName, "eq4jAhl2");
                Assert.AreEqual(data.ID, 55);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DataCheckRun v = new DataCheckRun();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.RunName = "eq4jAhl2";
                v.ID = 55;
                context.Set<DataCheckRun>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckRunVM));

            DataCheckRunVM vm = rv.Model as DataCheckRunVM;
            v = new DataCheckRun();
            v.ID = vm.Entity.ID;
       		
            v.RunName = "3bWUB5";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.RunName", "");
            vm.FC.Add("Entity.ID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheckRun>().FirstOrDefault();
 				
                Assert.AreEqual(data.RunName, "3bWUB5");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DataCheckRun v = new DataCheckRun();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.RunName = "eq4jAhl2";
                v.ID = 55;
                context.Set<DataCheckRun>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckRunVM));

            DataCheckRunVM vm = rv.Model as DataCheckRunVM;
            v = new DataCheckRun();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<DataCheckRun>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DataCheckRun v = new DataCheckRun();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.RunName = "eq4jAhl2";
                v.ID = 55;
                context.Set<DataCheckRun>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            DataCheckRun v1 = new DataCheckRun();
            DataCheckRun v2 = new DataCheckRun();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.RunName = "eq4jAhl2";
                v1.ID = 55;
                v2.RunName = "3bWUB5";
                context.Set<DataCheckRun>().Add(v1);
                context.Set<DataCheckRun>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckRunBatchVM));

            DataCheckRunBatchVM vm = rv.Model as DataCheckRunBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<DataCheckRun>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DataCheckRunListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
