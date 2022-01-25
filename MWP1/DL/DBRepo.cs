using Microsoft.Data.SqlClient;
using System.Data;

namespace DL;


/*
SELECT COUNT(*)
  FROM Store.COLUMNS
 WHERE table_catalog = 'database_name'-- the database
   AND table_name = 'Store'
*/


/// <summary>
/// One massive repo that I wanted to split into smaller ones but ran of time.
/// This handles all my communication to the SQL server.
/// </summary>
public class DBRepo : IRepo
{

    private string _connectionString;
    public DBRepo(string connectionString)
    {
        _connectionString = connectionString;//File.ReadAllText("connectionString.txt");
        //Console.WriteLine(_connectionString);
        //???
    }

    //------------------------------------------------------------------------------\\
    //<>                                Stores                                    <>\\
    //------------------------------------------------------------------------------\\

    /// <summary>
    /// Returns a list of all stores
    /// </summary>
    /// <returns>allStoreSQL</returns>
    public List<Store> GetAllStores()
    {
        List<Store> allStoreSQL = new List<Store>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string storeToSelect = "SELECT * FROM Store";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter storeAdapter = new SqlDataAdapter(storeToSelect, connection);    
        storeAdapter.Fill(CSSet, "Store");
        DataTable? StoreTable = CSSet.Tables["Store"];
            
        if(StoreTable != null)
        { 
            foreach(DataRow row in StoreTable.Rows)
            {
                Store storo = new Store(row);
                allStoreSQL.Add(storo);
            }
        }
        return allStoreSQL;
    }

    //Return Store by ID
    public async Task<Store> GetStoreByIdAsync(int storeId) //Added async and Task<> and Async to name
    {
        string query = "SELECT * FROM Store WHERE StoreId = @storoId";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@storoId", storeId);
        cmd.Parameters.Add(param);

        using SqlDataReader reader = cmd.ExecuteReader();
        Store store = new Store();
        //if(reader.Read()) 
        if (await reader.ReadAsync())
        {
            store.StoreID = reader.GetInt32(0);
            store.StoreName = reader.GetString(1);
            store.State = reader.GetString(2);
            store.City = reader.GetString(3);
            store.SalesTax = reader.GetDecimal(4);
        }

        connection.Close();
        return store;
    }

