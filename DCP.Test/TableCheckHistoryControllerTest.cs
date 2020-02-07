using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using DCP.Controllers;
using DCP.ViewModel.TableCheckHistoryVMs;
using DCP.Model;
using DCP.DataAccess;

namespace DCP.Test
{
    [TestClass]
    public class TableCheckHistoryControllerTest
    {
        private TableCheckHistoryController _controller;
        private string _seed;

        public TableCheckHistoryControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<TableCheckHistoryController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as TableCheckHistoryListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(TableCheckHistoryVM));

            TableCheckHistoryVM vm = rv.Model as TableCheckHistoryVM;
            TableCheckHistory v = new TableCheckHistory();
			
            v.TableID = AddTable();
            v.GroupValue = "7NLWrRZHS";
            v.GroupCount = 37;
            v.ID = 88;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TableCheckHistory>().FirstOrDefault();
				
                Assert.AreEqual(data.GroupValue, "7NLWrRZHS");
                Assert.AreEqual(data.GroupCount, 37);
                Assert.AreEqual(data.ID, 88);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            TableCheckHistory v = new TableCheckHistory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.TableID = AddTable();
                v.GroupValue = "7NLWrRZHS";
                v.GroupCount = 37;
                v.ID = 88;
                context.Set<TableCheckHistory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TableCheckHistoryVM));

            TableCheckHistoryVM vm = rv.Model as TableCheckHistoryVM;
            v = new TableCheckHistory();
            v.ID = vm.Entity.ID;
       		
            v.GroupValue = "MjpLS6w1w";
            v.GroupCount = 96;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.TableID", "");
            vm.FC.Add("Entity.GroupValue", "");
            vm.FC.Add("Entity.GroupCount", "");
            vm.FC.Add("Entity.ID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TableCheckHistory>().FirstOrDefault();
 				
                Assert.AreEqual(data.GroupValue, "MjpLS6w1w");
                Assert.AreEqual(data.GroupCount, 96);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            TableCheckHistory v = new TableCheckHistory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.TableID = AddTable();
                v.GroupValue = "7NLWrRZHS";
                v.GroupCount = 37;
                v.ID = 88;
                context.Set<TableCheckHistory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TableCheckHistoryVM));

            TableCheckHistoryVM vm = rv.Model as TableCheckHistoryVM;
            v = new TableCheckHistory();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<TableCheckHistory>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            TableCheckHistory v = new TableCheckHistory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.TableID = AddTable();
                v.GroupValue = "7NLWrRZHS";
                v.GroupCount = 37;
                v.ID = 88;
                context.Set<TableCheckHistory>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            TableCheckHistory v1 = new TableCheckHistory();
            TableCheckHistory v2 = new TableCheckHistory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.TableID = AddTable();
                v1.GroupValue = "7NLWrRZHS";
                v1.GroupCount = 37;
                v1.ID = 88;
                v2.TableID = v1.TableID; 
                v2.GroupValue = "MjpLS6w1w";
                v2.GroupCount = 96;
                context.Set<TableCheckHistory>().Add(v1);
                context.Set<TableCheckHistory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TableCheckHistoryBatchVM));

            TableCheckHistoryBatchVM vm = rv.Model as TableCheckHistoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<TableCheckHistory>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as TableCheckHistoryListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Int32 AddConnection()
        {
            Connection v = new Connection();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "s3LJ";
                v.Host = "pFy1iWj";
                v.Port = 96;
                v.Database = "2v6Pv1Ba";
                v.Username = "Zi0hnIt";
                v.Password = "9967";
                v.ID = 97;
                context.Set<Connection>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Int32 AddTable()
        {
            Table v = new Table();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.ConnectionID = AddConnection();
                v.TableName = "C3ZhAGX6";
                v.CreateTimeColumnName = "70k";
                v.UpdateTimeColumnName = "ItSrT";
                v.ID = 45;
                context.Set<Table>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
