using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using DCP.Controllers;
using DCP.ViewModel.DataCheckResultVMs;
using DCP.Model;
using DCP.DataAccess;

namespace DCP.Test
{
    [TestClass]
    public class DataCheckResultControllerTest
    {
        private DataCheckResultController _controller;
        private string _seed;

        public DataCheckResultControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DataCheckResultController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as DataCheckResultListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckResultVM));

            DataCheckResultVM vm = rv.Model as DataCheckResultVM;
            DataCheckResult v = new DataCheckResult();
			
            v.DataCheckID = AddDataCheck();
            v.DataCheckRunID = AddDataCheckRun();
            v.CreatedDate = "lCty6QGB";
            v.LeftCount = 93;
            v.RightCount = 31;
            v.ID = 31;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheckResult>().FirstOrDefault();
				
                Assert.AreEqual(data.CreatedDate, "lCty6QGB");
                Assert.AreEqual(data.LeftCount, 93);
                Assert.AreEqual(data.RightCount, 31);
                Assert.AreEqual(data.ID, 31);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DataCheckResult v = new DataCheckResult();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DataCheckID = AddDataCheck();
                v.DataCheckRunID = AddDataCheckRun();
                v.CreatedDate = "lCty6QGB";
                v.LeftCount = 93;
                v.RightCount = 31;
                v.ID = 31;
                context.Set<DataCheckResult>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckResultVM));

            DataCheckResultVM vm = rv.Model as DataCheckResultVM;
            v = new DataCheckResult();
            v.ID = vm.Entity.ID;
       		
            v.CreatedDate = "piqFa";
            v.LeftCount = 42;
            v.RightCount = 21;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DataCheckID", "");
            vm.FC.Add("Entity.DataCheckRunID", "");
            vm.FC.Add("Entity.CreatedDate", "");
            vm.FC.Add("Entity.LeftCount", "");
            vm.FC.Add("Entity.RightCount", "");
            vm.FC.Add("Entity.ID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DataCheckResult>().FirstOrDefault();
 				
                Assert.AreEqual(data.CreatedDate, "piqFa");
                Assert.AreEqual(data.LeftCount, 42);
                Assert.AreEqual(data.RightCount, 21);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DataCheckResult v = new DataCheckResult();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DataCheckID = AddDataCheck();
                v.DataCheckRunID = AddDataCheckRun();
                v.CreatedDate = "lCty6QGB";
                v.LeftCount = 93;
                v.RightCount = 31;
                v.ID = 31;
                context.Set<DataCheckResult>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckResultVM));

            DataCheckResultVM vm = rv.Model as DataCheckResultVM;
            v = new DataCheckResult();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<DataCheckResult>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DataCheckResult v = new DataCheckResult();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.DataCheckID = AddDataCheck();
                v.DataCheckRunID = AddDataCheckRun();
                v.CreatedDate = "lCty6QGB";
                v.LeftCount = 93;
                v.RightCount = 31;
                v.ID = 31;
                context.Set<DataCheckResult>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            DataCheckResult v1 = new DataCheckResult();
            DataCheckResult v2 = new DataCheckResult();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DataCheckID = AddDataCheck();
                v1.DataCheckRunID = AddDataCheckRun();
                v1.CreatedDate = "lCty6QGB";
                v1.LeftCount = 93;
                v1.RightCount = 31;
                v1.ID = 31;
                v2.DataCheckID = v1.DataCheckID; 
                v2.DataCheckRunID = v1.DataCheckRunID; 
                v2.CreatedDate = "piqFa";
                v2.LeftCount = 42;
                v2.RightCount = 21;
                context.Set<DataCheckResult>().Add(v1);
                context.Set<DataCheckResult>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DataCheckResultBatchVM));

            DataCheckResultBatchVM vm = rv.Model as DataCheckResultBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<DataCheckResult>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DataCheckResultListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Int32 AddConnection()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "XVVshT";
                v.Host = "7DJP2or";
                v.Port = 73;
                v.Database = "Hd9";
                v.Username = "pegXQ12";
                v.Password = "hz1nXC";
                v.ID = 22;
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
                v.TableName = "1ojc";
                v.CreateTimeColumnName = "jmOfWxPP";
                v.UpdateTimeColumnName = "IuVQLsDbV";
                v.ID = 53;
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

                v.Name = "iGGHM";
                v.Host = "CHn";
                v.Port = 9;
                v.Database = "H4CUa";
                v.Username = "cQm";
                v.Password = "jMJQ";
                v.ID = 40;
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
                v.TableName = "sHsDKJOD";
                v.CreateTimeColumnName = "QIRz1";
                v.UpdateTimeColumnName = "dSL1dVPw";
                v.ID = 35;
                context.Set<Table>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Int32 AddDataCheck()
        {
            DataCheck v = new DataCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.LeftTableID = AddLeftTable();
                v.RightTableID = AddRightTable();
                v.ID = 54;
                context.Set<DataCheck>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Int32 AddDataCheckRun()
        {
            DataCheckRun v = new DataCheckRun();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.RunName = "db9hTv";
                v.ID = 61;
                context.Set<DataCheckRun>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
