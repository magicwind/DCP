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
			
            v.LeftTableID = AddLeftTable();
            v.RightTableID = AddRightTable();
            v.ID = 21;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheck>().FirstOrDefault();
				
                Assert.AreEqual(data.ID, 21);
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
       			
                v.LeftTableID = AddLeftTable();
                v.RightTableID = AddRightTable();
                v.ID = 21;
                context.Set<DataCheck>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckVM));

            DataCheckVM vm = rv.Model as DataCheckVM;
            v = new DataCheck();
            v.ID = vm.Entity.ID;
       		
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LeftTableID", "");
            vm.FC.Add("Entity.RightTableID", "");
            vm.FC.Add("Entity.ID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheck>().FirstOrDefault();
 				
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
        		
                v.LeftTableID = AddLeftTable();
                v.RightTableID = AddRightTable();
                v.ID = 21;
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
				
                v.LeftTableID = AddLeftTable();
                v.RightTableID = AddRightTable();
                v.ID = 21;
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
				
                v1.LeftTableID = AddLeftTable();
                v1.RightTableID = AddRightTable();
                v1.ID = 21;
                v2.LeftTableID = v1.LeftTableID; 
                v2.RightTableID = v1.RightTableID; 
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

        private Int32 AddConnection()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "RvldQyp";
                v.Host = "Vb8x4WaQ";
                v.Port = 38;
                v.Database = "ajYFf";
                v.Username = "U6AOoJ";
                v.Password = "mHrA4";
                v.ID = 85;
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Int32 AddLeftTable()
        {
            Table v = new Table();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.ConnectionID = AddConnection();
                v.TableName = "lXkx";
                v.CreateTimeColumnName = "3BAL5A";
                v.UpdateTimeColumnName = "ktwL";
                v.ID = 3;
                context.Set<Table>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Int32 AddConnection2()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "I9Ey13Vxo";
                v.Host = "8Yicx6";
                v.Port = 65;
                v.Database = "jpHEQa";
                v.Username = "xrkwXJgRf";
                v.Password = "yGzCFwlP";
                v.ID = 28;
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Int32 AddRightTable()
        {
            Table v = new Table();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.ConnectionID = AddConnection2();
                v.TableName = "iaBac0";
                v.CreateTimeColumnName = "GXRDt";
                v.UpdateTimeColumnName = "F8W";
                v.ID = 45;
                context.Set<Table>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
