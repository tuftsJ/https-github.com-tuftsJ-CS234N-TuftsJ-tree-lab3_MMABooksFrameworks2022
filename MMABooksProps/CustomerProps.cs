using MMABooksTools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DBDataReader = MySql.Data.MySqlClient.MySqlDataReader;


namespace MMABooksProps
{
    [Serializable()]
    public class CustomerProps : IBaseProps
    {
        #region Auto-implemented Properties
        //properties for the customer
        public string CustomerID { get; set; } = "";
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";

        /// <summary>
        /// ConcurrencyID. Don't manipulate directly.
        /// </summary>
        public int ConcurrencyID { get; set; } = 0;
        #endregion
        public object Clone()
        {
            CustomerProps p = new CustomerProps();
            p.CustomerID = this.CustomerID;
            p.Name = this.Name;
            p.Address = this.Address;
            p.City = this.City; 
            p.State = this.State;
            p.ZipCode = this.ZipCode;
            p.ConcurrencyID = this.ConcurrencyID;
            return p;
        }

        // this is always the same ... so I should have made IBaseProps and abstract class
        public string GetCustomer()
        {
            string jsonString;
            jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

        public string GetState()
        {
            string jsonString;
            jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

        public void SetCustomer(string jsonString)
        {
            CustomerProps p = JsonSerializer.Deserialize<CustomerProps>(jsonString);
            this.CustomerID = p.CustomerID;
            this.Name = p.Name;
            this.ConcurrencyID = p.ConcurrencyID;
        }

        public void SetCustomer(DBDataReader dr)
        {
            this.CustomerID = ((string)dr["CustomerCode"]).Trim();
            this.Name = (string)dr["CustomerName"];
            this.ConcurrencyID = (Int32)dr["ConcurrencyID"];
        }

        public void SetState(string jsonString)
        {
            CustomerProps p = JsonSerializer.Deserialize<CustomerProps>(jsonString);
            this.CustomerID = p.CustomerID;
            this.Name = p.Name;
            this.Address = p.Address;
            this.City = p.City;
            this.State = p.State;
            this.ZipCode = p.ZipCode;
            this.ConcurrencyID = p.ConcurrencyID;
        }

        public void SetState(DBDataReader dr)
        {
            this.CustomerID = ((string)dr["CustomerID"]).Trim();
            this.Name = (string)dr["Name"];
            this.Address = (string)dr["Address"];
            this.City = (string)dr["City"];
            this.State = (string)dr["State"];
            this.ZipCode = (string)dr["ZipCode"];
            this.ConcurrencyID = (Int32)dr["ConcurrencyID"];
        }
    }
}

