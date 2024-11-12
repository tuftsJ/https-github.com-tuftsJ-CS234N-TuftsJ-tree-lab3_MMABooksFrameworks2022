using MMABooksTools;
using System;
using System.Collections.Generic;
using System.Text;
using MMABooksProps;
using MMABooksDB;

namespace MMABooksBusiness
{
    public class Customer : BaseBusiness
    {
        /// <summary>
        /// Read/Write property. 
        /// </summary>
        //  Notice that I used a name for the business object property that I thought would be more intuitive for the 
        //  application programmer.  It doesn't have to match the database.
        public string CustomerID
        {
            get
            {
                return ((CustomerProps)mProps).CustomerID;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).CustomerID))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 2)
                    {
                        mRules.RuleBroken("Abbreviation", false);
                        ((CustomerProps)mProps).CustomerID = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Abbreviation must be no more than 2 characters long.");
                    }
                }
            }
        }

        public String Name
        {
            get
            {
                return ((CustomerProps)mProps).Name;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).Name))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 20)
                    {
                        mRules.RuleBroken("Name", false);
                        ((CustomerProps)mProps).Name = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Name must be no more than 20 characters long.");
                    }
                }
            }
        }

        /*
         public string CustomerPhone { get; set; } = "";
        public string CustomerCity { get; set; } = "";
        public string CustomerCountry { get; set; } = "";

         
         */

        public string CustomerPhone
        {
            get
            {
                return ((CustomerProps)mProps).Address;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).Address))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 20)
                    {
                        mRules.RuleBroken("Name", false);
                        ((CustomerProps)mProps).Name = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Phone number must be no more than 20 characters long.");
                    }
                }
            }
        }
        
        public string City 
        {
            get
            {
                return ((CustomerProps)mProps).City;
            }
            
            set
            {
                if (!(value == ((CustomerProps)mProps).City))
                {                    
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public string State
        {
            get
            {
                return (((CustomerProps)mProps).State);
            }

            set
            {
                if (!(value == (((CustomerProps)mProps).State)))
                {
                }
                else 
                { 
                    throw new ArgumentOutOfRangeException(); } 
            }
        }
        public override object GetList()
        {
            List<Customer> customers = new List<Customer>();
            List<CustomerProps> props = new List<CustomerProps>();


            props = (List<CustomerProps>)mdbReadable.RetrieveAll();
            foreach (CustomerProps prop in props)
            {
                Customer c = new Customer(prop);
                customers.Add(c);
            }

            return customers;
        }

        protected override void SetDefaultProperties()
        {
        }

        /// <summary>
        /// Sets required fields for a record.
        /// </summary>
        protected override void SetRequiredRules()
        {
            mRules.RuleBroken("Abbreviation", true);
            mRules.RuleBroken("Name", true);
        }

        /// <summary>
        /// Instantiates mProps and mOldProps as new Props objects.
        /// Instantiates mbdReadable and mdbWriteable as new DB objects.
        /// </summary>
        protected override void SetUp()
        {
            mProps = new CustomerProps();
            mOldProps = new CustomerProps();

            mdbReadable = new CustomerDB();
            mdbWriteable = new CustomerDB();
        }

        #region constructors
        /// <summary>
        /// Default constructor - gets the connection string - assumes a new record that is not in the database.
        /// </summary>
        public Customer() : base()
        {
        }

        /// <summary>
        /// Calls methods SetUp() and Load().
        /// Use this constructor when the object is in the database AND the connection string is in a config file
        /// </summary>
        /// <param name="key">ID number of a record in the database.
        /// Sent as an arg to Load() to set values of record to properties of an 
        /// object.</param>
        public Customer(string key)
            : base(key)
        {
        }

        private Customer(CustomerProps props)
            : base(props)
        {
        }

        #endregion
    }
}

