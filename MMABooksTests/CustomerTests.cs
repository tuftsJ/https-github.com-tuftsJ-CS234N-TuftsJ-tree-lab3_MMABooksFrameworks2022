using NUnit.Framework;

using MMABooksBusiness;
using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {

        [SetUp]
        public void TestResetDatabase()
        {
            CustomerDB db = new CustomerDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetStateData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestNewCustomerConstructor()
        {
            // not in Data Store - no code
            Customer c = new Customer();
            Assert.AreEqual(string.Empty, c.CustomerID);
            Assert.AreEqual(string.Empty, c.Name);
            Assert.IsTrue(c.IsNew);
            Assert.IsFalse(c.IsValid);
        }


        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Customer c = new Customer("01");
            Assert.AreEqual("01", c.CustomerID);
            Assert.IsTrue(c.Name.Length > 0);
            Assert.IsFalse(c.IsNew);
            Assert.IsTrue(c.IsValid);
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Customer c = new Customer();
            c.CustomerID = "??";
            c.Name = "Who Am I";
            c.Save();
            Customer c2 = new Customer("??");
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
        }

        [Test]
        public void TestUpdate()
        {
            Customer c = new Customer("01");
            c.Name = "Edited Name";
            c.Save();

            Customer c2 = new Customer("01");
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
        }

        [Test]
        public void TestDelete()
        {
            Customer c = new Customer("01");
            c.Delete();
            c.Save();
            Assert.Throws<Exception>(() => new Customer("01"));
        }

        [Test]
        public void TestGetList()
        {
            Customer c = new Customer();
            List<Customer> customers = (List<Customer>)c.GetList();
            Assert.AreEqual(53, customers.Count);
            Assert.AreEqual("Alabama", customers[0].Name);
            Assert.AreEqual("AL", customers[0].CustomerID);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
            c.CustomerID = "??";
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Customer c = new Customer();
            Assert.Throws<ArgumentOutOfRangeException>(() => c.CustomerID = "???");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Customer c1 = new Customer("01");
            Customer c2 = new Customer("01");

            c1.Name = "Updated first";
            c1.Save();

            c2.Name = "Updated second";
            Assert.Throws<Exception>(() => c2.Save());
        }
    }
}