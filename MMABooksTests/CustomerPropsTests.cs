using NUnit.Framework;

using MMABooksProps;
using System;
using NuGet.Frameworks;

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
            props.CustomerID = "1";
            props.Name = "Mickey Mouse";
            props.Address = "101 Main St.";
            props.City = "Orlando";
            props.State = "Florida";
            props.ZipCode = "10001";
            
        }

        [Test]
        public void TestGetCustomer()
        {
            string jsonString = props.GetCustomer();
            Console.WriteLine(jsonString);
            Assert.IsTrue(jsonString.Contains(props.Address));
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
            Assert.AreEqual(props.Address, newProps.Address);
            Assert.AreEqual(props.City, newProps.City);
            Assert.AreEqual(props.State, newProps.State);
            Assert.AreEqual(props.ZipCode, newProps.ZipCode);
            Assert.AreEqual(props.ConcurrencyID, newProps.ConcurrencyID);
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