    /// <summary>
    /// Adds a store to the database
    /// </summary>
    /// <param name="storeToAdd"></param>
    public void AddStore(Store storeToAdd)
    {
        if (IsDuplicate(storeToAdd) == true) { return; }
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Store (StoreName, City, State, SalesTax) VALUES (@p1, @p2, @p3, @p4)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", storeToAdd.StoreName));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", storeToAdd.City));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", storeToAdd.State));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", storeToAdd.SalesTax));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Allows the manager to make changes to store information
    /// </summary>
    /// <param name="changeStoreInfo"></param>
    public void ChangeStoreInfo(Store changeStoreInfo)//(int storeIndex, string name, string city, string state)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = $"UPDATE Store SET StoreName = @p1, City = @p2, State = @p3, SalesTax = @p4 WHERE StoreId = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", changeStoreInfo.StoreID);
                cmd.Parameters.AddWithValue("@p1", changeStoreInfo.StoreName);
                cmd.Parameters.AddWithValue("@p2", changeStoreInfo.City);
                cmd.Parameters.AddWithValue("@p3", changeStoreInfo.State);
                cmd.Parameters.AddWithValue("@p4", changeStoreInfo.SalesTax);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"ChangeStoreInfo: changed: {changed}, invIndex: {changeStoreInfo.StoreID}");
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Check if restaurant is a duplicate
    /// </summary>
    /// <param name="Store"></param>
    /// <returns></returns>
    public bool IsDuplicate(Store store)
    {
        string searchQuery = $"SELECT * FROM Store WHERE StoreName='{store.StoreName}' AND City='{store.City}' AND State='{store.State}'";

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);

        connection.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            //Query returned something, there exists a record that shares the same name, city, and state to the Store the user is trying to create 
            return true;
        }
        //no record was returned. No duplicate record in the db
        return false;
    }

    //------------------------------------------------------------------------------\\
    //<>                               Customers                                  <>\\
    //------------------------------------------------------------------------------\\

    /// <summary>
    /// Returns a list from database of all customers
    /// </summary>
    /// <returns>allcusSQL</returns>
    public List<Customers> GetAllCustomers()
    {
        //throw new NotImplementedException();
        //string checkForUsername = "SELECT userName FROM Customer WHERE userName=' {s.userName}"; //ref
        List<Customers> allcusSQL = new List<Customers>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string cusToSelect = "SELECT * FROM Customers";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter cusAdapter = new SqlDataAdapter(cusToSelect, connection);
        cusAdapter.Fill(CSSet, "Customers");
        DataTable? cusTable = CSSet.Tables["Customers"];

        if (cusTable != null)
        {
            foreach (DataRow row in cusTable.Rows)
            {
                Customers custo = new Customers(row);
                allcusSQL.Add(custo);
            }
        }
        //Console.WriteLine("Finished Get Customers");    
        return allcusSQL;
    }


    public async Task<Customers> GetCustomerByIdAsync(int customerId)
    {
        string query = "SELECT * FROM Customers WHERE CustNumb = @custoId";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@custoId", customerId);
        cmd.Parameters.Add(param);

        using SqlDataReader reader = cmd.ExecuteReader();
        Customers customer = new Customers();
        if (await reader.ReadAsync())
        {
            customer.CustNumb = reader.GetInt32(0);
            customer.UserName = reader.GetString(1);
            customer.Pass = reader.GetString(2);
            customer.Employee = reader.GetBoolean(3);
        }

        connection.Close();
        return customer;
    }

    /// <summary>
    /// Adds a customer to the database
    /// </summary>
    /// <param name="addCust"></param>
    public void AddCustomer(Customers addCust)//int custNum, string userName, string pass
    {
        //throw new NotImplementedException();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Customers (UserName, Pass, Employee) VALUES (@p2, @p3, @p4)";
            //string sqlCmd = "SET IDENTITY_INSERT [dbo].[Customers] ON INSERT INTO Customers (CustNumb, UserName, Pass, Employee) VALUES (@p1, @p2, @p3, @p4) SET IDENTITY_INSERT [dbo].[Customers] OFF";

            using (SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                //SqlParameter param = (new SqlParameter("@p1", addCust.CustNumb));
                //cmd.Parameters.Add(param);
                SqlParameter param = (new SqlParameter("@p2", addCust.UserName));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", addCust.Pass));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", addCust.Employee));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }


    public void ChangeUserInfo(Customers changeCustomerInfo)
    {
        //throw new NotImplementedException();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Customers SET UserName = @p0, Pass = @p1, Employee = @p3 WHERE CustNumb = @p2";
            using (SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", changeCustomerInfo.UserName);
                cmd.Parameters.AddWithValue("@p1", changeCustomerInfo.Pass);
                cmd.Parameters.AddWithValue("@p2", changeCustomerInfo.CustNumb);
                cmd.Parameters.AddWithValue("@p3", changeCustomerInfo.Employee);
                //...

                int changed = cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }


    //------------------------------------------------------------------------------\\
    //<>                               Inventory                                  <>\\
    //------------------------------------------------------------------------------\\

    /// <summary>
    /// Returns a list od all inventory for all stores from the database
    /// </summary>
    /// <returns>allInvSQL</returns>
    public List<Inventory> GetAllInventory()
    {
        List<Inventory> allInvSQL = new List<Inventory>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string invToSelect = "SELECT * FROM Inventory";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter invAdapter = new SqlDataAdapter(invToSelect, connection);       
        invAdapter.Fill(CSSet, "Inventory");
        DataTable? InvTable = CSSet.Tables["Inventory"];
            
        
        if(InvTable != null)
        { 
            foreach(DataRow row in InvTable.Rows)
            {
                Inventory invo = new Inventory(row);
                allInvSQL.Add(invo);
            }
        }
        return allInvSQL;
    }

    //Get Inventory by store
    public async Task<Inventory> GetAllInventoryByStoreAsync(int storeId)
    {
        string query = "SELECT * FROM Inventory WHERE Store = @p0";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@p0", storeId);
        cmd.Parameters.Add(param);

        using SqlDataReader reader = cmd.ExecuteReader();
        Inventory inv = new();
        if (await reader.ReadAsync())
        {
            inv.Id = reader.GetInt32(0);
            inv.Store = reader.GetInt32(1);
            inv.Item = reader.GetInt32(2);
            inv.Qty = reader.GetInt32(3);
        }

        connection.Close();
        return inv;
    }

    /// <summary>
    /// Adds an inventory object to a store
    /// </summary>
    /// <param name="invToAdd"></param>
    public void AddInventory(Inventory invToAdd) //Id, Store, Item, Qty
    {
        //throw new NotImplementedException();     
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Inventory (Store, Item, Qty) VALUES (@p2, @p3, @p4)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p2", invToAdd.Store));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", invToAdd.Item));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", invToAdd.Qty));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// This is for changing the Qty of an item in inventory 
    /// Final Qty calculations are done prior to calling this and qtyToChange is final value
    /// </summary>
    /// <param name="invId"></param>
    /// <param name="qtyToChange"></param>
    public void ChangeInventory(int invId, int qtyToChange)//int storeIndex, Inventory changeInv)//int storeIndex, int apn, int qtyToAdjust
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Inventory SET Qty = @p0 WHERE Id = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                //SqlParameter param = (new SqlParameter("@p1", qtyToChange));
                cmd.Parameters.AddWithValue("@p0", qtyToChange);
                cmd.Parameters.AddWithValue("@p1", invId);
                //...

                int changed = cmd.ExecuteNonQuery();
                //Console.WriteLine($"changed: {changed}, invIndex: {invIndex}");
            }
            connection.Close();
        }
    }


    //------------------------------------------------------------------------------\\
    //<>                              Carried Items                               <>\\
    //------------------------------------------------------------------------------\\

    /// <summary>
    /// Returns a list of all carried items from the database
    /// </summary>
    /// <returns></returns>
    public List<ProdDetails> GetAllCarried()
    {
        //throw new NotImplementedException();
        List<ProdDetails> allcarSQL = new List<ProdDetails>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string carToSelect = "SELECT * FROM Carried";
        DataSet CSSet = new DataSet();
        using SqlDataAdapter carAdapter = new SqlDataAdapter(carToSelect, connection);    
        carAdapter.Fill(CSSet, "Carried");
        DataTable? carTable = CSSet.Tables["Carried"];
            
        if(carTable != null)
        { 
            foreach(DataRow row in carTable.Rows)
            {
                ProdDetails carto = new ProdDetails(row);
                allcarSQL.Add(carto);
            }
        }
        return allcarSQL;
    }

    //Return Carried by ID
    public async Task<ProdDetails> GetCarriedByIdAsync(int carriedId) //Added async and Task<> and Async to name
    {
        string query = "SELECT * FROM Carried WHERE APN = @p0";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@p0", carriedId);
        cmd.Parameters.Add(param);

        using SqlDataReader reader = cmd.ExecuteReader();
        ProdDetails carried = new ProdDetails();
        //if(reader.Read()) 
        if (await reader.ReadAsync())
        {
            carried.APN = reader.GetInt32(0);
            carried.Name = reader.GetString(1);
            carried.ItemType = reader.GetInt32(2);
            carried.Weight = reader.GetDouble(3);
            carried.Cost = reader.GetDecimal(4);
            carried.Descr = reader.GetString(5);
        }

        connection.Close();
        return carried;
    }

    /// <summary>
    /// Adds a new item to list of current items in database
    /// </summary>
    /// <param name="itemNew"></param>
    public void AddCarried(ProdDetails itemNew)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Carried (Name, ItemType, Weight, Cost, Descript) VALUES (@p2, @p4, @p5, @p6, @p7)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p2", itemNew.Name));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", itemNew.ItemType));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p5", itemNew.Weight));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p6", itemNew.Cost));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p7", itemNew.Descr));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Allows manager to make changes to a carried item
    /// </summary>
    /// <param name="changeCarriedItem"></param>
    public void ChangeCarried(ProdDetails changeCarriedItem)//int itemNum, string itemName, int itemType, string itemDesc, decimal itemCost, double itemWeight)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            //Console.WriteLine($"changeCarriedItem.APN: {changeCarriedItem.APN}, changeCarriedItem.Cost: {changeCarriedItem.Cost}");
            connection.Open(); 
            string sqlCmd = "UPDATE Carried SET Name = @p1, ItemType = @p2, Weight = @p3, Cost = @p4, Descript = @p5 WHERE APN = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", changeCarriedItem.APN);
                cmd.Parameters.AddWithValue("@p1", changeCarriedItem.Name);
                cmd.Parameters.AddWithValue("@p2", changeCarriedItem.ItemType);
                cmd.Parameters.AddWithValue("@p3", changeCarriedItem.Weight);
                cmd.Parameters.AddWithValue("@p4", changeCarriedItem.Cost);
                cmd.Parameters.AddWithValue("@p5", changeCarriedItem.Descr);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"Change Carried: changed: {changed}, invIndex: {changeCarriedItem}");
            }
            connection.Close();
        }
    }


    //------------------------------------------------------------------------------\\
    //<>                              Line Items                                  <>\\
    //------------------------------------------------------------------------------\\

    /// <summary>
    /// Returns a list of all line items from the database
    /// </summary>
    /// <returns>List of line items</returns>
    public List<LineItems> GetAllLineItem()
    {
        //throw new NotImplementedException();
        List<LineItems> allLISQL = new List<LineItems>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string liToSelect = "SELECT * FROM LineItems";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter liAdapter = new SqlDataAdapter(liToSelect, connection);    
        liAdapter.Fill(CSSet, "LineItems");
        DataTable? liTable = CSSet.Tables["LineItems"];
            
        if(liTable != null)
        { 
            foreach(DataRow row in liTable.Rows)
            {
                LineItems lio = new LineItems(row);
                allLISQL.Add(lio);
            }
        }   
        return allLISQL;
    }

    //Return Lineitems by ID
    public async Task<LineItems> GetLineitemsByIdAsync(int lineitemId) //Added async and Task<> and Async to name
    {
        string query = "SELECT * FROM Lineitems WHERE Id = @p0";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@p0", lineitemId);
        cmd.Parameters.Add(param);

        using SqlDataReader reader = cmd.ExecuteReader();
        LineItems lineitem = new LineItems();
        //if(reader.Read()) 
        if (await reader.ReadAsync())
        {
            lineitem.Id = reader.GetInt32(0);
            lineitem.InvId = reader.GetInt32(1);
            lineitem.OrderId = reader.GetInt32(2);
            lineitem.Qty = reader.GetInt32(3);
            lineitem.CostPerItem = reader.GetDecimal(4);
            lineitem.SalesTax = reader.GetDecimal(5);
            lineitem.PastOrder = reader.GetBoolean(6);
        }

        connection.Close();
        return lineitem;
    }

    /// <summary>
    /// Allows manager to add a new line item in the database
    /// </summary>
    /// <param name="newLI"></param>
    public void AddLineItem(LineItems newLI)
    {
        //throw new NotImplementedException();
        //Console.WriteLine($"DL: Adding a new item to lineitem,: {newLI}");//DEBUG

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO LineItems (InvId, OrderId, Qty, Cost, SalesTax, PastOrder) VALUES (@p2, @p3, @p4, @p5, @p6, @p7)"; //Id, @p1, 

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter  param = (new SqlParameter("@p2", newLI.InvId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", newLI.OrderId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", newLI.Qty));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p5", newLI.CostPerItem));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p6", newLI.SalesTax));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p7", newLI.PastOrder));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void FinalizeLineItem(LineItems finalLineItem)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE LineItems SET PastOrder = @p0 WHERE OrderId = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", finalLineItem.PastOrder);
                cmd.Parameters.AddWithValue("@p1", finalLineItem.OrderId);
                //...

                int changed = cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }


    //------------------------------------------------------------------------------\\
    //<>                                Orders                                    <>\\
    //------------------------------------------------------------------------------\\
    /// <summary>
    /// Returns a list of all orders from the database
    /// </summary>
    /// <returns>list of all orders</returns>
    public List<Orders> GetAllOrders()
    {
        //throw new NotImplementedException();
        //throw new NotImplementedException();
        List<Orders> allOrdSQL = new List<Orders>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordToSelect = "SELECT * FROM Orders";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordToSelect, connection);    
        ordAdapter.Fill(CSSet, "Orders");
        DataTable? ordTable = CSSet.Tables["Orders"];
            
        if(ordTable != null)
        {
            foreach(DataRow row in ordTable.Rows)
            {
                Orders ordo = new Orders(row);
                allOrdSQL.Add(ordo);
            }
        }
        //Console.WriteLine("Finished Get Orders");    
        return allOrdSQL;
    }


    //Return Order by ID
    public async Task<Orders> GetOrderByIdAsync(int orderId) //Added async and Task<> and Async to name
    {
        string query = "SELECT * FROM Orders WHERE OrderId = @ordoId";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@ordoId", orderId);
        cmd.Parameters.Add(param);

        using SqlDataReader reader = cmd.ExecuteReader();
        Orders order = new Orders();
        //if(reader.Read()) 
        if (await reader.ReadAsync())
        {
            order.OrderId = reader.GetInt32(0);
            order.CustomerId = reader.GetInt32(1);
            order.StoreId = reader.GetInt32(2);
            order.OrderDate = reader.GetDateTime(3);
            order.TotalQty = reader.GetInt32(4);
            order.TotalCost = reader.GetDecimal(5);
            order.OrderCompleted = reader.GetInt32(6);
        }

        connection.Close();
        return order;
    }


    /// <summary>
    /// Adds a new order to the database
    /// </summary>
    /// <param name="orderItems"></param>
    public void AddOrder(Orders orderItems)
    {
        //throw new NotImplementedException();
        /*Reference: DateTime dateTimeVariable = //some DateTime value, e.g. DateTime.Now;
        SqlCommand cmd = new SqlCommand("INSERT INTO <table> (<column>) VALUES (@value)", connection);
        cmd.Parameters.AddWithValue("@value", dateTimeVariable);*/
        //Console.WriteLine($"DL: Adding a new order");

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Orders (CustomerId, StoreId, OrderDate, Total, TotalCost, OrderCompleted) VALUES (@p2, @p3, @p4, @p5, @p6, @p7)"; //OrderId, @p1,

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                
                SqlParameter param = (new SqlParameter("@p2", orderItems.CustomerId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", orderItems.StoreId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", orderItems.OrderDate));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p5", orderItems.TotalQty));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p6", orderItems.TotalCost));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p7", orderItems.OrderCompleted));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Adds total quantity and cost to the order and makes the order as completed
    /// This is called after a purchase is confirmed
    /// </summary>
    /// <param name="finalDetails"></param>
    public void FinalizeOrder(Orders finalDetails)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Orders SET Total = @p0, TotalCost = @p1, OrderCompleted = @p3, OrderDate = @p4 WHERE OrderId = @p2";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", finalDetails.TotalQty);
                cmd.Parameters.AddWithValue("@p1", finalDetails.TotalCost);
                cmd.Parameters.AddWithValue("@p2", finalDetails.OrderId);
                cmd.Parameters.AddWithValue("@p3", finalDetails.OrderCompleted);
                cmd.Parameters.AddWithValue("@p4", finalDetails.OrderDate);
                //...

                int changed = cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void OmniDelete(int whatTable, int idToDelete)
    {
        //Omni Delete logic
        //Add another parameter to select table to delete from
        //Set two string vars like = "Orders"  = "OrderId" to set to approriate columns and tables
        string tableName = "";
        string idStr = "";

        if (whatTable == 1) { tableName = "Lineitems"; idStr = "Id"; }//Lineitems
        if (whatTable == 2) { tableName = "Inventory"; idStr = "Id"; }//Inventory
        if (whatTable == 3) { tableName = "Carried"; idStr = "APN"; }//Carried
        if (whatTable == 4) { tableName = "Orders"; idStr = "OrderId"; }//Orders
        if (whatTable == 5) { tableName = "Customers"; idStr = "CustNumb"; }//Customers
        if (whatTable == 6) { tableName = "Store"; idStr = "StoreId"; }//Store

        //throw new NotImplementedException();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = $"DELETE FROM {tableName} WHERE {idStr} = @p0"; //Find Store Id before calling this
            using (SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", idToDelete);
                //...

                int changed = cmd.ExecuteNonQuery();
                //Console.WriteLine($"changed: {changed}, ordersToDelete: {ordersToDelete}");//DEBUGGING
            }
            connection.Close();
        }
    }
}