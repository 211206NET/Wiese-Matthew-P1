/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DL
{
    internal class OmniRepo
    {
        private string _connectionString;
        public OmniRepo(string connectionString)
        {
            _connectionString = connectionString;

        }

        //------------------------------------------------------------------------------\\
        //<>                                Omni Get                                  <>\\
        //------------------------------------------------------------------------------\\
        public List<T> GetAll<T>(int listId)
        {

            using SqlConnection connection = new SqlConnection(_connectionString);

            List<Store> allStoreSQL = new List<Store>();
            switch (listId)//1 = store, 2 = Customers, 3 = Inventory, 4 = Carried Items, 5 = Line Items, 6 = Orders
            {
                case 1:
                    //List<Store> allStoreSQL = new List<Store>();
                    string storeToSelect = "SELECT * FROM Store";
                    DataSet CSSet = new DataSet();

                    using SqlDataAdapter storeAdapter = new SqlDataAdapter(storeToSelect, connection);
                    storeAdapter.Fill(CSSet, "Store");
                    DataTable? StoreTable = CSSet.Tables["Store"];

                    if (StoreTable != null)
                    {
                        foreach (DataRow row in StoreTable.Rows)
                        {
                            Store storo = new Store(row);
                            allStoreSQL.Add(storo);
                        }
                    }

                    return allStoreSQL;

                break;

                default:

                    return allStoreSQL;
                break;
            }
        }


        



    //........................................................................................\\
    }//End Class
}//End Namespace*/
