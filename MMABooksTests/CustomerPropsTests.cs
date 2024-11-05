using NUnit.Framework;

using MMABooksProps;
using System;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerPropsTests
    {
        CustomerProps props;
        [SetUp]
        public void Setup()
        {
            props = new CustomerProps();
            props.CustomerID = "11";
            props.Name = "This is a test";
            props.CustomerCity = "Eugene";
            props.CustomerPhone = "1234567890";
            props.CustomerCountry = "USA";
        }

        [Test]
        public void TestGetCustomer()
        {
            string jsonString = props.GetCustomer();
            Console.WriteLine(jsonString);
            Assert.IsTrue(jsonString.Contains(props.CustomerID));
            Assert.IsTrue(jsonString.Contains(props.Name));
        }

        [Test]
        public void TestSetState()
        {
            string jsonString = props.GetCustomer();
            CustomerProps newProps = new CustomerProps();
            newProps.SetState(jsonString);
            Assert.AreEqual(props.CustomerID, newProps.CustomerID);
            Assert.AreEqual(props.Name, newProps.Name);
            Assert.AreEqual(props.CustomerPhone, newProps.CustomerPhone);
        }

        [Test]
        public void TestClone()
        {
            CustomerProps newProps = (CustomerProps)props.Clone();
            Assert.AreEqual(props.CustomerID, newProps.CustomerID);
            Assert.AreEqual(props.Name, newProps.Name);
            Assert.AreEqual(props.ConcurrencyID, newProps.ConcurrencyID);
        }
    }
}