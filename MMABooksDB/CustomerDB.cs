﻿using System;
using System.Collections.Generic;
using System.Text;

using MMABooksTools;
using MMABooksProps;

using System.Data;

// *** I use an "alias" for the ado.net classes throughout my code
// When I switch to an oracle database, I ONLY have to change the actual classes here
using DBBase = MMABooksTools.BaseSQLDB;
using DBConnection = MySql.Data.MySqlClient.MySqlConnection;
using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using DBParameter = MySql.Data.MySqlClient.MySqlParameter;
using DBDataReader = MySql.Data.MySqlClient.MySqlDataReader;
using DBDataAdapter = MySql.Data.MySqlClient.MySqlDataAdapter;
using DBDbType = MySql.Data.MySqlClient.MySqlDbType;

namespace MMABooksDB
{
    public class CustomerDB : DBBase, IReadDB, IWriteDB
    {

        public CustomerDB() : base() { }
        public CustomerDB(DBConnection cn) : base(cn) { }

        public IBaseProps Create(IBaseProps p)
        {
            int rowsAffected = 0;
            CustomerProps props = (CustomerProps)p;

            DBCommand command = new DBCommand();
            command.CommandText = "usp_CustomerCreate";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("custId", DBDbType.Int32);
            command.Parameters.Add("name_p", DBDbType.VarChar);
            //... there are more parameters here
            command.Parameters[0].Direction = ParameterDirection.Output;
            command.Parameters["name_p"].Value = props.Name;
            //... and more values here

            try
            {
                rowsAffected = RunNonQueryProcedure(command);
                if (rowsAffected == 1)
                {
                    props.CustomerID = (string)command.Parameters[0].Value;
                    props.ConcurrencyID = 1;
                    return props;
                }
                else
                    throw new Exception("Unable to insert record. " + props.ToString());
            }
            catch (Exception e)
            {
                // log this error
                throw;
            }
            finally
            {
                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
            }
        }

        public bool Delete(IBaseProps props)
        {
            throw new NotImplementedException();
        }

        public IBaseProps Retrieve(object key)
        {
            throw new NotImplementedException();
        }

        public object RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(IBaseProps props)
        {
            throw new NotImplementedException();
        }
    }
}
