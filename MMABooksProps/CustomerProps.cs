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
        public string CustomerID { get; set; } = "";

        public string Name { get; set; } = "";

        public string CustomerPhone { get; set; } = "";
        public string CustomerCity { get; set; } = "";
        public string CustomerCountry { get; set; } = "";


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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void SetState(DBDataReader dr)
        {
            throw new NotImplementedException();
        }
    }
}

