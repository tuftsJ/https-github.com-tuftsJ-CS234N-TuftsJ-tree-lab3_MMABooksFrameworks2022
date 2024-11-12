using NUnit.Framework;

using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    public class CustomerDBTests
    {
        CustomerDB db;

        [SetUp]
        public void ResetData()
        {
            db = new CustomerDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestRetrieve()
        {
            CustomerProps p = (CustomerProps)db.Retrieve("01");
            Assert.AreEqual("01", p.CustomerID);
            Assert.AreEqual("John Doe", p.Name);
            Assert.AreEqual("Eugene", p.City);
            Assert.AreEqual("Ore", p.State);
        }

        [Test]
        public void TestRetrieveAll()
        {
            List<CustomerProps> list = (List<CustomerProps>)db.RetrieveAll();
            Assert.AreEqual(53, list.Count);
        }

        [Test]
        public void TestDelete()
        {
            CustomerProps p = (CustomerProps)db.Retrieve("01");
            Assert.True(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve("01"));
        }


        [Test]
        public void TestDeleteForeignKeyConstraint()
        {
            CustomerProps p = (CustomerProps)db.Retrieve("01");
            Assert.Throws<MySqlException>(() => db.Delete(p));
        }

        [Test]
        public void TestUpdate()
        {
            CustomerProps p = (CustomerProps)db.Retrieve("01");
            p.Name = "James";
            Assert.True(db.Update(p));
            p = (CustomerProps)db.Retrieve("01");
            Assert.AreEqual("James", p.Name);
        }

        [Test]
        public void TestUpdateFieldTooLong()
        {
            CustomerProps p = (CustomerProps)db.Retrieve("01");
            p.Name = "John Doe is customer 01.";
            Assert.Throws<MySqlException>(() => db.Update(p));
        }

        [Test]
        public void TestCreate()
        {
            CustomerProps p = new CustomerProps();
            p.CustomerID = "??";
            p.Name = "Who Am I?";
            db.Create(p);
            CustomerProps p2 = (CustomerProps)db.Retrieve(p.CustomerID);
            Assert.AreEqual(p.GetCustomer(), p2.GetCustomer());
        }

        [Test]
        public void TestCreatePrimaryKeyViolation()
        {
            CustomerProps p = new CustomerProps();
            p.CustomerID = "01";
            p.Name = "John";
            Assert.Throws<MySqlException>(() => db.Create(p));
        }
    }
}